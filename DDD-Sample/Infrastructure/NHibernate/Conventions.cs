using System;
using FluentNHibernate;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace DDD_Sample.Infrastructure
{
    public class IdConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.GeneratedBy.Assigned();
        }
    }

    public class FKConvention : ForeignKeyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
        }

        protected override string GetKeyName(Member property, Type type)
        {
            return type.Name + "Id";
        }
    }

    public class HasManyCascadeConvention : IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Cascade.AllDeleteOrphan();
        }
    }

    public class ReferencesCascadeConvention : IReferenceConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            instance.Cascade.SaveUpdate();
        }
    }

    public class PropertyNameConvention : IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            if (instance.Property.Name.Equals("Key", StringComparison.OrdinalIgnoreCase))
                instance.Column(String.Format("[{0}]", instance.Property.Name));
        }
    }
}