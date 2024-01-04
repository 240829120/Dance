using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Dance.Wpf;

/// <summary>
/// 导航视图
/// </summary>
public class DanceNavigationView : ItemsControl
{
    /// <summary>
    /// 处理器集合
    /// </summary>
    private readonly static Dictionary<DanceNavigationSwitchMode, DanceNavigationSwitchProviderBase> ProviderDic = [];

    static DanceNavigationView()
    {
        ProviderDic.Add(DanceNavigationSwitchMode.IsVisible, new DanceNavigationSwitchProvider_IsVisible());
        ProviderDic.Add(DanceNavigationSwitchMode.Opacity, new DanceNavigationSwitchProvider_Opacity());
        ProviderDic.Add(DanceNavigationSwitchMode.TranslationX, new DanceNavigationSwitchProvider_TranslationX());
        ProviderDic.Add(DanceNavigationSwitchMode.TranslationX_Opacity, new DanceNavigationSwitchProvider_TranslationX_Opacity());
        ProviderDic.Add(DanceNavigationSwitchMode.TranslationY, new DanceNavigationSwitchProvider_TranslationY());
        ProviderDic.Add(DanceNavigationSwitchMode.TranslationY_Opacity, new DanceNavigationSwitchProvider_TranslationY_Opacity());
        ProviderDic.Add(DanceNavigationSwitchMode.RotationLeftTop, new DanceNavigationSwitchProvider_RotationLeftTop());
        ProviderDic.Add(DanceNavigationSwitchMode.RotationLeftTop_Opacity, new DanceNavigationSwitchProvider_RotationLeftTop_Opacity());
        ProviderDic.Add(DanceNavigationSwitchMode.RotationCenter_Opacity, new DanceNavigationSwitchProvider_RotationCenter_Opacity());

        DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceNavigationView), new FrameworkPropertyMetadata(typeof(DanceNavigationView)));
    }

    public DanceNavigationView()
    {
        this.Loaded += DanceNavigationView_Loaded;
    }

    // ========================================================================================================
    // Property

    #region SelectedItem -- 当前选中项

    /// <summary>
    /// 当前选中项
    /// </summary>
    public object? SelectedItem
    {
        get { return (object?)GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }

    /// <summary>
    /// 当前选中项
    /// </summary>
    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register("SelectedItem", typeof(object), typeof(DanceNavigationView), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
        {
            if (s is not DanceNavigationView view)
                return;

            ProviderDic[view.SwitchMode].Switch(view, e.OldValue, e.NewValue);
        })));

    #endregion

    #region SwitchMode -- 切换模式

    /// <summary>
    /// 切换模式
    /// </summary>
    public DanceNavigationSwitchMode SwitchMode
    {
        get { return (DanceNavigationSwitchMode)GetValue(SwitchModeProperty); }
        set { SetValue(SwitchModeProperty, value); }
    }

    /// <summary>
    /// 切换模式
    /// </summary>
    public static readonly DependencyProperty SwitchModeProperty =
        DependencyProperty.Register("SwitchMode", typeof(DanceNavigationSwitchMode), typeof(DanceNavigationView), new PropertyMetadata(DanceNavigationSwitchMode.Opacity));

    #endregion

    #region SwitchDuration -- 切换持续时间

    /// <summary>
    /// 切换持续时间
    /// </summary>
    public TimeSpan SwitchDuration
    {
        get { return (TimeSpan)GetValue(SwitchDurationProperty); }
        set { SetValue(SwitchDurationProperty, value); }
    }

    /// <summary>
    /// 切换持续时间
    /// </summary>
    public static readonly DependencyProperty SwitchDurationProperty =
        DependencyProperty.Register("SwitchDuration", typeof(TimeSpan), typeof(DanceNavigationView), new PropertyMetadata(TimeSpan.FromSeconds(0.7)));

    #endregion

    #region SwitchEasing -- 切换过渡函数

    /// <summary>
    /// 切换过渡函数
    /// </summary>
    public IEasingFunction SwitchEasing
    {
        get { return (IEasingFunction)GetValue(SwitchEasingProperty); }
        set { SetValue(SwitchEasingProperty, value); }
    }

    /// <summary>
    /// 切换过渡函数
    /// </summary>
    public static readonly DependencyProperty SwitchEasingProperty =
        DependencyProperty.Register("SwitchEasing", typeof(IEasingFunction), typeof(DanceNavigationView), new PropertyMetadata(null));

    #endregion

    #region IsShowNavigationBar -- 是否显示导航条

    /// <summary>
    /// 是否显示导航条
    /// </summary>
    public bool IsShowNavigationBar
    {
        get { return (bool)GetValue(IsShowNavigationBarProperty); }
        set { SetValue(IsShowNavigationBarProperty, value); }
    }

    /// <summary>
    /// 是否显示导航条
    /// </summary>
    public static readonly DependencyProperty IsShowNavigationBarProperty =
        DependencyProperty.Register("IsShowNavigationBar", typeof(bool), typeof(DanceNavigationView), new PropertyMetadata(false));

    #endregion

    #region IsSwipeEnabled -- 轻扫是否启用

    /// <summary>
    /// 轻扫是否启用
    /// </summary>
    public bool IsSwipeEnabled
    {
        get { return (bool)GetValue(IsSwipeEnabledProperty); }
        set { SetValue(IsSwipeEnabledProperty, value); }
    }

    /// <summary>
    /// 轻扫是否启用
    /// </summary>
    public static readonly DependencyProperty IsSwipeEnabledProperty =
        DependencyProperty.Register("IsSwipeEnabled", typeof(bool), typeof(DanceNavigationView), new PropertyMetadata(false));

    #endregion

    // ========================================================================================================
    // Protected Override

    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        return item is DanceNavigationItem;
    }

    protected override DependencyObject GetContainerForItemOverride()
    {
        return new DanceNavigationItem();
    }

    // ========================================================================================================
    // Internal Function

    /// <summary>
    /// 左扫
    /// </summary>
    internal void LeftSwiped()
    {
        ProviderDic[this.SwitchMode].LeftSwiped(this);
    }

    /// <summary>
    /// 右扫
    /// </summary>
    internal void RightSwiped()
    {
        ProviderDic[this.SwitchMode].RightSwiped(this);
    }

    /// <summary>
    /// 上扫
    /// </summary>
    internal void UpSwiped()
    {
        ProviderDic[this.SwitchMode].UpSwiped(this);
    }

    /// <summary>
    /// 下扫
    /// </summary>
    internal void DownSwiped()
    {
        ProviderDic[this.SwitchMode].DownSwiped(this);
    }

    // ========================================================================================================
    // Private Function

    /// <summary>
    /// 加载完成
    /// </summary>
    private void DanceNavigationView_Loaded(object sender, RoutedEventArgs e)
    {
        this.Loaded -= DanceNavigationView_Loaded;

        // 第一次加载时切换至当前选择视图
        ProviderDic[DanceNavigationSwitchMode.Opacity].Switch(this, null, this.SelectedItem);
    }
}