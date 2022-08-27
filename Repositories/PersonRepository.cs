using MediatRSample.Application.Models;

namespace MediatRSample.Repositories
{
    public class PersonRepository : IRepository<Person>
    {
        private static Dictionary<int, Person> people = new Dictionary<int, Person>();

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await Task.Run(() => people.Values.ToList());
        }

        public async Task<Person> Get(int id)
        {
            return await Task.Run(() => people.GetValueOrDefault(id));
        }

        public async Task Add(Person person)
        {
            await Task.Run(() => people.Add(person.Id, person));
        }

        public async Task Edit(Person person)
        {
            await Task.Run(() =>
            {
                people.Remove(person.Id);
                people.Add(person.Id, person);
            });
        }

        public async Task Delete(int id)
        {
            await Task.Run(() => people.Remove(id));
        }
    }
}
