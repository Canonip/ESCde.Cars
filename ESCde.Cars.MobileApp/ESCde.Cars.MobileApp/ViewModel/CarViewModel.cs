using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XColor = Xamarin.Forms.Color;
using SColor = System.Drawing.Color;


namespace ESCde.Cars.MobileApp.ViewModel
{
    public class CarViewModel
    {
        private XColor _color;
        private string _colortext;
        public string Id { get; set; }
        public string ColorText
        {
            get { return _colortext; }
            set
            {
                //Hotfix, because Xamarin Color does not convert from string
                _colortext = value;
                var syscolor = SColor.FromName(_colortext);
                _color = XColor.FromRgb(syscolor.R, syscolor.G, syscolor.B);
            }
        }
        public string Description { get; set; }
        public XColor Color { get { return _color; } } 

    }
}
