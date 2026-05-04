using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DiceRollGame.ViewModels
{
    public partial class DiceViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _diceResult = 1;

        public DiceViewModel()
        {
            if (Accelerometer.Default.IsSupported)
            {
                Accelerometer.Default.ReadingChanged += Accelerometer_ReadingChanged;
            }
            else
            {
                throw new NotSupportedException("Accelerometer is not supported on this device.");
            }
        }

        [RelayCommand]
        public void StartShakeDetection()
        {
            if (Accelerometer.Default.IsSupported && !Accelerometer.Default.IsMonitoring)
            {
                try
                {
                    Accelerometer.Default.Start(SensorSpeed.Game);
                }
                catch (Exception)
                {
                    throw new Exception("Unable to start the accelerometer.");
                }
            }
        }

        [RelayCommand]
        public void StopShakeDetection()
        {
            if (Accelerometer.Default.IsSupported && Accelerometer.Default.IsMonitoring)
            {
                Accelerometer.Default.Stop();
            }
        }

        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            double acceleration = Math.Sqrt(data.Acceleration.X * data.Acceleration.X +
                                            data.Acceleration.Y * data.Acceleration.Y +
                                            data.Acceleration.Z * data.Acceleration.Z);

            if (acceleration > 2.0)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    RollDice();
                });
            }
        }

        [RelayCommand]
        private void RollDice()
        {
            DiceResult = Random.Shared.Next(1, 7);
        }
    }
}
