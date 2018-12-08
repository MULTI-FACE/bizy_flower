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
using System.Windows.Shapes;

namespace WpfApp2
{
    
    public partial class Window1 : Window
    {
        /// <summary>
        /// Логика взаимодействия для Window1.xaml
        /// Переменный для замеры экрана и размещения windows1 формы 
        /// </summary>
        double screeHeight = SystemParameters.FullPrimaryScreenHeight;
        double screeWidth = SystemParameters.FullPrimaryScreenWidth;

        /// <summary>
        /// Подгрузка объектов и размещения окна исходя из замеров
        /// </summary>
        public Window1()
        {
            InitializeComponent();
            this.Top = (screeHeight - 380.625);
            this.Left = (screeWidth - 312.028);

        }


        /// <summary>
        /// Grid_MouseMove - метод отслеживающий перемещение мышки
        /// После того, как пользователь сделал малейшее перемещение ПО отсчитывает 7 секунд от старта движения и завершает приложение
        /// </summary>
        /// <exception>
        /// На случай провисания, чтобы исключить на этапе разработки перегрузку и в случае чего завршить приложение 
        /// </exception>
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                DateTime open = DateTime.Now;
                DateTime sec = DateTime.Now;
                while (((sec - open).TotalSeconds) < 7)
                { sec = DateTime.Now; }
                Application.Current.Shutdown();
            }
            catch (Exception)
            { Application.Current.Shutdown(); }
        }
    }
}

