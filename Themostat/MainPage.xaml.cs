using Newtonsoft.Json;
using System;
using System.Net.Http;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Themostat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private const int AC_PIN = 3;
        private const int Heat_PIN = 5;

        public static Tempeture tempeture = new Tempeture();

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = tempeture;
            getOutTempeture();
            InitGPIO();
        }
        private void InitGPIO()
        {
            tempeture.AcPin = GpioController.GetDefault()?.OpenPin(AC_PIN);
            tempeture.HeatPin = GpioController.GetDefault()?.OpenPin(Heat_PIN);
            tempeture.AcPin.SetDriveMode(GpioPinDriveMode.Output);
            tempeture.HeatPin.SetDriveMode(GpioPinDriveMode.Output);
            tempeture.AcPin.Write(GpioPinValue.Low);
            tempeture.HeatPin.Write(GpioPinValue.Low);
        }

        private async void getOutTempeture()
        {
            try
            {
                string weburl = "http://api.openweathermap.org/data/2.5/weather?q=Dallas,usa&APPID=567648f0363539fefe45dad33d84032a";

                var response = await new HttpClient().GetAsync(new Uri(weburl));
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var account = JsonConvert.DeserializeObject<Current>(responseBody);
                tempeture.Temp = account.Main.temp;
                tempeture.Min = account.Main.temp_min;
                tempeture.Max = account.Main.temp_max;
                tempeture.Humidity = account.Main.humidity;
                tempeture.WindSpeed = account.wind.speed;

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

        }

        private void MinusHot_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(HotLimit.Text, out int ColdLimit))
                HotLimit.Text = (ColdLimit - 1).ToString();
            else
                HotLimit.Text = "80";
        }

        private void PlusHot_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(HotLimit.Text, out int limit))
                HotLimit.Text = (limit + 1).ToString();
            else
                HotLimit.Text = "80";
        }

        private void PlusCold_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ColdLimit.Text, out int limit))
                ColdLimit.Text = (limit - 1).ToString();
            else
                ColdLimit.Text = "80";
        }

        private void MinusCold_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ColdLimit.Text, out int limit))
                ColdLimit.Text = (limit - 1).ToString();

        }
    }
}
