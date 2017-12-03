using ESCde.Cars.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ESCde.Cars.MobileApp.Model
{
    public class ApiCaller
    {
        Uri Server { get; }

        public ApiCaller(Uri server)
        {
            Server = server;
        }
        /// <summary>
        /// Creates New Car in Backend
        /// </summary>
        /// <param name="car"></param>
        /// <returns>true if successful, false otherwise</returns>
        public async Task<bool> CreateNewCarAsync(Car car)
        {
            //Create Httpclient using "using", so we do not have to call Dispose()
            using (var client = new HttpClient())
            {
                client.BaseAddress = Server;
                //serialize to json using Newtonsoft.JSON
                var json = JsonConvert.SerializeObject(car);
                //create content
                var content = new StringContent(json, Encoding.UTF8, "application/json"); 

                HttpResponseMessage response = await client.PostAsync("/api/car", content); //DO IT!

                if (response.IsSuccessStatusCode) //this is bad practise! do not do this!
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Lists all Cars available
        /// </summary>
        /// <returns></returns>
        public async Task<List<Car>> GetAllCarsAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = Server;
                var response = await client.GetAsync("/api/car");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Car>>(responseString);
                }
                else
                {
                    //generic Exception is bad practise, create own exception for sth like this
                    throw new Exception(string.Format("Error: {0}", response.StatusCode));
                }
            }
        }
        public async Task<Car> UpdateCarAsync(Car car)
        {
            if (car.Guid == null || car.Guid == Guid.Empty)
            {
                throw new Exception("Car has no Guid!");
            }
            if (car.Color == null ||
                car.Model == null ||
                car.Vendor == null ||
                car.YearOfManufacture == 0)
            {
                throw new Exception("Car is not complete!");
            }

            //Create Httpclient using "using", so we do not have to call Dispose()
            using (var client = new HttpClient())
            {
                client.BaseAddress = Server;
                //serialize to json using Newtonsoft.JSON
                var json = JsonConvert.SerializeObject(car);
                //create content
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync("/api/car/" + car.Guid.ToString(), content); //DO IT!

                if (response.IsSuccessStatusCode) //this is bad practise! do not do this!
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Car>(responseString);
                }
                else
                {
                    throw new Exception(string.Format("Error: {0}", response.StatusCode));
                }
            }
        }

    }
}
