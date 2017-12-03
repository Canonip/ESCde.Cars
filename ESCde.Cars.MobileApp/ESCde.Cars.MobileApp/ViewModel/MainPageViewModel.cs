using ESCde.Cars.MobileApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESCde.Cars.MobileApp.ViewModel
{
    public class MainPageViewModel : IObserver, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<CarViewModel> cars = new ObservableCollection<CarViewModel>();
        public ObservableCollection<CarViewModel> Cars
        {
            get
            {
                return cars;
            }
            private set
            {
                cars = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cars)));
            }
        }

        public string ButtonText { get; set; }

        public void Update()
        {
            //do not overwrite with new ObservableCollection<Car>();
            //because CollectionChangedEvent will be null, thus not fire!
            Cars.Clear();
            foreach (var car in MyModel.Instance.Cars)
            {
                var carvm = new CarViewModel()
                {
                    ColorText = car.Color,
                    Id = car.Guid.ToString(),
                    Description = string.Format("{0}, {1} {2}", car.YearOfManufacture, car.Vendor, car.Model)
                };
                Cars.Add(carvm);
            }
        }

        public async Task RefreshFromServerAsync()
        {
            ButtonText = "Fetching ... plz wait";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonText)));
            await MyModel.Instance.RefreshCarsAsync();
            ButtonText = "Fetched Cars from backend. Click to fetch again";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonText)));
        }

        public MainPageViewModel()
        {
            MyModel.Instance.Register(this);
            ButtonText = "Fetch Cars from Backend";
            Cars.CollectionChanged += Cars_CollectionChanged;
            Cars.Add(new CarViewModel()
            {
                ColorText = "Colorful",
                Id = "this is no id",
                Description = "cool car"
            });
        }

        private void Cars_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cars)));
        }
    }
}
