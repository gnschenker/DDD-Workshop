using System;

namespace DDD_Sample.Infrastructure
{
    public interface IRepository
    {
        TAggregate GetById<TAggregate, TState>(Guid id) where TAggregate : IAggregate;
        void Save(IAggregate aggregate);
    }
}