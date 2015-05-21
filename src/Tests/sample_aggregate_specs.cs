using System;
using DDD_Sample.Controllers;
using DDD_Sample.Domain;
using DDD_Sample.Infrastructure;
using NUnit.Framework;

namespace Tests
{
    public abstract class sample_aggregate_specs : SpecificationBase
    {
        protected SampleAggregate Sut;

        protected override void Given()
        {
            Sut = new SampleAggregate(null);
        }
    }

    public class when_starting_a_new_sample : sample_aggregate_specs
    {
        private Guid _sampleId;
        private string _name;

        protected override void Given()
        {
            base.Given();
            _sampleId = Guid.NewGuid();
            _name = "Some sample";
        }

        protected override void When()
        {
            Sut.Start(_sampleId, _name);
        }

        [Then]
        public void it_should_set_the_id_and_name()
        {
            var state = (Sample) ((IAggregate) Sut).GetState();
            Assert.That(state.Id, Is.EqualTo(_sampleId));
            Assert.That(state.Name, Is.EqualTo(_name));
        }
    }

    public class when_starting_a_sample_with_an_undefined_name : sample_aggregate_specs
    {
        private Guid _sampleId;
        private string _name;

        protected override void Given()
        {
            base.Given();
            _sampleId = Guid.NewGuid();
            _name = null;
            Sut.Start(Guid.NewGuid(), "Some sample");
        }

        [Then]
        public void it_should_throw()
        {
            Assert.Throws<InvalidOperationException>(() => Sut.Start(_sampleId, _name));
        }
    }
}