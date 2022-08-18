using DataBuffer;

namespace ThreadProducerConsumer
{
    public abstract class ProducerConsumerBase
    {  
        protected int ItemsToProduce;  
        protected readonly IList _list;
        protected int _produced;
        protected int _consumed;

        private int ThreadsProducerQuantity;
        private int ThreadsConsumerQuantity;
        private readonly List<Thread> _threads;
        private readonly CancellationTokenSource _cts;


        protected abstract void ThreadProducer();

        protected abstract void ThreadConsumer();

        public ProducerConsumerBase(IList list, int producers, int consumers, int itemsToProduce)
        {
            _list = list;
            _threads = new List<Thread>();
            _cts = new CancellationTokenSource();
            _produced = 0;
            _consumed = 0;
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

            ShowConfig();

            Start();

            Wait();

            Finish();

            ShowStatus();

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
                if (!token.IsCancellationRequested)
                {
                    ShowStatus();
                }
                Thread.Sleep(5000);
            }
            while (!token.IsCancellationRequested);
        }

        private void ShowConfig()
        {
            Console.WriteLine("++++++++++++++ CONFIG +++++++++++");
            Console.WriteLine($"Items To Produce: #{ItemsToProduce}");
            Console.WriteLine($"Producers: #{ThreadsProducerQuantity}");
            Console.WriteLine($"Consumers: #{ThreadsConsumerQuantity}");
        }
        private void ShowStatus()
        {
            Console.WriteLine("++++++++++++++ STATUS ++++++++++");
            Console.WriteLine($"Produced: {_produced}");
            Console.WriteLine($"Consumed: {_consumed}");
            Console.WriteLine($"Buffer: {_list}");
        }
    }
}