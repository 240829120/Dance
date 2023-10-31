using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Dance.Wpf
{
    /// <summary>
    /// 热键绑定
    /// </summary>
    public class DanceHotkeyBinding : FrameworkElement
    {
        #region Key -- 键

        /// <summary>
        /// 键
        /// </summary>
        public Keys Key
        {
            get { return (Keys)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        /// <summary>
        /// 键
        /// </summary>
        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.Register("Key", typeof(Keys), typeof(DanceHotkeyBinding), new PropertyMetadata(Keys.None));

        #endregion

        #region Modifiers -- 辅键

        /// <summary>
        /// 辅键
        /// </summary>
        public Keys Modifiers
        {
            get { return (Keys)GetValue(ModifiersProperty); }
            set { SetValue(ModifiersProperty, value); }
        }

        /// <summary>
        /// 辅键
        /// </summary>
        public static readonly DependencyProperty ModifiersProperty =
            DependencyProperty.Register("Modifiers", typeof(Keys), typeof(DanceHotkeyBinding), new PropertyMetadata(Keys.None));

        #endregion

        #region Command -- 命令

        /// <summary>
        /// 命令
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// 命令
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(DanceHotkeyBinding), new PropertyMetadata(null));

        #endregion

        #region CommandParameter -- 命令参数

        /// <summary>
        /// 命令参数
        /// </summary>
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// 命令参数
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(DanceHotkeyBinding), new PropertyMetadata(null));

        #endregion
    }
}
