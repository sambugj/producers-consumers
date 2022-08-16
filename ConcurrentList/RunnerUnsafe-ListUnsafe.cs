using DataBuffer;
using ThreadProducerConsumer;

namespace MainProgram
{
    internal class RunnerUnsafe_ListUnsafe
    {
        public static void Start(int producers, int consumers, int itemsToProduce)
        {
            Console.WriteLine("Concurrent ProducerConsumerUnsafe(ListUnsafe)");

            var runner = new ProducerConsumerUnsafe(new ListUnsafe(), producers, consumers, itemsToProduce);

            Console.WriteLine("created with ListUnsafe");

            runner.Run();

            Console.WriteLine("RunnerUnsafe_ListUnsafe ENDED!");
        }
    }
}
