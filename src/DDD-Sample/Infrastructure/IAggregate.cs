namespace DDD_Sample.Infrastructure
{
    public interface IAggregate
    {
        object GetState();
    }
}