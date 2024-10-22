using Factory_FactoryMethodPattern.Factory;

namespace Factory_FactoryMethodPattern
{
    internal class Program
    {
        //https://www.youtube.com/watch?v=5RrR6MaimT0&list=PLBEm2Vv2nD-M2oMQwQfy1TFyqqecvP8um&index=2 

        static void Main(string[] args)
        {
            IBookFactory factory = new PdfBookFactory();
            IBook pdfBook = factory.Make();
            factory = new EpubBookFactory();
            IBook epubBook = factory.Make();
            Console.ReadLine();

        }
    }
}
