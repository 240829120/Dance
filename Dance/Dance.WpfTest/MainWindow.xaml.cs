using Dance.Wpf;
using System;
using System.Collections.Generic;
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

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.border.CreateKeyFrameAnimation()
                       .Thickness(Border.MarginProperty, null, new DanceAnimationKeyFrame<Thickness>(new Thickness(0, 0, 0, 0), 0),
                                                          new DanceAnimationKeyFrame<Thickness>(new Thickness(100, 0, 0, 0), 2))
                       .Double(Border.OpacityProperty, null, new DanceAnimationKeyFrame<double>(1, 0),
                                                        new DanceAnimationKeyFrame<double>(0, 4))
                       .Color("(Border.Background).(SolidColorBrush.Color)", null, new DanceAnimationKeyFrame<Color>(Colors.Red, 0),
                                                                                  new DanceAnimationKeyFrame<Color>(Colors.Yellow, 2))
                       .Commit();

        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            DanceDomain.Current.Dispose();
            Application.Current.Shutdown();
        }
    }
}
