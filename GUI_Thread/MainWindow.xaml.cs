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
using System.Threading;
namespace GUI_Thread
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource source;
        private CancellationToken toke;
        public MainWindow()
        {
            InitializeComponent();
            //_btn.Click += async (sender,e)=> {
            //    Do(false, "Doing");
            //    await Task.Delay(3000);
            //    Do(true, "Complete");
            //};
        }

        private async void _do_Click(object sender, RoutedEventArgs e)
        {
            _do.IsEnabled = false;
            source = new CancellationTokenSource();
            toke = source.Token;
            int complete = 0;
            const int time = 10;
            const int percent = 100 / time;
            for (int i = 0; i < time; i++) {
                if (toke.IsCancellationRequested)
                    break;
                try {
                    await Task.Delay(500, toke);
                    complete = (i+1) * percent;
                }
                catch (Exception ex) {
                    //ex.ToString();
                    complete = i * percent;
                }
                finally {
                    _progressBar.Value = complete;
                }
                
            }
            var msg = toke.IsCancellationRequested ? $"进度:{complete},已取消" : $"进度:{complete},已完成";
            MessageBox.Show(msg, "Infomation");
            _progressBar.Value = 0;
            InitTool();
           
        }

        private  void _cancel_Click(object sender, RoutedEventArgs e)
        {
            if (_do.IsEnabled) return;
            _do.IsEnabled = true;
            source.Cancel();
        }

        private void InitTool()
        {
            _progressBar.Value = 0;
            _do.IsEnabled = true;
            _cancel.IsEnabled = true;
        }
        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    _btn.IsEnabled = false;
        //    _lbl.Content = "Doing";

        //    await Task.Delay(3000);

        //    _btn.IsEnabled = true;
        //    _lbl.Content = "Complete";

        //}
        //private void Do(bool isEnabled,string text)
        //{
        //    _btn.IsEnabled = IsEnabled;
        //    _lbl.Content = text;

        //}
    }
}
