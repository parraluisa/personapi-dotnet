using personapi_dotnet.Controllers.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly PersonaDbContext _context;

        public PersonaRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Persona> GetAll()
        {
            return _context.Personas.ToList();
        }

        public Persona? GetById(int id)
        {
            var persona = _context.Personas.FirstOrDefault(p => p.Cc == id);
            if (persona != null)
            {
                return persona;
            }
            return null;
        }

        public void Add(Persona persona)
        {
            _context.Personas.Add(persona);
            _context.SaveChanges();
        }

        public void Update(Persona persona)
        {
            _context.Personas.Update(persona);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var persona = _context.Personas.FirstOrDefault(p => p.Cc == id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
                _context.SaveChanges();
            }
        }
    }
}
