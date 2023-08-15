using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;

namespace Dance.Wpf;

/// <summary>
/// 导航项视图
/// </summary>
public class DanceNavigationItem : ContentControl
{
    public DanceNavigationItem()
    {
        this.GestureRecognizers.Add(new SwipeGestureRecognizer { Direction = SwipeDirection.Left, Command = new RelayCommand(this.LeftSwiped) });
        this.GestureRecognizers.Add(new SwipeGestureRecognizer { Direction = SwipeDirection.Right, Command = new RelayCommand(this.RightSwiped) });
        this.GestureRecognizers.Add(new SwipeGestureRecognizer { Direction = SwipeDirection.Up, Command = new RelayCommand(this.UpSwiped) });
        this.GestureRecognizers.Add(new SwipeGestureRecognizer { Direction = SwipeDirection.Down, Command = new RelayCommand(this.DownSwiped) });
    }

    // ========================================================================================================
    // Private Function

    /// <summary>
    /// 左扫
    /// </summary>
    private void LeftSwiped()
    {
        if (this.LogicCollectionView is not DanceNavigationView navigation)
            return;

        navigation.LeftSwiped();
    }

    /// <summary>
    /// 右扫
    /// </summary>
    private void RightSwiped()
    {
        if (this.LogicCollectionView is not DanceNavigationView navigation)
            return;

        navigation.RightSwiped();
    }

    /// <summary>
    /// 上扫
    /// </summary>
    private void UpSwiped()
    {
        if (this.LogicCollectionView is not DanceNavigationView navigation)
            return;

        navigation.UpSwiped();
    }

    /// <summary>
    /// 下扫
    /// </summary>
    private void DownSwiped()
    {
        if (this.LogicCollectionView is not DanceNavigationView navigation)
            return;

        navigation.DownSwiped();
    }
}