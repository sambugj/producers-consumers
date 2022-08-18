namespace DataBuffer
{
    public class ListUnsafe : IList
    {
        private const int ItemsToShow = 10;

        private readonly List<string> _list = new();

        public virtual void AddItem(string item) => _list.Add(item);

        public virtual void RemoveItem(int index) => _list.RemoveAt(index);

        public virtual int Count
        {
            get { return _list.Count; }
        }

        public override string ToString() => $"[{string.Join(", ", _list.Take(ItemsToShow).ToList())}{AddDots()}] - Count: {Count}";

        private string AddDots() => Count > ItemsToShow ? "..." : string.Empty;
    }
}
