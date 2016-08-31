using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.StarWars.Types;

namespace GraphQL.StarWars
{
    public interface IStarWarsData
    {
        Task<Human> GetHumanByIdAsync(string id);

        Task<Droid> GetDroidByIdAsync(string id);

        IEnumerable<ICharacter> FriendsFor(string id);
        IEnumerable<ICharacter> FriendsFor(ICharacter character);

        Task<IEnumerable<ICharacter>> FriendsForAsync(string id);
        Task<IEnumerable<ICharacter>> FriendsForAsync(ICharacter character);
    }

    public class StarWarsData : IStarWarsData
    {
        private readonly List<Human> _humans = new List<Human>();
        private readonly List<Droid> _droids = new List<Droid>();

        public StarWarsData()
        {
            _humans.Add(new Human
            {
                Id = "1", Name = "Luke",
                Friends = new[] {"3", "4"},
                AppearsIn = new[] {4, 5, 6},
                HomePlanet = "Tatooine"
            });
            _humans.Add(new Human
            {
                Id = "2", Name = "Vader",
                AppearsIn = new[] {4, 5, 6},
                HomePlanet = "Tatooine"
            });

            _droids.Add(new Droid
            {
                Id = "3", Name = "R2-D2",
                Friends = new[] {"1", "4"},
                AppearsIn = new[] {4, 5, 6},
                PrimaryFunction = "Astromech"
            });
            _droids.Add(new Droid
            {
                Id = "4", Name = "C-3PO",
                AppearsIn = new[] {4, 5, 6},
                PrimaryFunction = "Protocol"
            });
        }

        public IEnumerable<ICharacter> FriendsFor(string id)
        {
            if (id == null)
            {
                return null;
            }

            return FriendsFor(
                (ICharacter)_humans.FirstOrDefault(h => h.Id == id) ??
                (ICharacter)_droids.FirstOrDefault(d => d.Id == id)
            );
        }

        public IEnumerable<ICharacter> FriendsFor(ICharacter character)
        {
            if (character == null)
            {
                return null;
            }

            var friends = new List<ICharacter>();
            var lookup = character.Friends;
            if (lookup != null)
            {
                _humans.Where(h => lookup.Contains(h.Id)).Apply(friends.Add);
                _droids.Where(d => lookup.Contains(d.Id)).Apply(friends.Add);
            }
            return friends;
        }

        public Task<IEnumerable<ICharacter>> FriendsForAsync(string id)
        {
            return Task.FromResult(FriendsFor(id));
        }

        public Task<IEnumerable<ICharacter>> FriendsForAsync(ICharacter character)
        {
            return Task.FromResult(FriendsFor(character));
        }

        public Task<Human> GetHumanByIdAsync(string id)
        {
            return Task.FromResult(_humans.FirstOrDefault(h => h.Id == id));
        }

        public Task<Droid> GetDroidByIdAsync(string id)
        {
            return Task.FromResult(_droids.FirstOrDefault(h => h.Id == id));
        }
    }
}
