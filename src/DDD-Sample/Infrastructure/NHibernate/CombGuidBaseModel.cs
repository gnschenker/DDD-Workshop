using System;

namespace DDD_Sample.Infrastructure
{
    public abstract class CombGuidBaseModel : BaseModel
    {
        protected CombGuidBaseModel()
        {
            Id = CombGuid.Generate();
        }

        public virtual Guid Id { get; set; }

        public virtual bool IsTransient()
        {
            return Id == CombGuid.Empty;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CombGuidBaseModel;

            if (other == null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return !IsTransient() && Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return IsTransient() ? base.GetHashCode() : Id.GetHashCode();
        }
    }
}