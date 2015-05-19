namespace DDD_Sample.Infrastructure
{
    public interface INHibernateSettings
    {
        string ConnectionString { get; }
        string CurrentSessionContextClass { get; }
        string DefaultSchema { get; }
        string AdonetBatchSize { get; }
    }
}