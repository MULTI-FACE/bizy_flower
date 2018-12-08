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

    public partial class MainWindow : Window
    {    /// <summary>
         /// Замеры экрана, переменный вынесены, как глобальный, чтобы в дальнейшем было удобно работать
         /// screeHeight - высота экрана
         /// screeHeight - ширина экрана
         /// </summary>
        double screeHeight = SystemParameters.FullPrimaryScreenHeight;
        double screeWidth = SystemParameters.FullPrimaryScreenWidth;
        /// <summary>
        /// Счетчик и взаимодействие get и set методов 
        /// MainWindow.TickCounterProperty реализация взаимодействия счетчика, формы и обработки значений
        /// Значение задаются только в случае вызов readomly
        /// </summary>
        private DispatcherTimer _timer;
        private static readonly DependencyProperty tickCounterProperty = DependencyProperty.Register(
            "TickCounter", typeof(int), typeof(MainWindow), new PropertyMetadata(default(int)));
        /// <summary>
        /// Обозначение ресурсов в качестве переменных для удобства использования, так же путь к hosts файлу
        /// path - путь к хост файлу
        /// block - файл формата txt, где хранятся листинг для блокировки сайтов
        /// hosts_true - файл того же формата, где хранится чистый файл хост, для восстановления 
        /// </summary>
        string path = @"c:\windows\system32\drivers\etc\hosts";
        string block = Properties.Resources.block;
        string hosts_true = Properties.Resources.hoststrue;

        /// <summary>
        /// MainWindow - где идет подгрузка всех объектов, замеры экрана пользователя, расположение WPF и очистка журнала бразуера IE
        /// Left - ширина экрана 
        /// Top - длина экрана
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Unlock();
            this.Top = (screeHeight - 380.625);
            this.Left = (screeWidth - 312.028);
            System.Diagnostics.Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 255");

        }


        /// <summary>
        /// TickCounter - аксессор для получение и записи данных о тике 
        /// </summary>
        public int TickCounter
        {
            get { return (int)GetValue(TickCounterProperty); }
            set { SetValue(TickCounterProperty, value); }
        }


        /// <summary>
        /// Связка аксессора, тика и формы в формате public 
        /// </summary>
        public static DependencyProperty TickCounterProperty => tickCounterProperty;


        /// <summary>
        /// Timer_Tick метод осуществляющий тик и проверку окончания тайминга
        /// Так же идет вызов разблокировки, закрытие существующей формы и вызов формы windows1, 
        /// когда конечный пользователь не нарушил условия тайминга 
        /// </summary>
        /// <param name="sender"></param> переменная для связки таймера
        /// <param name="e"></param> параметр не содержит данных
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


        /// <summary>
        /// play_Click_1 метод обработывающий клик мыши по кнопки play
        /// По щелчку запускает блокировщик и задается счетчик
        /// </summary>
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


        /// <summary>
        /// Block_Timer метод редактирующий данные в hosts файле
        /// </summary>
        /// <exception>
        /// Обработчик, на случай, если пользователь использует приложение без надлежащих прав (администратора)
        /// </exception>
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
        /// <summary>
        ///  stop_Click метод отвечающий за клик по внопке stop 
        ///  Реализация разблокировки при помощи вызов метода Unlock, закрытия текущей формы и вызов формы windows1
        /// </summary>
        /// <param name="sender"></param> переменная для связки таймера
        /// <param name="e"></param> параметр не содержит данных
        private void stop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _timer.Stop();
                Unlock();
                Window1 window1 = new Window1();
                window1.Show();
                this.Close();
            }
            catch (Exception)
            {
                Application.Current.Shutdown();
            }

        }

        /// <summary>
        ///  Unlock метод возврата файла hosts к прежнему состоянию
        /// </summary>
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

