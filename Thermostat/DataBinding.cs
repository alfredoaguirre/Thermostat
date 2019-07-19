using System;
using System.ComponentModel;
using Windows.Devices.Gpio;

namespace Themostat
{
    public class Tempeture : INotifyPropertyChanged
    {

        public static string HeadOnLabel = "head ON";
        public static string HeadOffLabel = "head OFF";
        public static string AcOnLabel = "AC ON";
        public static string AcOffLabel = "AC OFF";

      

        private float temp;
        private float min;
        private float max;
        private float humidity;
        private float windSpeed;
        private bool heatOn;
        private bool acOn;
        private string heatLabel = HeadOffLabel;
        private string acLabel = AcOffLabel;

        public float Temp { get => temp; set { temp = value; RaisePropertyChanged(nameof(Temp)); } }
        public float Min { get => min; set { min = value; RaisePropertyChanged(nameof(Min)); } }
        public float Max { get => max; set { max = value; RaisePropertyChanged(nameof(Max)); } }
        public float Humidity { get => humidity; set { humidity = value; RaisePropertyChanged(nameof(Humidity)); } }
        public float WindSpeed { get => windSpeed; set { windSpeed = value; RaisePropertyChanged(nameof(WindSpeed)); } }

        public Boolean HeatOn
        {
            get => heatOn;
            set
            {
                heatOn = value;
                if (value)
                {
                    AcOn = false;
                    HeatLabel = HeadOnLabel;
                    HeatPin?.Write(GpioPinValue.High);
                }
                else
                {
                    HeatLabel = HeadOffLabel;
                    HeatPin?.Write(GpioPinValue.Low);
                };
                RaisePropertyChanged(nameof(HeatOn));
            }
        }
        public Boolean AcOn
        {
            get => acOn;
            set
            {
                acOn = value;
                if (value)
                {
                    HeatOn = false;
                    AcLabel = AcOnLabel;
                    AcPin?.Write(GpioPinValue.High);
                }
                else
                {
                    AcLabel = AcOffLabel;
                    AcPin?.Write(GpioPinValue.Low);
                };

                RaisePropertyChanged(nameof(AcOn));
            }
        }

        public string HeatLabel { get => heatLabel; private set { heatLabel = value; RaisePropertyChanged(nameof(HeatLabel)); } }
        public string AcLabel { get => acLabel; private set { acLabel = value; RaisePropertyChanged(nameof(AcLabel)); } }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public GpioPin AcPin { get; set; }
        public GpioPin HeatPin { get; set; }
    }
}
