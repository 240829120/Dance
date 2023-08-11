using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// String类型动画构建器
    /// </summary>
    public class DanceStringAnimationBuilder : DanceAnimationBuilderProperty<string>
    {
        /// <summary>
        /// 动画构建器项
        /// </summary>
        /// <param name="propertyPath">关联属性</param>
        public DanceStringAnimationBuilder(string propertyPath) : base(propertyPath)
        {

        }

        /// <summary>
        /// 构建
        /// </summary>
        /// <returns>建时间线</returns>
        public override Timeline Build()
        {
            StringAnimationUsingKeyFrames timeline = new();
            timeline.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(this.PropertyPath));

            foreach (var keyFrame in this.KeyFrames)
            {
                timeline.KeyFrames.Add(new DiscreteStringKeyFrame(keyFrame.Value.Value, KeyTime.FromTimeSpan(keyFrame.Key)));
            }

            return timeline;
        }
    }
}
