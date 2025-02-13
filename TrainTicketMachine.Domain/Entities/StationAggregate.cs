using TrainTicketMachine.Domain.ValueObjects;

namespace TrainTicketMachine.Domain.Entities
{
    public class StationAggregate
    {
        public StationName Name { get; private set; }
        public List<char> NextCharacters { get; private set; } = new List<char>();

        public StationAggregate(string name)
        {
            Name = new StationName(name);
        }

        public void SetNextCharacters(IEnumerable<char> characters)
        {
            NextCharacters = characters.Distinct().OrderBy(c => c).ToList();
        }
    }
}
