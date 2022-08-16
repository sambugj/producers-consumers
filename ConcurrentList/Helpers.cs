namespace MainProgram
{
    internal class Helpers
    {
        static public int Producers(int p) => NaturalNumber(p);
        static public int Consumers(int c) => NaturalNumber(c);
        static private int NaturalNumber(int n) => n > 0 ? n : 1;
    }
}
