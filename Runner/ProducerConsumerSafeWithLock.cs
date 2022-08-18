using DataBuffer;

namespace ThreadProducerConsumer
{
    public class ProducerConsumerSafeWithLock : ProducerConsumerBase
    {       
        private readonly object _producerLockObj;
        private readonly object _consumerLockObj;

        public ProducerConsumerSafeWithLock(IList list, int producers, int consumers, int itemsToProduce) :
            base(list, producers, consumers, itemsToProduce)
        {   
            _producerLockObj = new();
            _consumerLockObj = new();
        }

        protected override void ThreadProducer()
        {
            while (true)
            {
                var product = string.Empty;
                
                lock (_producerLockObj)
                {
                    if (_produced >= ItemsToProduce)
                        return; // END!

                    product = $"P-{++_produced}";
                }
                
                _list.AddItem(product);

                Thread.Sleep(0); // Force the scheduler to change the context to another thread
            }
        }

        protected override void ThreadConsumer()
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
    }
}