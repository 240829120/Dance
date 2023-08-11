using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 按钮
    /// </summary>
    public class DanceButton : ContentView
    {
        public DanceButton()
        {
            TapGestureRecognizer tap = new();
            tap.Tapped += Tap_Tapped;
            this.GestureRecognizers.Add(tap);
        }

        // ==========================================================================================================
        // Event

        /// <summary>
        /// 点击事件
        /// </summary>
        public event EventHandler<EventArgs>? Clicked;

        // ==========================================================================================================
        // Command

        #region Command -- 点击命令

        /// <summary>
        /// 点击命令
        /// </summary>
        public RelayCommand? Command
        {
            get => (RelayCommand?)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// 点击命令
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(RelayCommand), typeof(DanceButton), null);

        #endregion

        #region CommandParameter -- 命令参数

        /// <summary>
        /// 命令参数
        /// </summary>
        public object? CommandParameter
        {
            get => (object?)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// 命令参数
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(DanceButton), null);

        #endregion

        // ==========================================================================================================
        // Private Function

        /// <summary>
        /// 点击
        /// </summary>
        private void Tap_Tapped(object? sender, TappedEventArgs e)
        {
            this.Clicked?.Invoke(this, e);
            this.Command?.Execute(this.CommandParameter);
        }
    }
}
