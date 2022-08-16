using DataBuffer;

namespace ThreadProducerConsumer
{
    public class ProducerConsumerSafeWithLock
    {
        
        private int ThreadsProducerQuantity;
        private int ThreadsConsumerQuantity;

        private int ItemsToProduce;

        private readonly List<Thread> _threads;
        private readonly CancellationTokenSource _cts;

        private readonly IList _list;
        private int _produced;
        private int _consumed;

        private readonly object _producerLockObj;
        private readonly object _consumerLockObj;

        public ProducerConsumerSafeWithLock(IList list, int producers, int consumers, int itemsToProduce)
        {
            _list = list;
            _threads = new List<Thread>();
            _cts = new CancellationTokenSource();
            _produced = 0;
            _consumed = 0;
            _producerLockObj = new();
            _consumerLockObj = new();
            ThreadsProducerQuantity = producers;
            ThreadsConsumerQuantity = consumers;
            ItemsToProduce = itemsToProduce;
        }

        public void Run()
        {
            if (ItemsToProduce < 0 || ThreadsProducerQuantity < 0 || ThreadsConsumerQuantity < 0)
            {
                Console.WriteLine($"CANNOT RUN. BAD PARAMETER!.");
                return;
            }

            ShowAll();

            Start();

            Wait();

            Finish();

            ShowAll();

            if (_list.Count == 0 && _produced == ItemsToProduce && _consumed == ItemsToProduce)
            {
                Console.WriteLine("******************************************************************");
                Console.WriteLine("******************************************************************");
                Console.WriteLine("**********************  Finished SUCCEEDED  **********************");
                Console.WriteLine("******************************************************************");
                Console.WriteLine("******************************************************************");
            }
            else
            {
                Console.WriteLine("ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!");
                Console.WriteLine("ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!");
                Console.WriteLine($"ERROR!: Finished With Error!!: The Buffer MUST BE Empty (HAS {_list.Count} ITEMS) and the data consumed and produced MUST BE: {ItemsToProduce}!!.");
                Console.WriteLine("ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!");
                Console.WriteLine("ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!ERROR!");
            }
        }

        private void Start()
        {
            StartTheMonitorThread();

            CreateWorkingThreads();

            StartWorkingThreads();
        }

        private void StartTheMonitorThread()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadMonitor), _cts.Token);
        }

        private void Wait()
        {
            WaitWorkingThreadsFinish();
        }

        private void Finish()
        {
            StopTheMonitorThread();
        }

        private void StopTheMonitorThread()
        {
            _cts.Cancel();
        }

        private void CreateWorkingThreads()
        {
            RunThread(ThreadsProducerQuantity, ThreadProducer);
            RunThread(ThreadsConsumerQuantity, ThreadConsumer);
        }
 
        private void ThreadProducer()
        {
            while (true)
            {
                lock (_producerLockObj)
                {
                    if (_produced >= ItemsToProduce)
                        return; // END!
                    
                    _list.AddItem($"P-{++_produced}");
                }

                Thread.Sleep(0); // Force the scheduler to change the context to another thread
            }
        }

        private void ThreadConsumer()
        {
            while (true)
            {
                lock (_consumerLockObj)
                {
                    if (_consumed >= ItemsToProduce)
                        return; // END!

                    if (_list.Count > 0)
                    {
                        // remove the last item
                        _list.RemoveItem(_list.Count - 1); 
                        _consumed++;
                    }
                }

                Thread.Sleep(0); // Force the scheduler to change the context to another thread.
            }
        }

        private void WaitWorkingThreadsFinish()
        {
            foreach (var thread in _threads)
                thread.Join();
        }

        private void StartWorkingThreads()
        {
            foreach (var thread in _threads)
                thread.Start();
        }

        private void RunThread(int threadsToRun, Action threadMethod)
        {
            for (int i = 0; i < threadsToRun; i++)
                _threads.Add(new Thread(new ThreadStart(threadMethod)));
        }

        private void ThreadMonitor(object? tokenObj)
        {
            if (tokenObj == null)
                return;

            var token = (CancellationToken)tokenObj;
            do
            {
                Thread.Sleep(5000);
                if (!token.IsCancellationRequested)
                {
                    ShowAll();
                }
            }
            while (!token.IsCancellationRequested);
        }

        private void ShowAll()
        {
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine($"Buffer: {_list}");
            Console.WriteLine($"Producers: #{ThreadsProducerQuantity}");
            Console.WriteLine($"Consumers: #{ThreadsConsumerQuantity}");
            Console.WriteLine($"Items To Produce: #{ItemsToProduce}");            
            Console.WriteLine($"Produced: {_produced}");
            Console.WriteLine($"Consumed: {_consumed}");
            Console.WriteLine("------------------------------------------------");
        }
    }
}