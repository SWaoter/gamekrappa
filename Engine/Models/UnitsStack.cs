namespace Engine.Models
{
    public class UnitsStack
    {
        public Unit Type { get;}
        public int Count { get;}
        public UnitsStack(Unit type, int count)
        {
            Type = new Unit(type);
            Count = count;
        }
        public UnitsStack(UnitsStack other)
        {
            Type = new Unit(other.Type);
            Count = other.Count;
        }
        public UnitsStack() { }
    }
}
