using DataBuffer;
using ThreadProducerConsumer;

namespace MainProgram
{
    internal class RunnerSafe_ListWithLock
    {
        public static void Start(int producers, int consumers, int itemsToProduce)
        {
            Console.WriteLine("Concurrent ProducerConsumerSafeWithLock(ListWithLock)");

            var runner = new ProducerConsumerSafeWithLock(new ListWithLock(), producers, consumers, itemsToProduce);

            Console.WriteLine("created with ListWithLock");

            runner.Run();

            Console.WriteLine("RunnerSafe_ListWithLock ENDED!");
        }
    }
}
