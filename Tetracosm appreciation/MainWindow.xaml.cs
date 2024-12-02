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
        ScaleTransform scale = new ScaleTransform();
        Random random = new Random();
        int minute = 9;
        int next_x = 0;
        int next_y = 0;
        bool right = true;
        public MainWindow()
        {
            InitializeComponent();
            Character.RenderTransform = trans;
            Character.LayoutTransform = scale;
            System.Windows.Threading.DispatcherTimer Main = new System.Windows.Threading.DispatcherTimer();
            Main.Tick += Move;
            Main.Interval = new TimeSpan(0, 0, 1);
            Main.Start();
        }

        private void Screen_Deactivated(object sender, EventArgs e)
        {
            Topmost = true;
            Activate();
        }
        
        private void Move(object sender, EventArgs e)
        {
            
            display.Content = "X: " + Character.RenderTransform.Value.OffsetX + "\nY: " + Character.RenderTransform.Value.OffsetY;
            minute++;
            if (minute >= 10)
            {
                minute = 0;
                int current_x = (int)Character.RenderTransform.Value.OffsetX;
                int current_y = (int)Character.RenderTransform.Value.OffsetY;
                next_x = random.Next((int)ActualHeight - 100);
                next_y = random.Next((int)ActualWidth - 100);
                DoubleAnimation anim1 = new DoubleAnimation{
                    From = Character.RenderTransform.Value.OffsetX,
                    To = next_y,
                    Duration =  TimeSpan.FromSeconds(6),
                    EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseInOut, Exponent = 3 }
                    };
                DoubleAnimation anim2 = new DoubleAnimation
                {
                    From = Character.RenderTransform.Value.OffsetY,
                    To = next_x,
                    Duration = TimeSpan.FromSeconds(6),
                    EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseInOut, Exponent = 3 }
                }; ;
                if (current_x<next_x)
                {
                    scale.ScaleX = 1;
                }
                else
                {
                    scale.ScaleX = -1;
                }
                trans.BeginAnimation(TranslateTransform.XProperty, anim1);
                trans.BeginAnimation(TranslateTransform.YProperty, anim2);
            }
        }
    }
}
