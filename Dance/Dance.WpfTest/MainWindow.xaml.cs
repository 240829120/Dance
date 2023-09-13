using Dance.Common;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dance.WpfTest
{
    public class Student
    {
        public string? Name { get; set; }

        public int Age { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Closed += MainWindow_Closed;
            this.Loaded += MainWindow_Loaded;
        }

        private readonly List<Student> List = new()
        {
            new Student { Name="zhangsan", Age =16 },
            new Student { Name="lisi", Age =17 },
            new Student { Name="wangwu", Age =18 },
        };

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //this.border.CreateKeyFrameAnimation()
            //           .Thickness(Border.MarginProperty, null, new DanceAnimationKeyFrame<Thickness>(new Thickness(0, 0, 0, 0), 0),
            //                                              new DanceAnimationKeyFrame<Thickness>(new Thickness(100, 0, 0, 0), 2))
            //           .Double(Border.OpacityProperty, null, new DanceAnimationKeyFrame<double>(1, 0),
            //                                            new DanceAnimationKeyFrame<double>(0, 4))
            //           .Color("(Border.Background).(SolidColorBrush.Color)", null, new DanceAnimationKeyFrame<Color>(Colors.Red, 0),
            //                                                                      new DanceAnimationKeyFrame<Color>(Colors.Yellow, 2))
            //           .Commit("TEST");

            //List<string> list = new()
            //{
            //    "https://soreal-erp.oss-cn-beijing.aliyuncs.com/soreal-cms/d3f5d1ca626d44dbba2527c04a97708e.jpg",
            //    "https://soreal-erp.oss-cn-beijing.aliyuncs.com/soreal-cms/a9560038774d4b8aa82ddef95cfdfd94.jpg",
            //    "https://soreal-erp.oss-cn-beijing.aliyuncs.com/soreal-cms/1c31356f394e4fe998f5fbf248bea7bd.jpeg"
            //};

            //this.lv.SetValue(BindableLayout.ItemsSourceProperty, this.list);
            //this.lv.ItemsSource = list;
            //this.lv.SelectedItem = list.FirstOrDefault();

            //DanceWebApiServer server = new();
            //server.Urls.Add("http://localhost:5001/");
            //server.Assemblies.Add(this.GetType().Assembly);

            //server.Start();
        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            DanceDomain.Current.Dispose();
            Application.Current.Shutdown();
        }

        private void DanceSwipeGestureRecognizer_Swiped(object sender, DanceSwipeGestureRecognizerEventArgs e)
        {
            Debug.WriteLine(e.Direction);
        }
    }
}
