using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 视频预览
    /// </summary>
    public class DanceVideoPreview : DancePreviewBase
    {
        public DanceVideoPreview()
        {
            this.PlayCommand = new RelayCommand(this.Play);
            this.PauseCommand = new RelayCommand(this.Pause);
            this.StopCommand = new RelayCommand(this.Stop);
            this.PositionDragStartedCommand = new RelayCommand(this.PositionDragStarted);
            this.PositionDragCompletedCommand = new RelayCommand(this.PositionDragCompleted);
        }

        // ==========================================================================================================
        // Field

        /// <summary>
        /// 播放器
        /// </summary>
        private MediaElement? PART_MediaElement;

        /// <summary>
        /// 位置是否开始拖拽
        /// </summary>
        private bool IsPositionDragStarted;

        // ==========================================================================================================
        // Property

        #region PlayCommand -- 播放命令

        /// <summary>
        /// 播放命令
        /// </summary>
        public RelayCommand? PlayCommand
        {
            get => (RelayCommand?)GetValue(PlayCommandProperty);
            set => SetValue(PlayCommandProperty, value);
        }

        /// <summary>
        /// 播放命令
        /// </summary>
        public static readonly BindableProperty PlayCommandProperty =
            BindableProperty.Create(nameof(PlayCommand), typeof(RelayCommand), typeof(DanceVideoPreview), null);

        #endregion

        #region PauseCommand -- 暂停命令

        /// <summary>
        /// 暂停命令
        /// </summary>
        public RelayCommand? PauseCommand
        {
            get => (RelayCommand?)GetValue(PauseCommandProperty);
            set => SetValue(PauseCommandProperty, value);
        }

        /// <summary>
        /// 暂停命令
        /// </summary>
        public static readonly BindableProperty PauseCommandProperty =
            BindableProperty.Create(nameof(PauseCommand), typeof(RelayCommand), typeof(DanceVideoPreview), null);

        #endregion

        #region StopCommand -- 停止命令

        /// <summary>
        /// 暂停命令
        /// </summary>
        public RelayCommand? StopCommand
        {
            get => (RelayCommand?)GetValue(StopCommandProperty);
            set => SetValue(StopCommandProperty, value);
        }

        /// <summary>
        /// 暂停命令
        /// </summary>
        public static readonly BindableProperty StopCommandProperty =
            BindableProperty.Create(nameof(StopCommand), typeof(RelayCommand), typeof(DanceVideoPreview), null);

        #endregion

        #region Volume -- 音量

        /// <summary>
        /// 音量
        /// </summary>
        public double Volume
        {
            get => (double)GetValue(VolumeProperty);
            set => SetValue(VolumeProperty, value);
        }

        /// <summary>
        /// 音量
        /// </summary>
        public static readonly BindableProperty VolumeProperty =
            BindableProperty.Create(nameof(Volume), typeof(double), typeof(DanceVideoPreview), 1d);

        #endregion

        #region Speed -- 速度

        /// <summary>
        /// 速度
        /// </summary>
        public double Speed
        {
            get => (double)GetValue(SpeedProperty);
            set => SetValue(SpeedProperty, value);
        }

        /// <summary>
        /// 速度
        /// </summary>
        public static readonly BindableProperty SpeedProperty =
            BindableProperty.Create(nameof(Speed), typeof(double), typeof(DanceVideoPreview), 1d);

        #endregion

        #region Position -- 位置

        /// <summary>
        /// 位置
        /// </summary>
        public TimeSpan Position
        {
            get => (TimeSpan)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        /// <summary>
        /// 位置
        /// </summary>
        public static readonly BindableProperty PositionProperty =
            BindableProperty.Create(nameof(Position), typeof(TimeSpan), typeof(DanceVideoPreview), TimeSpan.Zero);

        #endregion

        #region Duration -- 长度

        /// <summary>
        /// 长度
        /// </summary>
        public TimeSpan Duration
        {
            get => (TimeSpan)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        /// <summary>
        /// 长度
        /// </summary>
        public static readonly BindableProperty DurationProperty =
            BindableProperty.Create(nameof(Duration), typeof(TimeSpan), typeof(DanceVideoPreview), TimeSpan.Zero);

        #endregion

        #region MediaState -- 播放器状态

        /// <summary>
        /// 播放器状态
        /// </summary>
        public MediaElementState MediaState
        {
            get => (MediaElementState)GetValue(MediaStateProperty);
            set => SetValue(MediaStateProperty, value);
        }

        /// <summary>
        /// 播放器状态
        /// </summary>
        public static readonly BindableProperty MediaStateProperty =
            BindableProperty.Create(nameof(MediaState), typeof(MediaElementState), typeof(DanceVideoPreview), MediaElementState.None);

        #endregion

        #region ShouldAutoPlay -- 是否自动播放

        /// <summary>
        /// 是否自动播放
        /// </summary>
        public bool ShouldAutoPlay
        {
            get => (bool)GetValue(ShouldAutoPlayProperty);
            set => SetValue(ShouldAutoPlayProperty, value);
        }

        /// <summary>
        /// 是否自动播放
        /// </summary>
        public static readonly BindableProperty ShouldAutoPlayProperty =
            BindableProperty.Create(nameof(ShouldAutoPlay), typeof(bool), typeof(DanceVideoPreview), false);

        #endregion

        #region PlaybackMode -- 播放面板模式

        /// <summary>
        /// 播放面板模式
        /// </summary>
        public DanceVideoPreviewPlaybackMode PlaybackMode
        {
            get => (DanceVideoPreviewPlaybackMode)GetValue(PlaybackModeProperty);
            set => SetValue(PlaybackModeProperty, value);
        }

        /// <summary>
        /// 播放面板模式
        /// </summary>
        public static readonly BindableProperty PlaybackModeProperty =
            BindableProperty.Create(nameof(PlaybackMode), typeof(DanceVideoPreviewPlaybackMode), typeof(DanceVideoPreview), DanceVideoPreviewPlaybackMode.Easy);

        #endregion

        #region IsMute -- 是否静音

        /// <summary>
        /// 是否静音
        /// </summary>
        public bool IsMute
        {
            get => (bool)GetValue(IsMuteProperty);
            set => SetValue(IsMuteProperty, value);
        }

        /// <summary>
        /// 是否静音
        /// </summary>
        public static readonly BindableProperty IsMuteProperty =
            BindableProperty.Create(nameof(IsMute), typeof(bool), typeof(DanceVideoPreview), false);

        #endregion

        // ==========================================================================================================
        // Command

        #region PositionDragStartedCommand -- 位置拖拽开始命令

        /// <summary>
        /// 位置拖拽开始命令
        /// </summary>
        public RelayCommand? PositionDragStartedCommand
        {
            get => (RelayCommand?)GetValue(PositionDragStartedCommandProperty);
            set => SetValue(PositionDragStartedCommandProperty, value);
        }

        /// <summary>
        /// 位置拖拽开始命令
        /// </summary>
        public static readonly BindableProperty PositionDragStartedCommandProperty =
            BindableProperty.Create(nameof(PositionDragStartedCommand), typeof(RelayCommand), typeof(DanceVideoPreview), null);

        #endregion

        #region PositionDragCompletedCommand -- 位置拖拽结束命令

        /// <summary>
        /// 位置拖拽结束命令
        /// </summary>
        public RelayCommand? PositionDragCompletedCommand
        {
            get => (RelayCommand?)GetValue(PositionDragCompletedCommandProperty);
            set => SetValue(PositionDragCompletedCommandProperty, value);
        }

        /// <summary>
        /// 位置拖拽结束命令
        /// </summary>
        public static readonly BindableProperty PositionDragCompletedCommandProperty =
            BindableProperty.Create(nameof(PositionDragCompletedCommand), typeof(RelayCommand), typeof(DanceVideoPreview), null);

        #endregion

        // ==========================================================================================================
        // Override

        /// <summary>
        /// 应用模板
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_MediaElement = this.GetTemplateChild(nameof(PART_MediaElement)) as MediaElement;
            if (this.PART_MediaElement == null)
                return;

            this.PART_MediaElement.PositionChanged -= PART_MediaElement_PositionChanged;
            this.PART_MediaElement.PositionChanged += PART_MediaElement_PositionChanged;
        }

        // ==========================================================================================================
        // Public Function

        /// <summary>
        /// 播放
        /// </summary>
        public void Play()
        {
            if (this.PART_MediaElement == null)
                return;

            this.PART_MediaElement.Play();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            if (this.PART_MediaElement == null)
                return;

            this.PART_MediaElement.Pause();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            if (this.PART_MediaElement == null)
                return;

            this.PART_MediaElement.Stop();
        }

        // ==========================================================================================================
        // Private Function

        /// <summary>
        /// 位置拖拽开始
        /// </summary>
        private void PositionDragStarted()
        {
            this.IsPositionDragStarted = true;
        }

        /// <summary>
        /// 位置拖拽结束
        /// </summary>
        private void PositionDragCompleted()
        {
            this.PART_MediaElement?.SeekTo(this.Position);
            this.IsPositionDragStarted = false;
        }

        /// <summary>
        /// 位置值改变时触发
        /// </summary>
        private void PART_MediaElement_PositionChanged(object? sender, MediaPositionChangedEventArgs e)
        {
            if (this.IsPositionDragStarted)
                return;

            this.Position = e.Position;
        }
    }
}