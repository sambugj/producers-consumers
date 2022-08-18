using DataBuffer;
using ThreadProducerConsumer;

namespace MainProgram
{
    internal class RunnerUnsafe_ListWithLock
    {
        public static void Start(int producers, int consumers, int itemsToProduce)
        {
            Console.WriteLine("Concurrent ProducerConsumerUnsafe(ListWithLock)");

            var runner = new ProducerConsumerUnsafe(new ListWithLock(), producers, consumers, itemsToProduce);

            Console.WriteLine("created with ListWithLock");

            runner.Run();

            Console.WriteLine("RunnerUnsafe_ListWithLock ENDED!");
        }
    }
}
