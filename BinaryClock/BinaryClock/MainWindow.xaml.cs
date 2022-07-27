using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BinaryClock
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Sets all rectangles opacity to 0.35
            foreach (Rectangle r in LogicalTreeHelper.GetChildren(MainGrid).OfType<Rectangle>().Where((x) => !x.Name.ToLower().Contains("button")))
            {
                r.Fill.Opacity = 0.35;
            }
            StartClock();
        }

        private void StartClock()
        {
            int bitLength = 6;
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    DateTime _now = System.DateTime.Now;

                    // convert to base of 2 for binary
                    // padleft to get string of length 6
                    String _binHour = Convert.ToString(_now.Hour, 2).PadLeft(bitLength, '0');
                    String _binMinute = Convert.ToString(_now.Minute, 2).PadLeft(bitLength, '0');
                    String _binSeconds = Convert.ToString(_now.Second, 2).PadLeft(bitLength, '0');

                    // For each digit of the binary hour representation
                    for (int i = 0; i < bitLength; i++)
                    {
                        // Dispatcher invoke to refresh the UI, which belongs to the main thread
                        H0.Dispatcher.Invoke(() =>
                        {
                            // Update the contents of the labels which use decimal h/m/s representation
                            HH.Text = _now.Hour.ToString("00");
                            MM.Text = _now.Minute.ToString("00");
                            SS.Text = _now.Second.ToString("00");

                            // Search for a rectangle which name corresponds to the _binHour current char index.
                            // Then, set its opacity to 1 if the current _binHour digit is 1, or to 0.35 otherwise
                            ((Rectangle)MainGrid.FindName("H" + i.ToString())).Fill.Opacity =
                            _binHour.Substring(i, 1).CompareTo("1") == 0 ? 1 : 0.35;
                            ((Rectangle)MainGrid.FindName("M" + i.ToString())).Fill.Opacity =
                            _binMinute.Substring(i, 1).CompareTo("1") == 0 ? 1 : 0.35;
                            ((Rectangle)MainGrid.FindName("S" + i.ToString())).Fill.Opacity =
                            _binSeconds.Substring(i, 1).CompareTo("1") == 0 ? 1 : 0.35;
                        });
                    }
                }
            });
        }
        public Brush RedLGBBrush
        {
            get
            {
                LinearGradientBrush RedLGB = new LinearGradientBrush();
                RedLGB.StartPoint = new Point(0.5, 0);
                RedLGB.EndPoint = new Point(0.5, 1);
                RedLGB.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FFFFFF1B"), 0));
                RedLGB.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FFB41313"), 0.568));

                return RedLGB;
            }
        }
        public Brush GreenLGBBrush
        {
            get
            {
                LinearGradientBrush RedLGB = new LinearGradientBrush();
                RedLGB.StartPoint = new Point(0.5, 0);
                RedLGB.EndPoint = new Point(0.5, 1);
                RedLGB.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FFFFFF1B"), 0));
                RedLGB.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF29B413"), 0.568));

                return RedLGB;
            }
        }
        public Brush BlueLGBBrush
        {
            get
            {
                LinearGradientBrush RedLGB = new LinearGradientBrush();
                RedLGB.StartPoint = new Point(0.5, 0);
                RedLGB.EndPoint = new Point(0.5, 1);
                RedLGB.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF00F6FF"), 0));
                RedLGB.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF2113B4"), 0.568));

                return RedLGB;
            }
        }
        private void RedButtonPressed(object sender, MouseButtonEventArgs e)
        {
            foreach (TextBlock t in LogicalTreeHelper.GetChildren(MainGrid).OfType<TextBlock>())
            {
                t.Foreground = RedButton.Fill;
            }
            foreach (Rectangle r in LogicalTreeHelper.GetChildren(MainGrid).OfType<Rectangle>().Where((x) => !x.Name.ToLower().Contains("button")))
            {
                double previousOpacity = r.Fill.Opacity;
                r.Fill = RedLGBBrush;
                r.Fill.Opacity = previousOpacity;
            }
        }
        private void GreenButtonPressed(object sender, MouseButtonEventArgs e)
        {
            foreach (TextBlock t in LogicalTreeHelper.GetChildren(MainGrid).OfType<TextBlock>())
            {
                t.Foreground = GreenButton.Fill;
            }
            foreach (Rectangle r in LogicalTreeHelper.GetChildren(MainGrid).OfType<Rectangle>().Where((x) => !x.Name.ToLower().Contains("button")))
            {
                double previousOpacity = r.Fill.Opacity;
                r.Fill = GreenLGBBrush;
                r.Fill.Opacity = previousOpacity;
            }
        }
        private void BlueButtonPressed(object sender, MouseButtonEventArgs e)
        {
            foreach (TextBlock t in LogicalTreeHelper.GetChildren(MainGrid).OfType<TextBlock>())
            {
                t.Foreground = BlueButton.Fill;
            }
            foreach (Rectangle r in LogicalTreeHelper.GetChildren(MainGrid).OfType<Rectangle>().Where((x) => !x.Name.ToLower().Contains("button")))
            {
                double previousOpacity = r.Fill.Opacity;
                r.Fill = BlueLGBBrush;
                r.Fill.Opacity = previousOpacity;
            }
        }
    }
}
