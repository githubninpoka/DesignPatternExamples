namespace Factory_AbstractFactoryPatternExplanation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // this pattern not implemented yet.
            /*
             * 
             * https://www.youtube.com/watch?v=8IGS5vCEG1A&list=PLBEm2Vv2nD-M2oMQwQfy1TFyqqecvP8um&index=3
             * 12:48 describes the classes and their roles:
             * 
             * What strikes me about the abstract factory pattern is this: it seems to be a combination of factory pattern (the factory just returns a concrete implementation based on your wishes, like below:)
             * -- IAbstract type = factory.GetObject([ dog || cat ]);  and the factory based on the input, usually matches against a switch statement and decided to instantiate an object based on that.
             * 
             * now the factory method pattern is slightly different. in it, you instantiate a factory of a certain type, from the client.
             * client: IFactory dogFactory = new DogFactory();
             * client: IAnimal dog = dogFactory.Make();
             * -- the factory has a virtual and an implemented method, both enforced through the IFactory interface.
             * -- the factory is called through the virtual method, and the virtual method returns the object that the abstract/implemented method creates. taking away the implementation details from the client.
             * 
             * now the abstract factory method:
             * you have the interface factory.
             * the interface factory describes that there is to be a GetDog() method and a GetTiger() method.
             * the concrete factory (PetAnimalFactory or WildAnimalFactory) implements both methods, but differently because one is wild and the other is pet.
             * client: IAnimalFactory wildAnimalFactory = new WildAnimalFactory();
             * client: IWildAnimal wildDog = wildAnimalFactory.GetDog();
             * 
             * so we still know that WildDog, WildTiger, PetDog, PetTiger, will implement ITiger or IDog. (it could even be IAnimal)
             * and whatever type of dog you instantiate, you can use the known methods for any dog.
             * depending on the factory that you instantiate, you can create ([wilddog || wildtiger] || [petdog || pettiger])
             */

        }
    }
}
