using System;

namespace DDD_Sample.Contracts.Commands
{
    public class StartCommand
    {
        public string Name { get; set; }
    }

    public class NextCommand
    {
        public Guid Id { get; set; }
        public DateTime SomeDate { get; set; }
    }
}