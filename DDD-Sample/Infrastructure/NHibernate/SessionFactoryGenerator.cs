using System.Collections.Generic;
using System.Data;
using System.Linq;
using DDD_Sample.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using NHibernate;

namespace DDD_Sample.Infrastructure
{
    public interface ISessionFactoryGenerator
    {
        ISessionFactory CreateSessionFactory(bool showSql = false);
    }

    public class SessionFactoryGenerator : ISessionFactoryGenerator
    {
        private readonly INHibernateSettings _settings;
        private readonly IEnumerable<IConvention> _mappingConventions;

        public SessionFactoryGenerator(INHibernateSettings settings, IEnumerable<IConvention> mappingConventions)
        {
            _settings = settings;
            _mappingConventions = mappingConventions;
        }

        public ISessionFactory CreateSessionFactory(bool showSql = false)
        {
            var fluentConfiguration = GetConfiguration(showSql);
            var sessionFactory = fluentConfiguration.BuildSessionFactory();
            return sessionFactory;
        }


        private FluentConfiguration GetConfiguration(bool showSql = false)
        {
            var cfg = new AutoMappingConfiguration();

            var mapping = AutoMap.AssemblyOf<Sample>(cfg)
                .IgnoreBase<BaseModel>()
                .Conventions.Add(_mappingConventions.ToArray());

            var msSqlConfiguration = MsSqlConfiguration.MsSql2008.ConnectionString(_settings.ConnectionString);
            if (showSql) msSqlConfiguration = msSqlConfiguration.ShowSql();

            return Fluently.Configure()
                .ExposeConfiguration(x =>
                {
                    x.SetProperty("current_session_context_class", _settings.CurrentSessionContextClass);
                    x.SetProperty("connection.isolation", IsolationLevel.ReadUncommitted.ToString());
                    x.SetProperty("default_schema", _settings.DefaultSchema);
                    x.SetProperty("cache.use_query_cache", "true");
                    x.SetProperty("adonet.batch_size", _settings.AdonetBatchSize);
                })
                .Database(msSqlConfiguration)
                .Mappings(m => m.AutoMappings.Add(mapping));
        }
    }
}