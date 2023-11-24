using Dance.Common;
using Dance.Wpf;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Collections;
using Dance;

namespace Dance.WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
        }

        private List<TimelineTrackModel> list = new();

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //this.propertyGrid.SelectedObject = new Student();

            Random random = new();

            for (int t = 0; t < 20; t++)
            {
                TimelineTrackModel track = new() { Name = $"轨道{t}" };

                TimeSpan beginTime = TimeSpan.FromSeconds(random.Next(0, (int)TimeSpan.FromMinutes(5).TotalSeconds));

                for (int i = 0; i < 100; ++i)
                {
                    TimeSpan endTime = TimeSpan.FromSeconds(random.Next((int)beginTime.TotalSeconds, (int)(beginTime + TimeSpan.FromMinutes(5)).TotalSeconds));

                    if (beginTime >= this.timeline.Duration || endTime >= this.timeline.Duration)
                        break;

                    TimelineTrackItemModel item = new()
                    {
                        BeginTime = beginTime,
                        EndTime = endTime,
                        Name = $"{t}_{i}"
                    };

                    track.Items.Add(item);

                    beginTime = TimeSpan.FromSeconds(random.Next((int)endTime.TotalSeconds, (int)(beginTime + TimeSpan.FromMinutes(5)).TotalSeconds)); ;
                }

                list.Add(track);
            }

            this.timeline.ItemsSource = list;
        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            DanceDomain.Current?.Dispose();
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.timeline.IsPlaying = !this.timeline.IsPlaying;
        }

        private void timeline_ElementDragBegin(object sender, DanceTimelineElementDragBeginEventArgs e)
        {
            if (e.Element.DataContext is not TimelineTrackItemModel model)
                return;

            DragDrop.DoDragDrop(e.Timeline, model, DragDropEffects.Copy);
        }

        private void timeline_ElementDragOver(object sender, DanceTimelineElementDragEventArgs e)
        {
            if (e.EventArgs.Data.GetData(typeof(TimelineTrackItemModel)) is not TimelineTrackItemModel model)
                return;

            e.BeginTime = model.BeginTime;
            e.EndTime = model.EndTime;
        }

        private void timeline_ElementDrop(object sender, DanceTimelineElementDragEventArgs e)
        {
            if (e.BeginTime == null || e.EndTime == null)
                return;

            if (e.Track.DataContext is not TimelineTrackModel tackModel)
                return;

            if (e.EventArgs.Data.GetData(typeof(TimelineTrackItemModel)) is not TimelineTrackItemModel model)
                return;

            TimelineTrackItemModel? destModel = model.JsonObjectCopy<TimelineTrackItemModel>();
            if (destModel == null)
                return;

            destModel.BeginTime = e.BeginTime.Value;
            destModel.EndTime = e.EndTime.Value;
            destModel.IsSelected = false;

            tackModel.Items.Add(destModel);


        }
    }
}
