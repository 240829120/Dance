using HelixToolkit.SharpDX.Core;
using HelixToolkit.SharpDX.Core.Model.Scene;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 操作组件
    /// </summary>
    public class DanceTransformManipulator3D : GroupElement3D
    {
        /// <summary>
        /// X轴平移形状
        /// </summary>
        private static readonly Geometry3D TranslationXGeometry;

        /// <summary>
        /// X轴旋转形状
        /// </summary>
        private static readonly Geometry3D RotationXGeometry;

        /// <summary>
        /// 缩放形状
        /// </summary>
        private static readonly Geometry3D ScalingGeometry;

        static DanceTransformManipulator3D()
        {
            var bd = new MeshBuilder();
            var arrowLength = 1.5f;
            bd.AddArrow(Vector3.UnitX * arrowLength, new Vector3(1.2f * arrowLength, 0, 0), 0.08, 4, 12);
            bd.AddCylinder(Vector3.Zero, Vector3.UnitX * arrowLength, 0.04, 12);
            TranslationXGeometry = bd.ToMesh();

            bd = new MeshBuilder();
            var circle = MeshBuilder.GetCircle(32, true);
            var path = circle.Select(x => new Vector3(0, x.X, x.Y)).ToArray();
            bd.AddTube(path, 0.06, 8, true);
            RotationXGeometry = bd.ToMesh();

            bd = new MeshBuilder();
            bd.AddBox(Vector3.UnitX * 0.8f, 0.15, 0.15, 0.15);
            bd.AddCylinder(Vector3.Zero, Vector3.UnitX * 0.8f, 0.02, 4);
            ScalingGeometry = bd.ToMesh();

            TranslationXGeometry.OctreeParameter.MinimumOctantSize = 0.01f;
            TranslationXGeometry.UpdateOctree();

            RotationXGeometry.OctreeParameter.MinimumOctantSize = 0.01f;
            RotationXGeometry.UpdateOctree();

            ScalingGeometry.OctreeParameter.MinimumOctantSize = 0.01f;
            ScalingGeometry.UpdateOctree();
        }

        // =======================================================================================================
        // Field

        /// <summary>
        /// 平移模型
        /// </summary>
        private readonly MeshGeometryModel3D translationX, translationY, translationZ;

        /// <summary>
        /// 旋转模型
        /// </summary>
        private readonly MeshGeometryModel3D rotationX, rotationY, rotationZ;

        /// <summary>
        /// 缩放模型
        /// </summary>
        private readonly MeshGeometryModel3D scaleX, scaleY, scaleZ;

        /// <summary>
        /// 模型分组
        /// </summary>
        private readonly GroupModel3D translationGroup, rotationGroup, scaleGroup, ctrlGroup;

        /// <summary>
        /// 当前元素模型集合
        /// </summary>
        private readonly List<Element3D> models = new();

        // =======================================================================================================
        // Property

        #region Target -- 目标

        /// <summary>
        /// 目标
        /// </summary>
        public IDanceModel3D? Target
        {
            get { return (IDanceModel3D?)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        /// <summary>
        /// 目标
        /// </summary>
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(IDanceModel3D), typeof(DanceTransformManipulator3D), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceTransformManipulator3D manipulator)
                    return;

                manipulator.OnTargetChanged();
            })));

        #endregion

        #region Variables

        private readonly Element3D xrayEffect;
        private Vector3 centerOffset = Vector3.Zero;
        private Vector3 translationVector = Vector3.Zero;
        private Matrix rotationMatrix = Matrix.Identity;
        private Matrix scaleMatrix = Matrix.Identity;
        private Matrix targetMatrix = Matrix.Identity;

        private Element3D? target;
        private Viewport3DX? currentViewport;
        private Vector3 lastHitPosWS;
        private Vector3 normal;

        private Vector3 direction;
        private Vector3 currentHit;
        private bool isCaptured = false;
        private double sizeScale = 1;
        private Color4 currentColor;
        #endregion
        private enum ManipulationType
        {
            None, TranslationX, TranslationY, TranslationZ, RotationX, RotationY, RotationZ, ScaleX, ScaleY, ScaleZ
        }

        private ManipulationType manipulationType = ManipulationType.None;

        public DanceTransformManipulator3D()
        {
            var rotationYMatrix = Matrix.RotationZ((float)Math.PI / 2);
            var rotationZMatrix = Matrix.RotationY(-(float)Math.PI / 2);
            ctrlGroup = new GroupModel3D();

            translationX = new MeshGeometryModel3D() { Geometry = TranslationXGeometry, Material = DiffuseMaterials.Red, CullMode = SharpDX.Direct3D11.CullMode.Back, PostEffects = "ManipulatorXRayGrid" };
            translationY = new MeshGeometryModel3D() { Geometry = TranslationXGeometry, Material = DiffuseMaterials.Green, CullMode = SharpDX.Direct3D11.CullMode.Back, PostEffects = "ManipulatorXRayGrid" };
            translationZ = new MeshGeometryModel3D() { Geometry = TranslationXGeometry, Material = DiffuseMaterials.Blue, CullMode = SharpDX.Direct3D11.CullMode.Back, PostEffects = "ManipulatorXRayGrid" };

            translationY.Transform = new System.Windows.Media.Media3D.MatrixTransform3D(rotationYMatrix.ToMatrix3D());
            translationZ.Transform = new System.Windows.Media.Media3D.MatrixTransform3D(rotationZMatrix.ToMatrix3D());
            translationX.Mouse3DDown += Translation_Mouse3DDown;
            translationY.Mouse3DDown += Translation_Mouse3DDown;
            translationZ.Mouse3DDown += Translation_Mouse3DDown;
            translationX.Mouse3DMove += Translation_Mouse3DMove;
            translationY.Mouse3DMove += Translation_Mouse3DMove;
            translationZ.Mouse3DMove += Translation_Mouse3DMove;
            translationX.Mouse3DUp += Manipulation_Mouse3DUp;
            translationY.Mouse3DUp += Manipulation_Mouse3DUp;
            translationZ.Mouse3DUp += Manipulation_Mouse3DUp;

            translationGroup = new GroupModel3D();
            translationGroup.Children.Add(translationX);
            translationGroup.Children.Add(translationY);
            translationGroup.Children.Add(translationZ);
            ctrlGroup.Children.Add(translationGroup);

            rotationX = new MeshGeometryModel3D() { Geometry = RotationXGeometry, Material = DiffuseMaterials.Red, CullMode = SharpDX.Direct3D11.CullMode.Back, PostEffects = "ManipulatorXRayGrid" };
            rotationY = new MeshGeometryModel3D() { Geometry = RotationXGeometry, Material = DiffuseMaterials.Green, CullMode = SharpDX.Direct3D11.CullMode.Back, PostEffects = "ManipulatorXRayGrid" };
            rotationZ = new MeshGeometryModel3D() { Geometry = RotationXGeometry, Material = DiffuseMaterials.Blue, CullMode = SharpDX.Direct3D11.CullMode.Back, PostEffects = "ManipulatorXRayGrid" };

            rotationY.Transform = new System.Windows.Media.Media3D.MatrixTransform3D(rotationYMatrix.ToMatrix3D());
            rotationZ.Transform = new System.Windows.Media.Media3D.MatrixTransform3D(rotationZMatrix.ToMatrix3D());
            rotationX.Mouse3DDown += Rotation_Mouse3DDown;
            rotationY.Mouse3DDown += Rotation_Mouse3DDown;
            rotationZ.Mouse3DDown += Rotation_Mouse3DDown;
            rotationX.Mouse3DMove += Rotation_Mouse3DMove;
            rotationY.Mouse3DMove += Rotation_Mouse3DMove;
            rotationZ.Mouse3DMove += Rotation_Mouse3DMove;
            rotationX.Mouse3DUp += Manipulation_Mouse3DUp;
            rotationY.Mouse3DUp += Manipulation_Mouse3DUp;
            rotationZ.Mouse3DUp += Manipulation_Mouse3DUp;

            rotationGroup = new GroupModel3D();
            rotationGroup.Children.Add(rotationX);
            rotationGroup.Children.Add(rotationY);
            rotationGroup.Children.Add(rotationZ);
            ctrlGroup.Children.Add(rotationGroup);

            scaleX = new MeshGeometryModel3D() { Geometry = ScalingGeometry, Material = DiffuseMaterials.Red, CullMode = SharpDX.Direct3D11.CullMode.Back, PostEffects = "ManipulatorXRayGrid" };
            scaleY = new MeshGeometryModel3D() { Geometry = ScalingGeometry, Material = DiffuseMaterials.Green, CullMode = SharpDX.Direct3D11.CullMode.Back, PostEffects = "ManipulatorXRayGrid" };
            scaleZ = new MeshGeometryModel3D() { Geometry = ScalingGeometry, Material = DiffuseMaterials.Blue, CullMode = SharpDX.Direct3D11.CullMode.Back, PostEffects = "ManipulatorXRayGrid" };

            scaleY.Transform = new System.Windows.Media.Media3D.MatrixTransform3D(rotationYMatrix.ToMatrix3D());
            scaleZ.Transform = new System.Windows.Media.Media3D.MatrixTransform3D(rotationZMatrix.ToMatrix3D());
            scaleX.Mouse3DDown += Scaling_Mouse3DDown;
            scaleY.Mouse3DDown += Scaling_Mouse3DDown;
            scaleZ.Mouse3DDown += Scaling_Mouse3DDown;
            scaleX.Mouse3DMove += Scaling_Mouse3DMove;
            scaleY.Mouse3DMove += Scaling_Mouse3DMove;
            scaleZ.Mouse3DMove += Scaling_Mouse3DMove;
            scaleX.Mouse3DUp += Manipulation_Mouse3DUp;
            scaleY.Mouse3DUp += Manipulation_Mouse3DUp;

            scaleGroup = new GroupModel3D();
            scaleGroup.Children.Add(scaleX);
            scaleGroup.Children.Add(scaleY);
            scaleGroup.Children.Add(scaleZ);
            ctrlGroup.Children.Add(scaleGroup);

            Children.Add(ctrlGroup);
            xrayEffect = new PostEffectMeshXRayGrid()
            {
                EffectName = "ManipulatorXRayGrid",
                DimmingFactor = 0.5,
                BlendingFactor = 0.8,
                GridDensity = 4,
                GridColor = System.Windows.Media.Colors.Gray
            };
            if (xrayEffect.SceneNode is NodePostEffectXRayGrid grid)
            {
                grid.XRayDrawingPassName = DefaultPassNames.EffectMeshDiffuseXRayGridP3;
            }

            Children.Add(xrayEffect);
            SceneNode.Attached += SceneNode_OnAttached;
            SceneNode.Detached += SceneNode_OnDetached;


            models.Add(this.translationX);
            models.Add(this.translationY);
            models.Add(this.translationZ);
            models.Add(this.rotationX);
            models.Add(this.rotationY);
            models.Add(this.rotationZ);
            models.Add(this.scaleX);
            models.Add(this.scaleY);
            models.Add(this.scaleZ);
        }

        /// <summary>
        /// 判断元素是否被命中
        /// </summary>
        /// <param name="element">元素</param>
        /// <returns>是否被命中</returns>
        public bool HitTest(Element3D element)
        {
            return this.models.Contains(element);
        }

        private void SceneNode_OnDetached(object? sender, EventArgs? e)
        {
            //if (target != null)
            //{
            //    target.SceneNode.OnTransformChanged -= SceneNode_OnTransformChanged;
            //}
        }

        private void SceneNode_OnAttached(object? sender, EventArgs? e)
        {
            OnTargetChanged();
        }

        protected virtual bool CanBeginTransform(MouseDown3DEventArgs? e)
        {
            return true;
        }

        #region Handle Translation
        private void Translation_Mouse3DDown(object? sender, MouseDown3DEventArgs? e)
        {
            if (e == null || target == null || !CanBeginTransform(e))
            {
                return;
            }
            if (!(e.HitTestResult.ModelHit is Element3D elem))
            {
                manipulationType = ManipulationType.None;
                isCaptured = false;
                return;
            }
            if (elem == translationX)
            {
                manipulationType = ManipulationType.TranslationX;
                direction = Vector3.UnitX;
            }
            else if (elem == translationY)
            {
                manipulationType = ManipulationType.TranslationY;
                direction = Vector3.UnitY;
            }
            else if (elem == translationZ)
            {
                manipulationType = ManipulationType.TranslationZ;
                direction = Vector3.UnitZ;
            }
            else
            {
                manipulationType = ManipulationType.None;
                isCaptured = false;
                return;
            }
            var material = ((e.HitTestResult.ModelHit as MeshGeometryModel3D).Material as DiffuseMaterial);
            currentColor = material.DiffuseColor;
            material.DiffuseColor = Color.Yellow;
            currentViewport = e.Viewport;
            var cameraNormal = Vector3.Normalize(e.Viewport.Camera.CameraInternal.LookDirection);
            this.lastHitPosWS = e.HitTestResult.PointHit;
            var up = Vector3.Cross(cameraNormal, direction);
            normal = Vector3.Cross(up, direction);
            if (currentViewport.UnProjectOnPlane(e.Position.ToVector2(), lastHitPosWS, normal, out var hit))
            {
                currentHit = hit;
                isCaptured = true;
            }
        }

        private void Translation_Mouse3DMove(object? sender, MouseMove3DEventArgs? e)
        {
            if (e == null || !isCaptured)
            {
                return;
            }
            if (currentViewport.UnProjectOnPlane(e.Position.ToVector2(), lastHitPosWS, normal, out var hit))
            {
                var moveDir = hit - currentHit;
                currentHit = hit;
                switch (manipulationType)
                {
                    case ManipulationType.TranslationX:
                        translationVector += new Vector3(moveDir.X, 0, 0);
                        break;
                    case ManipulationType.TranslationY:
                        translationVector += new Vector3(0, moveDir.Y, 0);
                        break;
                    case ManipulationType.TranslationZ:
                        translationVector += new Vector3(0, 0, moveDir.Z);
                        break;
                }
                OnUpdateSelfTransform();
                OnUpdateTargetMatrix();
            }
        }
        #endregion

        #region Handle Rotation
        private void Rotation_Mouse3DDown(object? sender, MouseDown3DEventArgs? e)
        {
            if (e == null || target == null || !CanBeginTransform(e))
            {
                return;
            }
            if (!(e.HitTestResult.ModelHit is Element3D elem))
            {
                manipulationType = ManipulationType.None;
                isCaptured = false;
                return;
            }
            if (elem == rotationX)
            {
                manipulationType = ManipulationType.RotationX;
                direction = new Vector3(1, 0, 0);
            }
            else if (elem == rotationY)
            {
                manipulationType = ManipulationType.RotationY;
                direction = new Vector3(0, 1, 0);
            }
            else if (elem == rotationZ)
            {
                manipulationType = ManipulationType.RotationZ;
                direction = new Vector3(0, 0, 1);
            }
            else
            {
                manipulationType = ManipulationType.None;
                isCaptured = false;
                return;
            }
            var material = ((e.HitTestResult.ModelHit as MeshGeometryModel3D).Material as DiffuseMaterial);
            currentColor = material.DiffuseColor;
            material.DiffuseColor = Color.Yellow;
            currentViewport = e.Viewport;
            normal = Vector3.Normalize(e.Viewport.Camera.CameraInternal.LookDirection);
            this.lastHitPosWS = e.HitTestResult.PointHit;
            //var up = Vector3.Cross(cameraNormal, direction);
            //normal = Vector3.Cross(up, direction);
            if (currentViewport.UnProjectOnPlane(e.Position.ToVector2(), lastHitPosWS, normal, out var hit))
            {
                currentHit = hit;
                isCaptured = true;
            }
        }

        private void Rotation_Mouse3DMove(object? sender, MouseMove3DEventArgs? e)
        {
            if (e == null || !isCaptured)
            {
                return;
            }
            if (currentViewport.UnProjectOnPlane(e.Position.ToVector2(), lastHitPosWS, normal, out var hit))
            {
                var position = this.translationVector + centerOffset;
                var v = Vector3.Normalize(currentHit - position);
                var u = Vector3.Normalize(hit - position);
                var currentAxis = Vector3.Cross(u, v);
                var axis = Vector3.UnitX;
                currentHit = hit;
                switch (manipulationType)
                {
                    case ManipulationType.RotationX:
                        axis = Vector3.UnitX;
                        break;
                    case ManipulationType.RotationY:
                        axis = Vector3.UnitY;
                        break;
                    case ManipulationType.RotationZ:
                        axis = Vector3.UnitZ;
                        break;
                }
                var sign = -Vector3.Dot(axis, currentAxis);
                var theta = (float)(Math.Sign(sign) * Math.Asin(currentAxis.Length()));
                switch (manipulationType)
                {
                    case ManipulationType.RotationX:
                        rotationMatrix *= Matrix.RotationX(theta);
                        break;
                    case ManipulationType.RotationY:
                        rotationMatrix *= Matrix.RotationY(theta);
                        break;
                    case ManipulationType.RotationZ:
                        rotationMatrix *= Matrix.RotationZ(theta);
                        break;
                }
                OnUpdateTargetMatrix();
            }
        }
        #endregion

        #region Handle Scaling
        private void Scaling_Mouse3DDown(object? sender, MouseDown3DEventArgs? e)
        {
            if (e == null || target == null || !CanBeginTransform(e))
            {
                return;
            }
            if (!(e.HitTestResult.ModelHit is Element3D elem))
            {
                manipulationType = ManipulationType.None;
                isCaptured = false;
                return;
            }
            if (elem == scaleX)
            {
                manipulationType = ManipulationType.ScaleX;
                direction = Vector3.UnitX;
            }
            else if (elem == scaleY)
            {
                manipulationType = ManipulationType.ScaleY;
                direction = Vector3.UnitY;
            }
            else if (elem == scaleZ)
            {
                manipulationType = ManipulationType.ScaleZ;
                direction = Vector3.UnitZ;
            }
            else
            {
                manipulationType = ManipulationType.None;
                isCaptured = false;
                return;
            }
            var material = ((e.HitTestResult.ModelHit as MeshGeometryModel3D).Material as DiffuseMaterial);
            currentColor = material.DiffuseColor;
            material.DiffuseColor = Color.Yellow;
            currentViewport = e.Viewport;
            var cameraNormal = Vector3.Normalize(e.Viewport.Camera.CameraInternal.LookDirection);
            this.lastHitPosWS = e.HitTestResult.PointHit;
            var up = Vector3.Cross(cameraNormal, direction);
            normal = Vector3.Cross(up, direction);
            if (currentViewport.UnProjectOnPlane(e.Position.ToVector2(), lastHitPosWS, normal, out var hit))
            {
                currentHit = hit;
                isCaptured = true;
            }
        }

        private void Scaling_Mouse3DMove(object? sender, MouseMove3DEventArgs? e)
        {
            if (e == null || !isCaptured)
            {
                return;
            }
            if (currentViewport.UnProjectOnPlane(e.Position.ToVector2(), lastHitPosWS, normal, out var hit))
            {
                var moveDir = hit - currentHit;
                currentHit = hit;
                var orgAxis = Vector3.Zero;
                float scale = 1;
                switch (manipulationType)
                {
                    case ManipulationType.ScaleX:
                        orgAxis = Vector3.UnitX;
                        scale = moveDir.X;
                        break;
                    case ManipulationType.ScaleY:
                        orgAxis = Vector3.UnitY;
                        scale = moveDir.Y;
                        break;
                    case ManipulationType.ScaleZ:
                        orgAxis = Vector3.UnitZ;
                        scale = moveDir.Z;
                        break;
                }
                var axisX = Vector3.TransformNormal(Vector3.UnitX, rotationMatrix);
                var axisY = Vector3.TransformNormal(Vector3.UnitY, rotationMatrix);
                var axisZ = Vector3.TransformNormal(Vector3.UnitZ, rotationMatrix);
                var dotX = Vector3.Dot(axisX, orgAxis);
                var dotY = Vector3.Dot(axisY, orgAxis);
                var dotZ = Vector3.Dot(axisZ, orgAxis);
                scaleMatrix.M11 += scale * Math.Abs(dotX);
                scaleMatrix.M22 += scale * Math.Abs(dotY);
                scaleMatrix.M33 += scale * Math.Abs(dotZ);
                OnUpdateTargetMatrix();
            }
        }
        #endregion

        private void Manipulation_Mouse3DUp(object? sender, MouseUp3DEventArgs? e)
        {
            if (e == null || isCaptured)
            {
                var material = ((e.HitTestResult.ModelHit as MeshGeometryModel3D).Material as DiffuseMaterial);
                material.DiffuseColor = currentColor;
            }
            manipulationType = ManipulationType.None;
            isCaptured = false;
        }

        private void ResetTransforms()
        {
            scaleMatrix = rotationMatrix = targetMatrix = Matrix.Identity;
            translationVector = Vector3.Zero;
            OnUpdateSelfTransform();
        }
        /// <summary>
        /// Called when [target changed]. Use target boundingbox center as Manipulator center
        /// </summary>
        /// <param name="target">The target.</param>
        private void OnTargetChanged()
        {
            if (this.Target == null || this.Target.Element == null)
            {
                this.target = null;
                this.Visibility = Visibility.Collapsed;
                this.ResetTransforms();
            }
            else
            {
                this.target = this.Target.Element;
                this.Visibility = Visibility.Visible;

                this.centerOffset = this.target.SceneNode.BoundsSphere.Center;
                this.sizeScale = this.target.Bounds.Width;

                SceneNode_OnTransformChanged(target.SceneNode, new TransformArgs(target.SceneNode.ModelMatrix));
            }
        }

        private void SceneNode_OnTransformChanged(object sender, TransformArgs e)
        {
            var m = e.Transform;
            m.Decompose(out var scale, out var rotation, out var translation);
            scaleMatrix = Matrix.Scaling(scale);
            rotationMatrix = Matrix.RotationQuaternion(rotation);
            if (centerOffset != Vector3.Zero)
            {
                var org = Matrix.Translation(-centerOffset) * scaleMatrix * rotationMatrix * Matrix.Translation(centerOffset);
                translationVector = translation - org.TranslationVector;
            }
            else
            {
                translationVector = m.TranslationVector;
            }
            OnUpdateSelfTransform();
            //OnUpdateTargetMatrix();
        }

        private void OnUpdateTargetMatrix()
        {
            if (target == null)
            {
                return;
            }
            targetMatrix = Matrix.Translation(-centerOffset) * scaleMatrix * rotationMatrix * Matrix.Translation(centerOffset) * Matrix.Translation(translationVector);
            target.Transform = new System.Windows.Media.Media3D.MatrixTransform3D(targetMatrix.ToMatrix3D());

        }

        private void OnUpdateSelfTransform()
        {
            var m = Matrix.Translation(centerOffset + translationVector);
            m.M11 = m.M22 = m.M33 = (float)sizeScale;

            ctrlGroup.Transform = new System.Windows.Media.Media3D.MatrixTransform3D(m.ToMatrix3D());
        }

        protected override SceneNode OnCreateSceneNode()
        {
            return new AlwaysHitGroupNode(this);
        }

        private sealed class AlwaysHitGroupNode : GroupNode
        {
            private readonly HashSet<object> models = new();
            private readonly DanceTransformManipulator3D manipulator;
            public AlwaysHitGroupNode(DanceTransformManipulator3D manipulator)
            {
                this.manipulator = manipulator;
            }

            protected override bool OnAttach(IEffectsManager effectsManager)
            {
                models.Add(manipulator.translationX);
                models.Add(manipulator.translationY);
                models.Add(manipulator.translationZ);
                models.Add(manipulator.rotationX);
                models.Add(manipulator.rotationY);
                models.Add(manipulator.rotationZ);
                models.Add(manipulator.scaleX);
                models.Add(manipulator.scaleY);
                models.Add(manipulator.scaleZ);
                return base.OnAttach(effectsManager);
            }
            protected override bool OnHitTest(HitTestContext context, Matrix totalModelMatrix, ref List<HitTestResult> hits)
            {
                //Set hit distance to 0 so event manipulator is inside the model, hit test still works
                if (base.OnHitTest(context, totalModelMatrix, ref hits))
                {
                    if (hits.Count > 0)
                    {
                        var res = new HitTestResult() { Distance = float.MaxValue };
                        foreach (var hit in hits)
                        {
                            if (models.Contains(hit.ModelHit))
                            {
                                if (hit.Distance < res.Distance)
                                {
                                    res = hit;
                                }
                            }
                        }
                        res.Distance = 0;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}