using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// Color类型动画构建器
    /// </summary>
    public class DanceColorAnimationBuilder : DanceAnimationBuilderProperty<Color>
    {
        /// <summary>
        /// 动画构建器项
        /// </summary>
        /// <param name="propertyPath">关联属性</param>
        public DanceColorAnimationBuilder(string propertyPath) : base(propertyPath)
        {

        }

        /// <summary>
        /// 构建
        /// </summary>
        /// <returns>建时间线</returns>
        public override Timeline Build()
        {
            ColorAnimationUsingKeyFrames timeline = new();
            timeline.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(this.PropertyPath));

            foreach (var keyFrame in this.KeyFrames)
            {
                timeline.KeyFrames.Add(new EasingColorKeyFrame(keyFrame.Value.Value, KeyTime.FromTimeSpan(keyFrame.Key), this.Easing));
            }

            return timeline;
        }
    }
}
