using System.Collections.Generic;
using FluentNHibernate.Conventions;
using NHibernate;
using NHibernate.Context;
using StructureMap.Configuration.DSL;

namespace DDD_Sample.Infrastructure
{
    public class NhibernateRegistry : Registry
    {
        public NhibernateRegistry()
        {
            Scan(x =>
            {
                x.AssemblyContainingType<IdConvention>();
                x.AddAllTypesOf<IConvention>();
                x.Exclude(type => !typeof(IConvention).IsAssignableFrom(type));
            });


            For<IEnumerable<IConvention>>().Use(ctx => ctx.GetAllInstances<IConvention>());
            ForSingletonOf<ISessionFactoryGenerator>().Use<SessionFactoryGenerator>();

            ForSingletonOf<ISessionFactory>().Use(
                ctx => ctx.GetInstance<ISessionFactoryGenerator>().CreateSessionFactory());

            For<ISession>().Use((ctx) =>
            {
                var sessionFactory = ctx.GetInstance<ISessionFactory>();
                ISession session;
                if (CallSessionContext.HasBind(sessionFactory))
                {
                    session = sessionFactory.GetCurrentSession();
                }
                else
                {
                    session = sessionFactory.OpenSession();
                    CallSessionContext.Bind(session);
                }
                return session;
            });

        }
    }
}