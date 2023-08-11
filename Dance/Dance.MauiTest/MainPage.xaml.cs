using System.Collections.Generic;

namespace Dance.MauiTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object? sender, EventArgs e)
        {
            List<string> list = new();

            list.Add("https://soreal-erp.oss-cn-beijing.aliyuncs.com/soreal-cms/d3f5d1ca626d44dbba2527c04a97708e.jpg");
            list.Add("https://soreal-erp.oss-cn-beijing.aliyuncs.com/soreal-cms/a9560038774d4b8aa82ddef95cfdfd94.jpg");
            list.Add("https://soreal-erp.oss-cn-beijing.aliyuncs.com/soreal-cms/1c31356f394e4fe998f5fbf248bea7bd.jpeg");

            //this.lv.SetValue(BindableLayout.ItemsSourceProperty, this.list);
            this.lv.ItemsSource = list;
            this.lv.SelectedItem = list.FirstOrDefault();

        }
    }
}