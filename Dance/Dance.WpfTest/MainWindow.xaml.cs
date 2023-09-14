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
        public int Index { get; set; }

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
            new Student { Index =0, Name="zhangsan", Age =16 },
            new Student { Index =1, Name="lisi", Age =17 },
            new Student { Index =2, Name="wangwu", Age =18 },
            new Student { Index =3, Name="zhangsan", Age =16 },
            new Student { Index =4, Name="lisi", Age =17 },
            new Student { Index =5, Name="wangwu", Age =18 },
            new Student { Index =6, Name="zhangsan", Age =16 },
            new Student { Index =7, Name="lisi", Age =17 },
            new Student { Index =8, Name="wangwu", Age =18 },
            new Student { Index =9, Name="zhangsan", Age =16 },
            new Student { Index =10, Name="lisi", Age =17 },
            new Student { Index =11, Name="wangwu", Age =18 },
            new Student { Index =12, Name="zhangsan", Age =16 },
            new Student { Index =13, Name="lisi", Age =17 },
            new Student { Index =14, Name="wangwu", Age =18 },
            new Student { Index =15, Name="zhangsan", Age =16 },
            new Student { Index =16, Name="lisi", Age =17 },
            new Student { Index =17, Name="wangwu", Age =18 },
            new Student { Index =18, Name="zhangsan", Age =16 },
            new Student { Index =19, Name="lisi", Age =17 },
            new Student { Index =20, Name="wangwu", Age =18 },
            new Student { Index =21, Name="zhangsan", Age =16 },
            new Student { Index =22, Name="lisi", Age =17 },
            new Student { Index =23, Name="wangwu", Age =18 },
            new Student { Index =24, Name="zhangsan", Age =16 },
            new Student { Index =25, Name="lisi", Age =17 },
            new Student { Index =26, Name="wangwu", Age =18 },
            new Student { Index =27, Name="zhangsan", Age =16 },
            new Student { Index =28, Name="lisi", Age =17 },
            new Student { Index =29, Name="wangwu", Age =18 },
            new Student { Index =30, Name="zhangsan", Age =16 },
            new Student { Index =31, Name="lisi", Age =17 },
            new Student { Index =32, Name="wangwu", Age =18 },
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

            this.dg.ItemsSource = List;
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
