using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 线段关节
    /// </summary>
    public abstract class DanceLineSegmentJointBase : DanceJoint
    {
        #region BodyA -- 体元素A

        /// <summary>
        /// 体元素A
        /// </summary>
        public DanceBody BodyA
        {
            get { return (DanceBody)GetValue(BodyAProperty); }
            set { SetValue(BodyAProperty, value); }
        }

        /// <summary>
        /// 体元素A
        /// </summary>
        public static readonly DependencyProperty BodyAProperty =
            DependencyProperty.Register("BodyA", typeof(DanceBody), typeof(DanceLineSegmentJointBase), new PropertyMetadata(null));

        #endregion

        #region BodyB -- 体元素B

        /// <summary>
        /// 体元素B
        /// </summary>
        public DanceBody BodyB
        {
            get { return (DanceBody)GetValue(BodyBProperty); }
            set { SetValue(BodyBProperty, value); }
        }

        /// <summary>
        /// 体元素B
        /// </summary>
        public static readonly DependencyProperty BodyBProperty =
            DependencyProperty.Register("BodyB", typeof(DanceBody), typeof(DanceLineSegmentJointBase), new PropertyMetadata(null));

        #endregion

        #region AnchorA -- 锚定点A

        /// <summary>
        /// 锚定点A
        /// </summary>
        public DancePoint AnchorA
        {
            get { return (DancePoint)GetValue(AnchorAProperty); }
            set { SetValue(AnchorAProperty, value); }
        }

        /// <summary>
        /// 锚定点A
        /// </summary>
        public static readonly DependencyProperty AnchorAProperty =
            DependencyProperty.Register("AnchorA", typeof(DancePoint), typeof(DanceLineSegmentJointBase), new PropertyMetadata(DancePoint.Zero));

        #endregion

        #region AnchorB -- 锚定点B

        /// <summary>
        /// 锚定点B
        /// </summary>
        public DancePoint AnchorB
        {
            get { return (DancePoint)GetValue(AnchorBProperty); }
            set { SetValue(AnchorBProperty, value); }
        }

        /// <summary>
        /// 锚定点B
        /// </summary>
        public static readonly DependencyProperty AnchorBProperty =
            DependencyProperty.Register("AnchorB", typeof(DancePoint), typeof(DanceLineSegmentJointBase), new PropertyMetadata(DancePoint.Zero));

        #endregion

    }
}
