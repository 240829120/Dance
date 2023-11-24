using Dance.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Dance.WpfTest
{
    [ExpandableObject]
    public class Lession
    {
        [Category("基础"), Description("描述描述123")]
        public string? Name { get; set; }

        [Category("基础"), Description("描述描述123")]
        public int Age { get; set; }


    }

    [ExpandableObject]
    public class Teacher
    {

        [Category("基础"), Description("描述描述123")]
        public string? Name { get; set; }

        [Category("基础"), Description("描述描述123")]
        public int Age { get; set; }

        [Category("基础"), Description("描述描述1231111")]
        public List<Lession> Lessions { get; set; } = new() { new Lession { Name = "t1" } };


        [Category("基础"), Description("描述描述1231111")]
        public Lession Lession2 { get; set; } = new Lession();
    }

    public class Student
    {
        [Category("基础"), Description("描述描述123")]
        public string? Property_1_1 { get; set; }


        [Category("基础")]
        public int Property_1_2 { get; set; }


        [Category("基础")]
        public string? Property_1_3 { get; set; }

        [Category("基础")]
        public string? Property_1_4 { get; set; }


        [Category("基础2")]
        public System.Windows.Media.SolidColorBrush Brush { get; set; }

        [Category("基础2")]
        public Thickness Thickness { get; set; } = new Thickness(1, 2, 3, 4);

        [Category("基础2")]
        public DateTime DateTime { get; set; }

        [Category("基础2")]
        public TimeSpan TimeSpan { get; set; }

        [Category("基础2")]
        public System.Windows.Media.Color Color { get; set; }

        [Category("老师")]
        public Teacher Teacher { get; set; } = new();

        [Category("列表")]
        public List<Teacher> Items { get; set; } = new() { new Teacher { Name = "zs", Age = 17 } };

        [Category("列表")]
        public List<string> Items2 { get; set; } = new();

        [Category("列表")]
        public List<int> Items3 { get; set; } = new List<int>() { 1, 2, 3 };


        [Category("测试")]
        public HorizontalAlignment HorizontalAlignment { get; set; }
    }

    public class TimelineTrackItemModel : DanceModel, IDanceTimelineTrackElement, IDanceJsonObject
    {
        public string PART_DanceObjectType => this.GetType().AssemblyQualifiedName ?? string.Empty;

        public string Name { get; set; }

        #region BeginTime -- 开始时间

        private TimeSpan beginTime;

        public TimeSpan BeginTime
        {
            get { return beginTime; }
            set { beginTime = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region EndTime -- 结束时间

        private TimeSpan endTime;

        public TimeSpan EndTime
        {
            get { return endTime; }
            set { endTime = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region IsSelected -- 是否被选中

        private bool isSelected;
        /// <summary>
        /// 是否被选中
        /// </summary>
        [JsonIgnore]
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; this.OnPropertyChanged(); }
        }

        #endregion
    }

    public class TimelineTrackModel : DanceModel, IDanceTimelineTrack, IDanceJsonObject
    {
        public string PART_DanceObjectType => this.GetType().AssemblyQualifiedName ?? string.Empty;

        public string Name { get; set; }

        #region IsSelected 

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; this.OnPropertyChanged(); }
        }

        #endregion

        public IList<IDanceTimelineTrackElement> Items { get; } = new ObservableCollection<IDanceTimelineTrackElement>();
    }

}
