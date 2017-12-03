using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESCde.Cars.Model
{
    public class Car
    {
        public Guid Guid { get; }
        public string Vendor { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public int YearOfManufacture { get; set; }

        public Car()
        {
            Guid = Guid.NewGuid();
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}, Color: {3}", YearOfManufacture, Vendor, Model, Color);
        }

    }
}
