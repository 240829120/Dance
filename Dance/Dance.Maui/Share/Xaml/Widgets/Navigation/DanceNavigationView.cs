using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Converters;
using Microsoft.Maui.Platform;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace Dance.Maui;

/// <summary>
/// 导航视图
/// </summary>
public class DanceNavigationView : DanceCollectionView
{
    /// <summary>
    /// 处理器集合
    /// </summary>
    private readonly static Dictionary<DanceNavigationSwitchMode, DanceNavigationSwitchProviderBase> ProviderDic = new();

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
    }

    // ========================================================================================================
    // Property

    #region SwitchMode -- 切换模式

    /// <summary>
    /// 切换模式
    /// </summary>
    public DanceNavigationSwitchMode SwitchMode
    {
        get => (DanceNavigationSwitchMode)GetValue(SwitchModeProperty);
        set => SetValue(SwitchModeProperty, value);
    }

    /// <summary>
    /// 切换模式
    /// </summary>
    public static readonly BindableProperty SwitchModeProperty =
        BindableProperty.Create(nameof(SwitchMode), typeof(DanceNavigationSwitchMode), typeof(DanceNavigationView), DanceNavigationSwitchMode.Opacity);

    #endregion

    #region SwitchDuration -- 切换持续时间

    /// <summary>
    /// 切换持续时间
    /// </summary>
    public TimeSpan SwitchDuration
    {
        get => (TimeSpan)GetValue(SwitchDurationProperty);
        set => SetValue(SwitchDurationProperty, value);
    }

    /// <summary>
    /// 切换持续时间
    /// </summary>
    public static readonly BindableProperty SwitchDurationProperty =
        BindableProperty.Create(nameof(SwitchMode), typeof(TimeSpan), typeof(DanceNavigationView), TimeSpan.FromSeconds(0.7));

    #endregion

    #region SwitchEasing -- 切换过渡函数

    /// <summary>
    /// 切换过渡函数
    /// </summary>
    public Easing SwitchEasing
    {
        get => (Easing)GetValue(SwitchEasingProperty);
        set => SetValue(SwitchEasingProperty, value);
    }

    /// <summary>
    /// 切换过渡函数
    /// </summary>
    public static readonly BindableProperty SwitchEasingProperty =
        BindableProperty.Create(nameof(SwitchMode), typeof(Easing), typeof(DanceNavigationView), Easing.CubicInOut);

    #endregion

    #region IsShowNavigationBar -- 是否显示导航条

    /// <summary>
    /// 是否显示导航条
    /// </summary>
    public bool IsShowNavigationBar
    {
        get => (bool)GetValue(IsShowNavigationBarProperty);
        set => SetValue(IsShowNavigationBarProperty, value);
    }

    /// <summary>
    /// 是否显示导航条
    /// </summary>
    public static readonly BindableProperty IsShowNavigationBarProperty =
        BindableProperty.Create(nameof(IsShowNavigationBar), typeof(bool), typeof(DanceNavigationView), false);

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
    /// 是否显示导航条
    /// </summary>
    public static readonly BindableProperty IsSwipeEnabledProperty =
        BindableProperty.Create(nameof(IsSwipeEnabled), typeof(bool), typeof(DanceNavigationView), false);

    #endregion

    // ========================================================================================================
    // Override

    /// <summary>
    /// 当选中项发生改变时触发
    /// </summary>
    /// <param name="oldValue">旧值</param>
    /// <param name="newValue">新值</param>
    protected override void OnSelectedItemChagned(object oldValue, object newValue)
    {
        ProviderDic[this.SwitchMode].Switch(this, oldValue, newValue);
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

}