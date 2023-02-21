using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Threading;
using static coord;

namespace WEATHER_APP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object? sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToString("ddd, dd MMM yyy hh:mm:ss tt 'EST'"); //"HH:mm:ss.fff"
        }

        string APIKey = "4a10b7f5f601e202246857687ab8ed87";

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            getWeather();
        }

        private void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", txtCity.Text, APIKey);
                var json = web.DownloadString(url);
                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                imgCondition.Source = new BitmapImage(new Uri("https://openweathermap.org/img/w/" + Info.weather[0].icon + ".png"));

                labConditions.Content = Info.weather[0].main;
                labDetails.Content = Info.weather[0].description;
                labSunset.Content = Info.sys.sunset.ToString();
                labSunrise.Content = Info.sys.sunrise.ToString();
                labWindSpeed.Content = Info.wind.speed.ToString();
                labPressure.Content = Info.main.pressure.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
