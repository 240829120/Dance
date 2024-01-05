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
    //public class TestModel : DanceModel, IDanceModel3D
    //{
    //    public string PART_DanceObjectType => this.GetType()?.AssemblyQualifiedName ?? string.Empty;


    //    public DataTemplate DataTemplate { get; set; }



    //}

    public class TestModel(IDanceHistoryManager historyManager) : DanceHistoryModel(historyManager)
    {
        private double _value;

        public double Value
        {
            get { return _value; }
            set { this.OnHistoryPropertyChanged(ref _value, value); }
        }

    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Model = new(this.HistoryManager);

            this.slider.DataContext = this.Model;
            this.slider.GotMouseCapture += Slider_GotMouseCapture;
            this.slider.LostMouseCapture += Slider_LostMouseCapture;

            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
        }

        private void Slider_LostMouseCapture(object sender, MouseEventArgs e)
        {
            this.HistoryManager.IsExecuting = false;
            this.HistoryManager.Append(new DancePropertyChangedHistoryStep(this.Model, nameof(TestModel.Value), this.Model.GetCache<double>("Value"), this.Model.Value));
        }

        private void Slider_GotMouseCapture(object sender, MouseEventArgs e)
        {
            this.HistoryManager.IsExecuting = true;
            this.Model.Cache("Value", this.Model.Value);
        }

        private readonly DanceHistoryManager HistoryManager = new();

        private readonly TestModel Model;

        //   public List<TestModel> list = new List<TestModel>();

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            DanceDomain.Current?.Dispose();
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            this.HistoryManager.Undo();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            this.HistoryManager.Redo();
        }
    }
}