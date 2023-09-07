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
/// ������ͼ
/// </summary>
public class DanceNavigationView : DanceCollectionView
{
    /// <summary>
    /// ����������
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

    #region SwitchMode -- �л�ģʽ

    /// <summary>
    /// �л�ģʽ
    /// </summary>
    public DanceNavigationSwitchMode SwitchMode
    {
        get => (DanceNavigationSwitchMode)GetValue(SwitchModeProperty);
        set => SetValue(SwitchModeProperty, value);
    }

    /// <summary>
    /// �л�ģʽ
    /// </summary>
    public static readonly BindableProperty SwitchModeProperty =
        BindableProperty.Create(nameof(SwitchMode), typeof(DanceNavigationSwitchMode), typeof(DanceNavigationView), DanceNavigationSwitchMode.Opacity);

    #endregion

    #region SwitchDuration -- �л�����ʱ��

    /// <summary>
    /// �л�����ʱ��
    /// </summary>
    public TimeSpan SwitchDuration
    {
        get => (TimeSpan)GetValue(SwitchDurationProperty);
        set => SetValue(SwitchDurationProperty, value);
    }

    /// <summary>
    /// �л�����ʱ��
    /// </summary>
    public static readonly BindableProperty SwitchDurationProperty =
        BindableProperty.Create(nameof(SwitchMode), typeof(TimeSpan), typeof(DanceNavigationView), TimeSpan.FromSeconds(0.7));

    #endregion

    #region SwitchEasing -- �л����ɺ���

    /// <summary>
    /// �л����ɺ���
    /// </summary>
    public Easing SwitchEasing
    {
        get => (Easing)GetValue(SwitchEasingProperty);
        set => SetValue(SwitchEasingProperty, value);
    }

    /// <summary>
    /// �л����ɺ���
    /// </summary>
    public static readonly BindableProperty SwitchEasingProperty =
        BindableProperty.Create(nameof(SwitchMode), typeof(Easing), typeof(DanceNavigationView), Easing.CubicInOut);

    #endregion

    #region IsShowNavigationBar -- �Ƿ���ʾ������

    /// <summary>
    /// �Ƿ���ʾ������
    /// </summary>
    public bool IsShowNavigationBar
    {
        get => (bool)GetValue(IsShowNavigationBarProperty);
        set => SetValue(IsShowNavigationBarProperty, value);
    }

    /// <summary>
    /// �Ƿ���ʾ������
    /// </summary>
    public static readonly BindableProperty IsShowNavigationBarProperty =
        BindableProperty.Create(nameof(IsShowNavigationBar), typeof(bool), typeof(DanceNavigationView), false);

    #endregion

    #region IsSwipeEnabled -- ��ɨ�Ƿ�����

    /// <summary>
    /// ��ɨ�Ƿ�����
    /// </summary>
    public bool IsSwipeEnabled
    {
        get { return (bool)GetValue(IsSwipeEnabledProperty); }
        set { SetValue(IsSwipeEnabledProperty, value); }
    }

    /// <summary>
    /// �Ƿ���ʾ������
    /// </summary>
    public static readonly BindableProperty IsSwipeEnabledProperty =
        BindableProperty.Create(nameof(IsSwipeEnabled), typeof(bool), typeof(DanceNavigationView), false);

    #endregion

    // ========================================================================================================
    // Override

    /// <summary>
    /// ��ѡ������ı�ʱ����
    /// </summary>
    /// <param name="oldValue">��ֵ</param>
    /// <param name="newValue">��ֵ</param>
    protected override void OnSelectedItemChagned(object oldValue, object newValue)
    {
        ProviderDic[this.SwitchMode].Switch(this, oldValue, newValue);
    }

    // ========================================================================================================
    // Internal Function

    /// <summary>
    /// ��ɨ
    /// </summary>
    internal void LeftSwiped()
    {
        ProviderDic[this.SwitchMode].LeftSwiped(this);
    }

    /// <summary>
    /// ��ɨ
    /// </summary>
    internal void RightSwiped()
    {
        ProviderDic[this.SwitchMode].RightSwiped(this);
    }

    /// <summary>
    /// ��ɨ
    /// </summary>
    internal void UpSwiped()
    {
        ProviderDic[this.SwitchMode].UpSwiped(this);
    }

    /// <summary>
    /// ��ɨ
    /// </summary>
    internal void DownSwiped()
    {
        ProviderDic[this.SwitchMode].DownSwiped(this);
    }

}