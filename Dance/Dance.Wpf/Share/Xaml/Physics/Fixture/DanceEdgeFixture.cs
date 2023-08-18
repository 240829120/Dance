using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Dynamics;

namespace Dance.Wpf
{
    /// <summary>
    /// 边元素
    /// </summary>
    public class DanceEdgeFixture : DanceFixture
    {
        #region Start -- 开始点

        /// <summary>
        /// 开始点
        /// </summary>
        public DancePoint Start
        {
            get { return (DancePoint)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        /// <summary>
        /// 开始点
        /// </summary>
        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(DancePoint), typeof(DanceEdgeFixture), new PropertyMetadata(DancePoint.Zero));

        #endregion

        #region End -- 结束点

        /// <summary>
        /// 结束点
        /// </summary>
        public DancePoint End
        {
            get { return (DancePoint)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }

        /// <summary>
        /// 结束点
        /// </summary>
        public static readonly DependencyProperty EndProperty =
            DependencyProperty.Register("End", typeof(DancePoint), typeof(DanceEdgeFixture), new PropertyMetadata(DancePoint.Zero));

        #endregion

        /// <summary>
        /// 获取碰撞体
        /// </summary>
        /// <returns>碰撞体</returns>
        public override Fixture GetFixture()
        {
            EdgeShape shape = new(new(this.Start.X, this.Start.Y), new(this.End.X, this.End.Y));
            Fixture fixture = new(shape)
            {
                Friction = this.Friction,
                Restitution = this.Restitution,
            };

            return fixture;
        }
    }
}
