using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Dynamics;

namespace Dance.Maui
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
            get => (DancePoint)GetValue(StartProperty);
            set => SetValue(StartProperty, value);
        }

        /// <summary>
        /// 开始点
        /// </summary>
        public static readonly BindableProperty StartProperty =
            BindableProperty.Create(nameof(Start), typeof(DancePoint), typeof(DanceCircleFixture), DancePoint.Zero);

        #endregion

        #region End -- 结束点

        /// <summary>
        /// 结束点
        /// </summary>
        public DancePoint End
        {
            get => (DancePoint)GetValue(EndProperty);
            set => SetValue(EndProperty, value);
        }

        /// <summary>
        /// 结束点
        /// </summary>
        public static readonly BindableProperty EndProperty =
            BindableProperty.Create(nameof(End), typeof(DancePoint), typeof(DanceCircleFixture), DancePoint.Zero);

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
