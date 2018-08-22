using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppDataBind
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //!取消正在运行的线程
        CancellationTokenSource source = null;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void CalcBtnClick(object sender, RoutedEventArgs e)
        {
            source = new CancellationTokenSource();
            _result.Text = string.Empty;
            int from = int.Parse(_from.Text);
            int to = int.Parse(_to.Text);
            _calcButton.IsEnabled = false;
            _cancelButton.IsEnabled = true;
            ThreadPool.QueueUserWorkItem((state)=> {
                int total = CountPrimes(from, to,source.Token);
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    _result.Text = "Total Primes:" + total.ToString();
                    _calcButton.IsEnabled = true;
                }));
            });
            
        }

        private void _cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (source != null)
                source.Cancel();
        }
        private int CountPrimes(int from,int to,CancellationToken token)
        {
            int total = 0;
            for(int i = from; i <= to; i++)
            {
                if (token.IsCancellationRequested)
                    return -1;
                bool isPrime = true;
                int limit = (int)Math.Sqrt(i);
                for(int j = 2; j <= limit; j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                    total++;
            }
            return total;
        }
    }
    public class Person
    {
        public int Age { get; set; }
    }
    public class AgeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int age;
            if(int.TryParse((string)value,out age))
            {
                if (age < 0 || age > 120)
                    return new ValidationResult(false, "this is invalide age.");
                else
                    return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "please input the age.");
        }
    }

}
