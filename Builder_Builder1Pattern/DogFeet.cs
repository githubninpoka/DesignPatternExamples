using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_Builder1Pattern
{
    internal class DogFeet
    {
        public int NumberOfFeet { get; set; }

        public DogFeet(int numberOfFeet=4)
        {
            NumberOfFeet = numberOfFeet;
        }
    }
}
