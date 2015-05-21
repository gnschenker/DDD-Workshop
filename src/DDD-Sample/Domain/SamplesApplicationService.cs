using System;
using DDD_Sample.Contracts.Commands;
using DDD_Sample.Controllers;
using DDD_Sample.Infrastructure;

namespace DDD_Sample.Domain
{
    public interface ISamplesApplicationService
    {
        Guid When(StartCommand cmd);
        void When(NextCommand cmd);
    }

    public class SamplesApplicationService : ISamplesApplicationService
    {
        private readonly IRepository _repository;

        public SamplesApplicationService(IRepository repository)
        {
            _repository = repository;
        }

        public Guid When(StartCommand cmd)
        {
            var sampleId = CombGuid.Generate();
            var agg = new SampleAggregate(null);
            agg.Start(sampleId, cmd.Name);
            _repository.Save(agg);
            return sampleId;
        }

        public void When(NextCommand cmd)
        {
            var agg = _repository.GetById<SampleAggregate, Sample>(cmd.Id);
            agg.Next(cmd.SomeDate);
            _repository.Save(agg);
        }
    }
}