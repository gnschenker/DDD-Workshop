using System;
using NHibernate;

namespace DDD_Sample.Infrastructure
{
    public class NHibernateRepository : IRepository
    {
        private readonly ISession _session;

        public NHibernateRepository(ISession session)
        {
            _session = session;
        }

        public TAggregate GetById<TAggregate, TState>(Guid id) where TAggregate : IAggregate
        {
            var state = _session.Get<TState>(id);
            return (TAggregate)Activator.CreateInstance(typeof(TAggregate), new object[] { state });
        }

        public void Save(IAggregate aggregate)
        {
            var state = aggregate.GetState();
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(state);
                tx.Commit();
            }
        }
    }
}