namespace DataBuffer
{
    public interface IList
    {
        void AddItem(string item);
        void RemoveItem(int index);
        int Count { get; }
        string ToString();
    }
}
