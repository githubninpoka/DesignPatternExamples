using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_Builder1Pattern
{
    internal class DogBuilder
    {
        private Dog _dog;

        public DogBuilder()
        {
            _dog = new Dog();
            _dog.DogFeet = new DogFeet(); // this is a hidden action done by the builder, abstracted away from the client.
        }

        public void GiveEyes(string color)
        {
            _dog.Eyes = color;
        }

        public void GiveEars(string color)
        {
            _dog.Ears = color;
        }
        public Dog Build()
        {
            return _dog;
        }
    }
}
