using System;
using FluentNHibernate.Automapping;

namespace DDD_Sample.Infrastructure
{
    public class AutoMappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type != typeof(BaseModel) &&
                   typeof(BaseModel).IsAssignableFrom(type);
        }
    }
}