using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Random random = new Random();

        //transforms
        ScaleTransform scale = new ScaleTransform();
        TranslateTransform trans = new TranslateTransform();

        //movement
        int next_y = 0;
        int next_x = 0;
        byte movetimer = 45;

        //modechange
        string[] modes = { "fly", "sit", "sleep", "hover"};
        string mode = "fly";
        short changetimer = 0;

        //animation
        byte framecount = 4;
        byte frame = 1;
        string prevanim = "fly";
        string animname = "fly";
        byte slowmo = 1;

        //debug toggle
        bool debugOpen = false;

        //startup
        public MainWindow()
        {
            InitializeComponent();
            PlayArea.Height = SystemParameters.VirtualScreenHeight - 1;
            PlayArea.Width = SystemParameters.VirtualScreenWidth;
            Left = SystemParameters.VirtualScreenLeft;
            Top = SystemParameters.VirtualScreenTop;
            if (random.Next(10)>5)
            {
                trans.X = random.Next((int)Width, (int)Width + 75);
            }
            else
            {
                trans.X = random.Next(100,175)*-1;
            }
            
            trans.Y = random.Next((int)Height-100);
            Character.RenderTransform = trans;
            Character.LayoutTransform = scale;
            System.Windows.Threading.DispatcherTimer Main = new System.Windows.Threading.DispatcherTimer();
            Main.Tick += Tick;
            Main.Interval = new TimeSpan(0, 0, 0, 0, 120);
            Main.Start();
            Topmost = true;
        }

        //tick every 120 millisecs
        private void Tick(object sender, EventArgs e)
        {
            Debug.Content = "X:" + trans.X + "\nY: "+trans.Y + "\n" +
                "\nMode: " + mode + "\n" +
                "\nWindow:\nX: " + PlayArea.ActualWidth + "\nY: " + PlayArea.ActualHeight;
            movetimer++;
            changetimer++;
            if (changetimer > random.Next(250, 550))
            {
                mode = modes[random.Next(0, modes.Length)];
                changetimer = 0;
                movetimer = 45;
            }

            prevanim = animname;
               
            switch (mode)
            {
                case "fly":
                    fly();
                    break;
                case "sit":
                    sit();
                    break;
                case "sing":
                    sing();
                    break;
                case "sleep":
                    sleep();
                    break;
                case "hover":
                    hover();
                    break;
                default:
                    fly();
                    break;
            }
            animate();
        }

        //flying around randomly
        private void fly()
        {
            if (movetimer >= 65)
            {
                movetimer = 0;
                framecount = 4;
                slowmo = 1;
                animname = "fly";
                
                int current_x = (int)Character.RenderTransform.Value.OffsetX;
                int current_y = (int)Character.RenderTransform.Value.OffsetY;

                next_y = random.Next((int)ActualHeight-100);
                next_x = random.Next((int)ActualWidth-100);

                DoubleAnimation anim1 = new DoubleAnimation
                {
                    From = Character.RenderTransform.Value.OffsetX,
                    To = next_x,
                    Duration = TimeSpan.FromSeconds(10),
                    EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseInOut, Exponent = 3 }
                };
                DoubleAnimation anim2 = new DoubleAnimation
                {
                    From = Character.RenderTransform.Value.OffsetY,
                    To = next_y,
                    Duration = TimeSpan.FromSeconds(10),
                    EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut, Exponent = 1 }
                };
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
        //perch on the bottom of the screen
        private void sit()
        {
            
            if (trans.Y <= ActualHeight - 74 && trans.Y >= ActualHeight - 140)
            {
                next_y = (int)ActualHeight - 74;
                DoubleAnimation anim1 = new DoubleAnimation
                {
                    From = Character.RenderTransform.Value.OffsetX,
                    To = Character.RenderTransform.Value.OffsetX,
                    Duration = TimeSpan.FromSeconds(0.1),
                };
                DoubleAnimation anim2 = new DoubleAnimation
                {
                    From = Character.RenderTransform.Value.OffsetY,
                    To = next_y,
                    Duration = TimeSpan.FromSeconds(0.1),
                    EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseIn, Exponent = 1 }
                };
                trans.BeginAnimation(TranslateTransform.XProperty, anim1);
                trans.BeginAnimation(TranslateTransform.YProperty, anim2);
                movetimer = 0;
                animname = "sit";
                framecount = 5;
                slowmo = 3;
            }
            else if(movetimer >= 45)
            {
                movetimer = 0;
                framecount = 4;
                slowmo = 1;
                animname = "fly";

                int current_x = (int)Character.RenderTransform.Value.OffsetX;
                int current_y = (int)Character.RenderTransform.Value.OffsetY;

                next_y = (int)ActualHeight - 100;
                next_x = random.Next(current_x - 500, current_x + 500);
                if (next_x > ActualWidth - 100)
                    next_x = (int)ActualWidth - 100;
                if (next_x < 0)
                    next_x = 0;

                DoubleAnimation anim1 = new DoubleAnimation
                {
                    From = Character.RenderTransform.Value.OffsetX,
                    To = next_x,
                    Duration = TimeSpan.FromSeconds(6),
                    EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseInOut, Exponent = 3 }
                };
                DoubleAnimation anim2 = new DoubleAnimation
                {
                    From = Character.RenderTransform.Value.OffsetY,
                    To = next_y,
                    Duration = TimeSpan.FromSeconds(6),
                    EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut, Exponent = 1 }
                };
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
        //perch on the bottom of the screen andd start singing (missing particles for notes)
        private void sing()
        {
            
        }
        //perch on the bottom of the screen and lie down
        private void sleep()
        {
            if (trans.Y <= ActualHeight - 74 && trans.Y >= ActualHeight - 120)
            {
                next_y = (int)ActualHeight - 74;
                DoubleAnimation anim1 = new DoubleAnimation
                {
                    From = Character.RenderTransform.Value.OffsetX,
                    To = Character.RenderTransform.Value.OffsetX,
                    Duration = TimeSpan.FromSeconds(0.1),
                };
                DoubleAnimation anim2 = new DoubleAnimation
                {
                    From = Character.RenderTransform.Value.OffsetY,
                    To = next_y,
                    Duration = TimeSpan.FromSeconds(0.1),
                    EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseIn, Exponent = 1 }
                };
                trans.BeginAnimation(TranslateTransform.XProperty, anim1);
                trans.BeginAnimation(TranslateTransform.YProperty, anim2);
                movetimer = 0;
                framecount = 1;
                animname = "sleep";
                slowmo = 3;
            }
            else if (movetimer >= 45)
            {
                movetimer = 0;
                framecount = 4;
                slowmo = 1;
                animname = "fly";

                int current_x = (int)Character.RenderTransform.Value.OffsetX;
                int current_y = (int)Character.RenderTransform.Value.OffsetY;

                next_y = (int)ActualHeight - 100;
                next_x = random.Next(current_x - 500, current_x + 500);

                if (next_x > ActualWidth - 100)
                    next_x = (int)ActualWidth - 100;
                if (next_x < 0)
                    next_x = 0;

                DoubleAnimation anim1 = new DoubleAnimation
                {
                    From = Character.RenderTransform.Value.OffsetX,
                    To = next_x,
                    Duration = TimeSpan.FromSeconds(6),
                    EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseInOut, Exponent = 3 }
                };
                DoubleAnimation anim2 = new DoubleAnimation
                {
                    From = Character.RenderTransform.Value.OffsetY,
                    To = next_y,
                    Duration = TimeSpan.FromSeconds(6),
                    EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut, Exponent = 1 }
                };
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
        //stop in a spot randomly
        private void hover()
        {
            if (movetimer >= 30)
            {
                movetimer = 0;
                framecount = 4;
                slowmo = 1;
                animname = "fly";

                int current_x = (int)Character.RenderTransform.Value.OffsetX;
                int current_y = (int)Character.RenderTransform.Value.OffsetY;

                next_y = random.Next(current_y - 400, current_y + 400);
                next_x = random.Next(current_x - 400, current_x + 400);

                if (next_y > ActualHeight - 100)
                    next_y = (int)ActualHeight - 100;
                if (next_y < 0)
                    next_y = 0;
                if (next_x > ActualWidth - 100)
                    next_x = (int)ActualWidth - 100;
                if (next_x < 0)
                    next_x = 0;

                DoubleAnimation anim1 = new DoubleAnimation
                {
                    From = Character.RenderTransform.Value.OffsetX,
                    To = next_x,
                    Duration = TimeSpan.FromSeconds(5),
                    EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseInOut, Exponent = 3 }
                };
                DoubleAnimation anim2 = new DoubleAnimation
                {
                    From = Character.RenderTransform.Value.OffsetY,
                    To = next_y,
                    Duration = TimeSpan.FromSeconds(5),
                    EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut, Exponent = 1 }
                };
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
        //animation
        private void animate()
        {
            if (prevanim != animname)
            {
                frame = 1;
            }
            if (frame % slowmo == 0)
            {
                Character.Source = new BitmapImage(new Uri(@"/Tetracosm appreciation;component/Images/" + animname + "-Frame-" + frame / slowmo + ".png", UriKind.Relative));
            }
            frame++;
            if (frame > framecount * slowmo)
            {
                frame = 1;
            }
        }
        private void ToggleDebug(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightCtrl)
            {
                if (debugOpen)
                {
                    Debug.Visibility = Visibility.Hidden;
                    debugOpen = false;
                }
                else
                {
                    Debug.Visibility = Visibility.Visible;
                    debugOpen = true;
                }
                
            }
            
        }
    }
}
