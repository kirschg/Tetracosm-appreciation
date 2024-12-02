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
        //transforms
        Random random = new Random();
        ScaleTransform scale = new ScaleTransform();
        TranslateTransform trans = new TranslateTransform();

        //movement
        int next_y = 0;
        int next_x = 0;
        int movetimer = 45;

        //modechange
        string[] modes = { "fly", "hover", "sit", "rest", "sleep"};
        string mode = "fly";
        int changetimer = 0;

        //animation
        int framecount = 4;
        int frame = 1;
        string animname = "fly";

        //startup
        public MainWindow()
        {
            InitializeComponent();
            trans.X = -120;
            trans.Y = random.Next(-100, 1200);
            Character.RenderTransform = trans;
            Character.LayoutTransform = scale;
            System.Windows.Threading.DispatcherTimer Main = new System.Windows.Threading.DispatcherTimer();
            Main.Tick += Tick;
            Main.Interval = new TimeSpan(0, 0, 0, 0, 120);
            Main.Start();
            Topmost = true;
        }

        //keeping it on top
        /*private void Screen_Deactivated(object sender, EventArgs e)
        {
            Topmost = true;
            Activate();
        }*/
        
        //tick every 120 millisecs
        private void Tick(object sender, EventArgs e)
        {
            movetimer++;
            changetimer++;
            /*if (changetimer>random.Next(150,250))
                mode = modes[random.Next(0,modes.Length)];*/

            switch (mode)
            {
                case "fly":
                    animname = "fly";
                    fly();
                    break;
                case "sit":
                    animname = "stand";
                    sit();
                    break;
                case "rest":
                    animname = "rest";
                    rest();
                    break;
                case "sleep":
                    animname = "sleep";
                    sleep();
                    break;
                case "hover":
                    animname = "fly";
                    hover();
                    break;
                default:
                    animname = "fly";
                    fly();
                    break;
            }

            Character.Source = new BitmapImage(new Uri(@"/Tetracosm appreciation;component/Images/" + animname + "-Frame-" + frame + ".png", UriKind.Relative));
            frame++;
            if (frame>framecount)
            {
                frame = 1;
            }
            
        }

        //flying around randomly
        private void fly()
        {
            if (movetimer >= 50)
            {
                movetimer = 0;
                int current_x = (int)Character.RenderTransform.Value.OffsetX;
                int current_y = (int)Character.RenderTransform.Value.OffsetY;
                next_y = random.Next((int)ActualHeight - 100);
                next_x = random.Next((int)ActualWidth - 100);
                DoubleAnimation anim1 = new DoubleAnimation
                {
                    From = Character.RenderTransform.Value.OffsetX,
                    To = next_x,
                    Duration = TimeSpan.FromSeconds(8),
                    EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseInOut, Exponent = 3 }
                };
                DoubleAnimation anim2 = new DoubleAnimation
                {
                    From = Character.RenderTransform.Value.OffsetY,
                    To = next_y,
                    Duration = TimeSpan.FromSeconds(8),
                    EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut, Exponent = 1 }
                }; ;
                if (current_x < next_x)
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

        private void sit()
        {

        }

        private void rest()
        {

        }

        private void sleep()
        {

        }

        private void hover()
        {

        }
    }
}
