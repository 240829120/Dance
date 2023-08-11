using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
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
            get => (DanceBody)GetValue(BodyAProperty);
            set => SetValue(BodyAProperty, value);
        }

        /// <summary>
        /// 体元素A
        /// </summary>
        public static readonly BindableProperty BodyAProperty =
            BindableProperty.Create(nameof(BodyA), typeof(DanceBody), typeof(DanceLineSegmentJointBase), null);

        #endregion

        #region BodyB -- 体元素B

        /// <summary>
        /// 体元素B
        /// </summary>
        public DanceBody BodyB
        {
            get => (DanceBody)GetValue(BodyBProperty);
            set => SetValue(BodyBProperty, value);
        }

        /// <summary>
        /// 体元素B
        /// </summary>
        public static readonly BindableProperty BodyBProperty =
            BindableProperty.Create(nameof(BodyB), typeof(DanceBody), typeof(DanceLineSegmentJointBase), null);

        #endregion

        #region AnchorA -- 锚定点A

        /// <summary>
        /// 锚定点A
        /// </summary>
        public DancePoint AnchorA
        {
            get => (DancePoint)GetValue(AnchorAProperty);
            set => SetValue(AnchorAProperty, value);
        }

        /// <summary>
        /// 锚定点A
        /// </summary>
        public static readonly BindableProperty AnchorAProperty =
            BindableProperty.Create(nameof(AnchorA), typeof(DancePoint), typeof(DanceLineSegmentJointBase), DancePoint.Zero);

        #endregion

        #region AnchorB -- 锚定点B

        /// <summary>
        /// 锚定点B
        /// </summary>
        public DancePoint AnchorB
        {
            get => (DancePoint)GetValue(AnchorBProperty);
            set => SetValue(AnchorBProperty, value);
        }

        /// <summary>
        /// 锚定点B
        /// </summary>
        public static readonly BindableProperty AnchorBProperty =
            BindableProperty.Create(nameof(AnchorB), typeof(DancePoint), typeof(DanceLineSegmentJointBase), DancePoint.Zero);

        #endregion

    }
}
