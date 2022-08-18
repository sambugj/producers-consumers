using DataBuffer;

namespace ThreadProducerConsumer
{
    public class ProducerConsumerUnsafe : ProducerConsumerBase
    {
        public ProducerConsumerUnsafe(IList list, int producers, int consumers, int itemsToProduce) :
            base(list, producers, consumers, itemsToProduce) {}

        protected override void ThreadProducer()
        {
            while (true)
            {   
                if (_produced >= ItemsToProduce)
                    return; // END!

                _list.AddItem($"P-{++_produced}");
                
                Thread.Sleep(0);
            }
        }

        protected override void ThreadConsumer()
        {
            while (true)
            {
                if (_consumed >= ItemsToProduce)
                        return; // END!

                if (_list.Count > 0)
                {
                    // remove the last item
                    _list.RemoveItem(_list.Count - 1); 
                    _consumed++;
                }

                Thread.Sleep(0);
            }
        }
    }
}