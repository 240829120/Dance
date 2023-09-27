using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 消息框
    /// </summary>
    public class DanceMessageBox : ContentControl
    {
        static DanceMessageBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceMessageBox), new FrameworkPropertyMetadata(typeof(DanceMessageBox)));
        }

        public DanceMessageBox()
        {
            this.YesCommand = new RelayCommand(this.Yes);
            this.NoCommand = new RelayCommand(this.No);
            this.CancelCommand = new RelayCommand(this.Cancel);
        }

        #region Action -- 行为

        /// <summary>
        /// 行为
        /// </summary>
        public DanceMessageBoxAction Action
        {
            get { return (DanceMessageBoxAction)GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }

        /// <summary>
        /// 行为
        /// </summary>
        public static readonly DependencyProperty ActionProperty =
            DependencyProperty.Register("Action", typeof(DanceMessageBoxAction), typeof(DanceMessageBox), new PropertyMetadata(DanceMessageBoxAction.YES));

        #endregion

        #region Header -- 头部

        /// <summary>
        /// 头部
        /// </summary>
        public object? Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// 头部
        /// </summary>
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(DanceMessageBox), new PropertyMetadata(null));

        #endregion

        #region Icon -- 图标

        /// <summary>
        /// 图标
        /// </summary>
        public ImageSource? Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// 图标
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(DanceMessageBox), new PropertyMetadata(null));

        #endregion

        #region HasIcon -- 是否拥有图标

        /// <summary>
        /// 是否拥有图标
        /// </summary>
        public bool HasIcon
        {
            get { return (bool)GetValue(HasIconProperty); }
            set { SetValue(HasIconPropertyKey, value); }
        }

        /// <summary>
        /// 是否拥有图标
        /// </summary>
        public static readonly DependencyPropertyKey HasIconPropertyKey =
            DependencyProperty.RegisterReadOnly("HasIcon", typeof(bool), typeof(DanceMessageBox), new PropertyMetadata(false));

        /// <summary>
        /// 是否拥有图标
        /// </summary>
        public static readonly DependencyProperty HasIconProperty = HasIconPropertyKey.DependencyProperty;

        #endregion

        #region HasNoButton -- 是否拥有否定按钮

        /// <summary>
        /// 是否拥有否定按钮
        /// </summary>
        public bool HasNoButton
        {
            get { return (bool)GetValue(HasNoButtonProperty); }
            internal set { SetValue(HasNoButtonPropertyKey, value); }
        }

        /// <summary>
        /// 是否拥有否定按钮
        /// </summary>
        public static readonly DependencyPropertyKey HasNoButtonPropertyKey =
            DependencyProperty.RegisterReadOnly("HasNoButton", typeof(bool), typeof(DanceMessageBox), new PropertyMetadata(false));

        /// <summary>
        /// 是否拥有否定按钮
        /// </summary>
        public static readonly DependencyProperty HasNoButtonProperty = HasNoButtonPropertyKey.DependencyProperty;

        #endregion

        #region HasCancelButton -- 是否拥有取消按钮

        /// <summary>
        /// 是否拥有取消按钮
        /// </summary>
        public bool HasCancelButton
        {
            get { return (bool)GetValue(HasCancelButtonProperty); }
            internal set { SetValue(HasCancelButtonPropertyKey, value); }
        }

        /// <summary>
        /// 是否拥有取消按钮
        /// </summary>
        public static readonly DependencyPropertyKey HasCancelButtonPropertyKey =
            DependencyProperty.RegisterReadOnly("HasCancelButton", typeof(bool), typeof(DanceMessageBox), new PropertyMetadata(false));

        /// <summary>
        /// 是否拥有取消按钮
        /// </summary>
        public static readonly DependencyProperty HasCancelButtonProperty = HasCancelButtonPropertyKey.DependencyProperty;

        #endregion

        #region ResultAction -- 返回行为

        /// <summary>
        /// 返回行为
        /// </summary>
        public DanceMessageBoxAction ResultAction
        {
            get { return (DanceMessageBoxAction)GetValue(ResultActionPropertyKey.DependencyProperty); }
            set { SetValue(ResultActionPropertyKey, value); }
        }

        /// <summary>
        /// 返回行为
        /// </summary>
        public static readonly DependencyPropertyKey ResultActionPropertyKey =
            DependencyProperty.RegisterReadOnly("ResultAction", typeof(DanceMessageBoxAction), typeof(DanceMessageBox), new PropertyMetadata(DanceMessageBoxAction.CANCEL));

        #endregion

        #region YesCommand -- 确定命令

        /// <summary>
        /// 确定命令
        /// </summary>
        public RelayCommand YesCommand { get; private set; }

        /// <summary>
        /// 确定
        /// </summary>
        private void Yes()
        {
            this.ResultAction = DanceMessageBoxAction.YES;

            Window.GetWindow(this)?.Close();
        }

        #endregion

        #region NoCommand -- 否定命令

        /// <summary>
        /// 否定命令
        /// </summary>
        public RelayCommand NoCommand { get; private set; }

        /// <summary>
        /// 否定
        /// </summary>
        private void No()
        {
            this.ResultAction = DanceMessageBoxAction.NO;

            Window.GetWindow(this)?.Close();
        }

        #endregion

        #region CancelCommand -- 取消命令

        /// <summary>
        /// 取消命令
        /// </summary>
        public RelayCommand CancelCommand { get; private set; }

        /// <summary>
        /// 取消
        /// </summary>
        private void Cancel()
        {
            this.ResultAction = DanceMessageBoxAction.CANCEL;

            Window.GetWindow(this)?.Close();
        }

        #endregion
    }
}