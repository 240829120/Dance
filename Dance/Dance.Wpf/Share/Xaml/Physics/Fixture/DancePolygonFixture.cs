﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using nkast.Aether.Physics2D.Collision.Shapes;
using nkast.Aether.Physics2D.Common;
using nkast.Aether.Physics2D.Dynamics;

namespace Dance.Wpf
{
    /// <summary>
    /// 多边形碰撞体
    /// </summary>
    public class DancePolygonFixture : DanceFixture
    {
        public DancePolygonFixture()
        {
            this.Points = [];
        }

        #region Points -- 点集合

        /// <summary>
        /// 点集合
        /// </summary>
        public DancePointCollection Points
        {
            get { return (DancePointCollection)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }

        /// <summary>
        /// 点集合
        /// </summary>
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(DancePointCollection), typeof(DancePolygonFixture), new PropertyMetadata(null));

        #endregion

        /// <summary>
        /// 获取碰撞体
        /// </summary>
        /// <returns>碰撞体</returns>
        public override Fixture GetFixture()
        {
            if (this.Points.Count == 0)
                throw new InvalidOperationException("PolygonFixtureElement points not be null");

            Vertices vectors = [];
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
