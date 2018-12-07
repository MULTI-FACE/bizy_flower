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
using System.Windows.Threading;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double screeHeight = SystemParameters.FullPrimaryScreenHeight;
        double screeWidth = SystemParameters.FullPrimaryScreenWidth;
        private DispatcherTimer _timer;
        public static readonly DependencyProperty TickCounterProperty = DependencyProperty.Register(
            "TickCounter", typeof(int), typeof(MainWindow), new PropertyMetadata(default(int)));
        string path = @"c:\windows\system32\drivers\etc\hosts";
        string block = Properties.Resources.block;
        string hosts_true = Properties.Resources.hoststrue;

        public MainWindow()
        {
            InitializeComponent();
            Unlock();
            this.Top = (screeHeight - 380.625);
            this.Left = (screeWidth - 312.028);
            System.Diagnostics.Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 255");

        }

        public int TickCounter
        {
            get { return (int)GetValue(TickCounterProperty); }
            set { SetValue(TickCounterProperty, value); }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            if (--TickCounter <= 0)
            {
                var timer = (DispatcherTimer)sender;
                timer.Stop();
                Unlock();
                Window2 window2 = new Window2();
                window2.Show();
                this.Close();

            }
        }

        private void play_Click_1(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 1");
            TickCounter = 60;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1d);
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();
            Block_Timer();
        }

        private void Block_Timer()
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, false))
                {
                    file.WriteLine(block);
                    file.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + "Зайдите в безопасном режиме");
            }

        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            Unlock();
            Window1 window1 = new Window1();
            window1.Show();
            this.Close();

        }
        private void Unlock()
        {
           
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, false))
            {
                file.WriteLine(hosts_true);
                file.Close();
            }


        }
    }
    }

