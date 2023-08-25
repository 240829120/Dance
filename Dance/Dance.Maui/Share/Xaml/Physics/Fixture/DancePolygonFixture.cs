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
    /// 多边形碰撞体
    /// </summary>
    public class DancePolygonFixture : DanceFixture
    {
        #region Points -- 点集合

        /// <summary>
        /// 点集合
        /// </summary>
        public DancePointCollection Points
        {
            get => (DancePointCollection)GetValue(PointsProperty);
            set => SetValue(PointsProperty, value);
        }

        /// <summary>
        /// 点集合
        /// </summary>
        public static readonly BindableProperty PointsProperty =
            BindableProperty.Create(nameof(Points), typeof(DancePointCollection), typeof(DancePolygonFixture), defaultValueCreator: b => new DancePointCollection());

        #endregion

        /// <summary>
        /// 获取碰撞体
        /// </summary>
        /// <returns>碰撞体</returns>
        public override Fixture GetFixture()
        {
            if (this.Points.Count == 0)
                throw new InvalidOperationException("PolygonFixtureElement points not be null");

            Vertices vectors = new();
            foreach (DancePoint point in this.Points)
            {
                vectors.Add(new Vector2(point.X, point.Y));
            }

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
