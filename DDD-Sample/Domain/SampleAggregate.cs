using System;
using DDD_Sample.Infrastructure;

namespace DDD_Sample.Domain
{
    public class SampleAggregate : IAggregate
    {
        private readonly Sample _state;

        public SampleAggregate(Sample state)
        {
            _state = state ?? new Sample();
        }

        public object GetState()
        {
            return _state;
        }

        public void Start(Guid id, string name)
        {
            _state.Id = id;
            _state.Name = name;
        }

        public void Next(DateTime someDate)
        {
            _state.SomeDate = someDate;
        }
    }

    public class Sample : CombGuidBaseModel
    {
        public virtual string Name { get; set; }
        public virtual DateTime? SomeDate { get; set; }
    }
}