using HelixToolkit.Wpf.SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Dance.Wpf
{
    /// <summary>
    /// 三维变换
    /// </summary>
    public class DanceTransform3D : DanceModel
    {
        // ===================================================================================
        // Property

        #region TranslationX -- X偏移量

        private float translationX;
        /// <summary>
        /// X偏移量
        /// </summary>
        public float TranslationX
        {
            get { return translationX; }
            set { translationX = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region TranslationY -- Y偏移量

        private float translationY;
        /// <summary>
        /// Y偏移量
        /// </summary>
        public float TranslationY
        {
            get { return translationY; }
            set { translationY = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region TranslationZ -- Z偏移量

        private float translationZ;
        /// <summary>
        /// Z偏移量
        /// </summary>
        public float TranslationZ
        {
            get { return translationZ; }
            set { translationZ = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region CenterX -- X中心

        private float centerX;
        /// <summary>
        /// X中心
        /// </summary>
        public float CenterX
        {
            get { return centerX; }
            set { centerX = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region CenterY -- Y中心

        private float centerY;
        /// <summary>
        /// Y中心
        /// </summary>
        public float CenterY
        {
            get { return centerY; }
            set { centerY = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region CenterZ -- Z中心

        private float centerZ;
        /// <summary>
        /// Z中心
        /// </summary>
        public float CenterZ
        {
            get { return centerZ; }
            set { centerZ = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region RotationX -- X旋转弧度

        private float rotationX;
        /// <summary>
        /// X旋转弧度
        /// </summary>
        public float RotationX
        {
            get { return rotationX; }
            set { rotationX = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region RotationY -- Y旋转弧度

        private float rotationY;
        /// <summary>
        /// Y旋转弧度
        /// </summary>
        public float RotationY
        {
            get { return rotationY; }
            set { rotationY = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region RotationZ -- Z旋转弧度

        private float rotationZ;
        /// <summary>
        /// Z旋转弧度
        /// </summary>
        public float RotationZ
        {
            get { return rotationZ; }
            set { rotationZ = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region ScaleX -- X缩放

        private float scaleX = 1f;
        /// <summary>
        /// X缩放
        /// </summary>
        public float ScaleX
        {
            get { return scaleX; }
            set { scaleX = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region ScaleY -- Y缩放

        private float scaleY = 1f;
        /// <summary>
        /// Y缩放
        /// </summary>
        public float ScaleY
        {
            get { return scaleY; }
            set { scaleY = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region ScaleZ -- Z缩放

        private float scaleZ = 1f;
        /// <summary>
        /// Z缩放
        /// </summary>
        public float ScaleZ
        {
            get { return scaleZ; }
            set { scaleZ = value; this.OnPropertyChanged(); }
        }

        #endregion

        /// <summary>
        /// 转化为矩阵变换
        /// </summary>
        /// <returns>矩阵变换</returns>
        public MatrixTransform3D ToMatrixTransform3D()
        {
            SharpDX.Matrix translation = SharpDX.Matrix.Translation(this.TranslationX, this.TranslationY, this.TranslationZ);
            SharpDX.Vector3 center = new(this.CenterX, this.CenterY, this.CenterZ);
            SharpDX.Matrix scale = SharpDX.Matrix.Scaling(this.ScaleX, this.ScaleY, this.ScaleZ);
            SharpDX.Matrix rotation = SharpDX.Matrix.RotationX(this.RotationX) * SharpDX.Matrix.RotationY(this.RotationY) * SharpDX.Matrix.RotationZ(this.RotationZ);

            SharpDX.Matrix transform = SharpDX.Matrix.Translation(-center) * scale * rotation * SharpDX.Matrix.Translation(center) * translation;

            return new(transform.ToMatrix3D());
        }
    }
}
