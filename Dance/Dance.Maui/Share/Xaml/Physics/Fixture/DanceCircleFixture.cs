using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nkast.Aether.Physics2D.Collision.Shapes;
using nkast.Aether.Physics2D.Common;
using nkast.Aether.Physics2D.Dynamics;

namespace Dance.Maui
{
    /// <summary>
    /// 圆形碰撞体
    /// </summary>
    public class DanceCircleFixture : DanceFixture
    {
        #region Radius -- 半径

        /// <summary>
        /// 半径
        /// </summary>
        public float Radius
        {
            get => (float)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        /// <summary>
        /// 半径
        /// </summary>
        public static readonly BindableProperty RadiusProperty =
            BindableProperty.Create(nameof(Radius), typeof(float), typeof(DanceCircleFixture), 5f);

        #endregion

        /// <summary>
        /// 获取碰撞体
        /// </summary>
        /// <returns>碰撞体</returns>
        public override Fixture GetFixture()
        {
            CircleShape shape = new(this.Radius, this.Density)
            {
                Position = new Vector2(this.Position.X, this.Position.Y)
            };
            Fixture fixture = new(shape)
            {
                Friction = this.Friction,
                Restitution = this.Restitution,
            };

            return fixture;
        }
    }
}
