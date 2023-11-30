using HelixToolkit.SharpDX.Core.Model.Scene;
using HelixToolkit.Wpf.SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 场景节点分组
    /// </summary>
    public class DanceSceneRootNodeModel3D : Element3D
    {
        public DanceSceneRootNodeModel3D()
        {
            this.GroupNode = new() { Tag = this };
        }

        // =======================================================================================================
        // Field

        /// <summary>
        /// 分组节点
        /// </summary>
        private readonly GroupNode GroupNode;

        // =======================================================================================================
        // Property

        #region Root -- 根

        /// <summary>
        /// 根
        /// </summary>
        public SceneNode Root
        {
            get { return (SceneNode)GetValue(RootProperty); }
            set { SetValue(RootProperty, value); }
        }

        /// <summary>
        /// 根
        /// </summary>
        public static readonly DependencyProperty RootProperty =
            DependencyProperty.Register("Root", typeof(SceneNode), typeof(DanceSceneRootNodeModel3D), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceSceneRootNodeModel3D element)
                    return;

                element.GroupNode.Clear();

                if (e.NewValue is not SceneNode node)
                    return;

                element.GroupNode.AddChildNode(node);

            })));

        #endregion

        /// <summary>
        /// 创建节点时
        /// </summary>
        /// <returns></returns>
        protected override SceneNode OnCreateSceneNode()
        {
            return this.GroupNode;
        }
    }
}
