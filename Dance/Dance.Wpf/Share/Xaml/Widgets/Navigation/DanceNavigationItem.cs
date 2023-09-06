using System;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Wpf;

/// <summary>
/// ��������ͼ
/// </summary>
public class DanceNavigationItem : ContentControl
{
    static DanceNavigationItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceNavigationItem), new FrameworkPropertyMetadata(typeof(DanceNavigationItem)));
    }

    public DanceNavigationItem()
    {
        DanceSwipeGestureRecognizer swipe = new();
        swipe.Swiped += Swipe_Swiped;

        DanceGestureRecognizer.SetSwipe(this, swipe);
    }

    // ========================================================================================================
    // Private Function

    private void Swipe_Swiped(object sender, DanceSwipeGestureRecognizerEventArgs e)
    {
        switch (e.Direction)
        {
            case DanceSwipeGestureRecognizerDirection.Up: this.UpSwiped(); break;
            case DanceSwipeGestureRecognizerDirection.Down: this.DownSwiped(); break;
            case DanceSwipeGestureRecognizerDirection.Left: this.LeftSwiped(); break;
            case DanceSwipeGestureRecognizerDirection.Right: this.RightSwiped(); break;
            default: break;
        }
    }

    /// <summary>
    /// ��ɨ
    /// </summary>
    private void LeftSwiped()
    {
        if (DanceXamlExpansion.GetVisualTreeParent<DanceNavigationView>(this) is not DanceNavigationView navigation)
            return;

        if (!navigation.IsSwipeGestureEnabled)
            return;

        navigation.LeftSwiped();
    }

    /// <summary>
    /// ��ɨ
    /// </summary>
    private void RightSwiped()
    {
        if (DanceXamlExpansion.GetVisualTreeParent<DanceNavigationView>(this) is not DanceNavigationView navigation)
            return;

        if (!navigation.IsSwipeGestureEnabled)
            return;

        navigation.RightSwiped();
    }

    /// <summary>
    /// ��ɨ
    /// </summary>
    private void UpSwiped()
    {
        if (DanceXamlExpansion.GetVisualTreeParent<DanceNavigationView>(this) is not DanceNavigationView navigation)
            return;

        if (!navigation.IsSwipeGestureEnabled)
            return;

        navigation.UpSwiped();
    }

    /// <summary>
    /// ��ɨ
    /// </summary>
    private void DownSwiped()
    {
        if (DanceXamlExpansion.GetVisualTreeParent<DanceNavigationView>(this) is not DanceNavigationView navigation)
            return;

        if (!navigation.IsSwipeGestureEnabled)
            return;

        navigation.DownSwiped();
    }
}