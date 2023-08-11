using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// CheckBox
    /// </summary>
    public class DanceCheckBox : TemplatedView
    {
        public DanceCheckBox()
        {
            TapGestureRecognizer tap = new();
            tap.Tapped += Tap_Tapped;
            this.GestureRecognizers.Add(tap);
        }

        // ==========================================================================================================
        // Event

        /// <summary>
        /// 勾选状态改变时触发
        /// </summary>
        public event EventHandler<CheckedChangedEventArgs>? CheckedChanged;

        // ==========================================================================================================
        // Property

        #region IsChecked -- 是否勾选

        /// <summary>
        /// 点击命令
        /// </summary>
        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        /// <summary>
        /// 点击命令
        /// </summary>
        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(DanceCheckBox), false, BindingMode.TwoWay);

        #endregion

        #region CheckedContent -- 勾选内容

        /// <summary>
        /// 勾选内容
        /// </summary>
        public View? CheckedContent
        {
            get => (View?)GetValue(CheckedContentProperty);
            set => SetValue(CheckedContentProperty, value);
        }

        /// <summary>
        /// 勾选内容
        /// </summary>
        public static readonly BindableProperty CheckedContentProperty =
            BindableProperty.Create(nameof(CheckedContent), typeof(View), typeof(DanceCheckBox), null);

        #endregion

        #region UnCheckedContent -- 未勾选内容

        /// <summary>
        /// 未勾选内容
        /// </summary>
        public View? UnCheckedContent
        {
            get => (View?)GetValue(UnCheckedContentProperty);
            set => SetValue(UnCheckedContentProperty, value);
        }

        /// <summary>
        /// 未勾选内容
        /// </summary>
        public static readonly BindableProperty UnCheckedContentProperty =
            BindableProperty.Create(nameof(UnCheckedContent), typeof(View), typeof(DanceCheckBox), null);

        #endregion

        #region CheckedChangedCommand -- 切换改变命令

        /// <summary>
        /// 切换改变命令
        /// </summary>
        public RelayCommand<bool>? CheckedChangedCommand
        {
            get => (RelayCommand<bool>?)GetValue(CheckedChangedCommandProperty);
            set => SetValue(CheckedChangedCommandProperty, value);
        }

        /// <summary>
        /// 切换改变命令
        /// </summary>
        public static readonly BindableProperty CheckedChangedCommandProperty =
            BindableProperty.Create(nameof(CheckedChangedCommand), typeof(RelayCommand<bool>), typeof(DanceCheckBox), null);

        #endregion

        // ==========================================================================================================
        // Private Function

        /// <summary>
        /// 点击
        /// </summary>
        private void Tap_Tapped(object? sender, TappedEventArgs e)
        {
            this.IsChecked = !this.IsChecked;
            this.CheckedChanged?.Invoke(this, new CheckedChangedEventArgs(this.IsChecked));
            this.CheckedChangedCommand?.Execute(this.IsChecked);
        }
    }
}
