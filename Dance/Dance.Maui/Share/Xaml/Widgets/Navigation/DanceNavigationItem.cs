using CommunityToolkit.Mvvm.Input;

namespace Dance.Maui;

/// <summary>
/// ��������ͼ
/// </summary>
public class DanceNavigationItem : DanceCollectionItem
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
    /// ��ɨ
    /// </summary>
    private void LeftSwiped()
    {
        if (this.LogicCollectionView is not DanceNavigationView navigation)
            return;

        if (!navigation.IsSwipeEnabled)
            return;

        navigation.LeftSwiped();
    }

    /// <summary>
    /// ��ɨ
    /// </summary>
    private void RightSwiped()
    {
        if (this.LogicCollectionView is not DanceNavigationView navigation)
            return;

        if (!navigation.IsSwipeEnabled)
            return;

        navigation.RightSwiped();
    }

    /// <summary>
    /// ��ɨ
    /// </summary>
    private void UpSwiped()
    {
        if (this.LogicCollectionView is not DanceNavigationView navigation)
            return;

        if (!navigation.IsSwipeEnabled)
            return;

        navigation.UpSwiped();
    }

    /// <summary>
    /// ��ɨ
    /// </summary>
    private void DownSwiped()
    {
        if (this.LogicCollectionView is not DanceNavigationView navigation)
            return;

        if (!navigation.IsSwipeEnabled)
            return;

        navigation.DownSwiped();
    }
}