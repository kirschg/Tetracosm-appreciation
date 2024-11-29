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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tetracosm_appreciation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TranslateTransform trans = new TranslateTransform();
        Random random = new Random();
        int minute = 0;
        int next_x = 0;
        int next_y = 0;
        bool right = true;
        public MainWindow()
        {
            InitializeComponent();
            Character.RenderTransform = trans;
            System.Windows.Threading.DispatcherTimer Main = new System.Windows.Threading.DispatcherTimer();
            Main.Tick += Main_Tick;
            Main.Interval = new TimeSpan(0, 0, 1);
            Main.Start();
        }

        private void Screen_Deactivated(object sender, EventArgs e)
        {
            Topmost = true;
            Activate();
        }
        
        private void Main_Tick(object sender, EventArgs e)
        {
            
            display.Content = "X: " + Character.RenderTransform.Value.OffsetX + "\nY: " + Character.RenderTransform.Value.OffsetY;
            minute++;
            if (minute >= 3)
            {
                minute = 0;
                int current_x = (int)Character.RenderTransform.Value.OffsetX;
                int current_y = (int)Character.RenderTransform.Value.OffsetY;
                next_x = random.Next((int)ActualHeight - 100);
                next_y = random.Next((int)ActualWidth - 100);
                if (current_x<next_x)
                {
                    Character.RenderTransform.Value.ScalePrepend(1, 1);
                }
                else
                {
                    Character.RenderTransform.Value.ScalePrepend(1, -1);
                }
                DoubleAnimation anim1 = new DoubleAnimation(Character.RenderTransform.Value.OffsetX, next_y, TimeSpan.FromSeconds(3));
                DoubleAnimation anim2 = new DoubleAnimation(Character.RenderTransform.Value.OffsetY, next_x, TimeSpan.FromSeconds(3));
                trans.BeginAnimation(TranslateTransform.XProperty, anim1);
                trans.BeginAnimation(TranslateTransform.YProperty, anim2);
            }
        }
    }
}
