namespace DataBuffer
{
    public class ListWithLock : ListUnsafe
    {
        private readonly object _lockObj;

        public ListWithLock() : base()
        {
            _lockObj = new();
        }

        public override void AddItem(string item)
        {
            lock (_lockObj)
            {
                base.AddItem(item);
            }
        }

        public override void RemoveItem(int index)
        {
            lock (_lockObj)
            {
                base.RemoveItem(index);
            }
        }

        public override int Count
        {
            get
            {
                lock (_lockObj)
                {
                    return base.Count;
                }
            }
        }

        public override string ToString()
        {
            lock (_lockObj)
            {
                return base.ToString();
            }
        }
    }
}
