using ESCde.Cars.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESCde.Cars.MobileApp.Model
{
    public class MyModel : Observable
    {
        private MyModel()
        {
            caller = new ApiCaller(new Uri("http://localhost:54559"));
        }

        private ApiCaller caller;
        private static MyModel _instance;
        public static MyModel Instance
        {
            get
            {
                if (_instance == null) _instance = new MyModel();
                return _instance;
            }
        }

        private List<Car> cars = new List<Car>();

        public List<Car> Cars { get { return cars; } }

        public async Task RefreshCarsAsync()
        {
            cars = await caller.GetAllCarsAsync();
            Update();
        }

    }
}
