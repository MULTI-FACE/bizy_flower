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
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        double screeHeight = SystemParameters.FullPrimaryScreenHeight;
        double screeWidth = SystemParameters.FullPrimaryScreenWidth;
        public Window1()
        {
            InitializeComponent();
            this.Top = (screeHeight - 380.625);
            this.Left = (screeWidth - 312.028);

        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                DateTime open = DateTime.Now;
                DateTime sec = DateTime.Now;
                while (((sec - open).TotalSeconds) < 15)
                { sec = DateTime.Now; }
                Application.Current.Shutdown();
            }
            catch (Exception)
            { Application.Current.Shutdown(); }
        }
    }
}
