using ESCde.Cars.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESCde.Cars.Backend.Model
{
    public class Database
    {
        private static Database _instance;

        public static Database Instance
        {
            get
            {
                if (_instance == null) _instance = new Database();
                return _instance;
            }
        }

        public List<Car> Cars { get; set; } = new List<Car>();
    }
}
