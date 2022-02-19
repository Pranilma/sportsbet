using CodeChallenge.Service.Exceptions;

namespace CodeChallenge.Service.Model
{
    public class Player
    {
        public Player(string name, int number)
        {
            if (number <= 0 || string.IsNullOrWhiteSpace(name))
                throw new InvalidPlayerException();

            Name = name;
            Number = number;
        }
        public int Number { get; }
        public string Name { get; }
    }
}
