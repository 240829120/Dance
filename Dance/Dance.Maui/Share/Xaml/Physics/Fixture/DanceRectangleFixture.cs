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
    /// 矩形碰撞体元素
    /// </summary>
    public class DanceRectangleFixture : DanceFixture
    {
        #region Width -- 宽度

        /// <summary>
        /// 宽度
        /// </summary>
        public float Width
        {
            get => (float)GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }

        /// <summary>
        /// 宽度
        /// </summary>
        public static readonly BindableProperty WidthProperty =
            BindableProperty.Create(nameof(Width), typeof(float), typeof(DanceRectangleFixture), 10f);

        #endregion

        #region Height -- 高度

        /// <summary>
        /// 高度
        /// </summary>
        public float Height
        {
            get => (float)GetValue(HeightProperty);
            set => SetValue(HeightProperty, value);
        }

        /// <summary>
        /// 高度
        /// </summary>
        public static readonly BindableProperty HeightProperty =
            BindableProperty.Create(nameof(Height), typeof(float), typeof(DanceRectangleFixture), 10f);

        #endregion

        /// <summary>
        /// 获取碰撞体
        /// </summary>
        /// <returns>碰撞体</returns>
        public override Fixture GetFixture()
        {
            float left = -this.Width / 2f + this.Position.X;
            float top = -this.Height / 2f + this.Position.Y;
            float right = this.Width / 2f + this.Position.X;
            float bottom = this.Height / 2f + this.Position.Y;

            Vertices vectors =
            [
                new(left, top),
                new(right, top),
                new(right, bottom),
                new(left, bottom)
            ];

            PolygonShape shape = new(vectors, this.Density);
            Fixture fixture = new(shape)
            {
                Friction = this.Friction,
                Restitution = this.Restitution,
            };

            return fixture;
        }
    }
}
