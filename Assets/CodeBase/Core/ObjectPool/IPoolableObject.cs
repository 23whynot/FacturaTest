namespace CodeBase.Core.ObjectPool
{
    public interface IPoolableObject
    {
        public bool IsActive {get; }
        public void Activate();
        public void Deactivate();
    }
}
