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
/// ������ͼ
/// </summary>
public class DanceNavigationView : ItemsControl
{
    /// <summary>
    /// ����������
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

    #region SelectedItem -- ��ǰѡ����

    /// <summary>
    /// ��ǰѡ����
    /// </summary>
    public object? SelectedItem
    {
        get { return (object?)GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }

    /// <summary>
    /// ��ǰѡ����
    /// </summary>
    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register("SelectedItem", typeof(object), typeof(DanceNavigationView), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
        {
            if (s is not DanceNavigationView view)
                return;

            ProviderDic[view.SwitchMode].Switch(view, e.OldValue, e.NewValue);
        })));

    #endregion

    #region SwitchMode -- �л�ģʽ

    /// <summary>
    /// �л�ģʽ
    /// </summary>
    public DanceNavigationSwitchMode SwitchMode
    {
        get { return (DanceNavigationSwitchMode)GetValue(SwitchModeProperty); }
        set { SetValue(SwitchModeProperty, value); }
    }

    /// <summary>
    /// �л�ģʽ
    /// </summary>
    public static readonly DependencyProperty SwitchModeProperty =
        DependencyProperty.Register("SwitchMode", typeof(DanceNavigationSwitchMode), typeof(DanceNavigationView), new PropertyMetadata(DanceNavigationSwitchMode.Opacity));

    #endregion

    #region SwitchDuration -- �л�����ʱ��

    /// <summary>
    /// �л�����ʱ��
    /// </summary>
    public TimeSpan SwitchDuration
    {
        get { return (TimeSpan)GetValue(SwitchDurationProperty); }
        set { SetValue(SwitchDurationProperty, value); }
    }

    /// <summary>
    /// �л�����ʱ��
    /// </summary>
    public static readonly DependencyProperty SwitchDurationProperty =
        DependencyProperty.Register("SwitchDuration", typeof(TimeSpan), typeof(DanceNavigationView), new PropertyMetadata(TimeSpan.FromSeconds(0.7)));

    #endregion

    #region SwitchEasing -- �л����ɺ���

    /// <summary>
    /// �л����ɺ���
    /// </summary>
    public IEasingFunction SwitchEasing
    {
        get { return (IEasingFunction)GetValue(SwitchEasingProperty); }
        set { SetValue(SwitchEasingProperty, value); }
    }

    /// <summary>
    /// �л����ɺ���
    /// </summary>
    public static readonly DependencyProperty SwitchEasingProperty =
        DependencyProperty.Register("SwitchEasing", typeof(IEasingFunction), typeof(DanceNavigationView), new PropertyMetadata(null));

    #endregion

    #region IsShowNavigationBar -- �Ƿ���ʾ������

    /// <summary>
    /// �Ƿ���ʾ������
    /// </summary>
    public bool IsShowNavigationBar
    {
        get { return (bool)GetValue(IsShowNavigationBarProperty); }
        set { SetValue(IsShowNavigationBarProperty, value); }
    }

    /// <summary>
    /// �Ƿ���ʾ������
    /// </summary>
    public static readonly DependencyProperty IsShowNavigationBarProperty =
        DependencyProperty.Register("IsShowNavigationBar", typeof(bool), typeof(DanceNavigationView), new PropertyMetadata(false));

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
    /// ��ɨ�Ƿ�����
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

    // ========================================================================================================
    // Private Function

    /// <summary>
    /// �������
    /// </summary>
    private void DanceNavigationView_Loaded(object sender, RoutedEventArgs e)
    {
        this.Loaded -= DanceNavigationView_Loaded;

        // ��һ�μ���ʱ�л�����ǰѡ����ͼ
        ProviderDic[DanceNavigationSwitchMode.Opacity].Switch(this, null, this.SelectedItem);
    }
}