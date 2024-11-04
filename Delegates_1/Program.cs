namespace Delegates_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // first way
            MathOp f = Add;
            Console.WriteLine(f(3,4));

            // second way
            Calculate(3, 4, Add);
            Calculate(3, 4, Subtract);

            // third way with lambda
            Calculate(3, 4, (x, y) => x + y);

            // fourth older way with delegate inline
            Calculate(3, 4, delegate (int x, int y) { return x * y; });
        }

        static void Calculate(int x, int y, MathOp f)
        {
            Console.WriteLine(f(x, y));
        }

        delegate int MathOp(int x, int y);
        static int Add(int x, int y)
        {
            return x + y;
        }
        static int Subtract(int x, int y)
        {
            return x - y;
        }
    }
}
