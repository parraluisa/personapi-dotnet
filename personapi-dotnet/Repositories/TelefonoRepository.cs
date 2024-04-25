using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories
{
    public class TelefonoRepository : ITelefonoRepository
    {
        private readonly PersonaDbContext _context;

        public TelefonoRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Telefono> GetAll()
        {
            return _context.Telefonos.ToList();
        }

        public Telefono? GetByNumber(string numero)
        {
            var telefono = _context.Telefonos.FirstOrDefault(t => t.Num == numero);
            if (telefono != null)
            {
                return telefono;
            }
            return null;
        }

        public IEnumerable<Telefono> GetByDuenio(int id)
        {
            var telefonos = _context.Telefonos.Where(t => t.Duenio == id);
            if (telefonos.Any())
            {
                return telefonos;
            }
            return null;
        }

        public void Add(Telefono telefono)
        {
            _context.Telefonos.Add(telefono);
            _context.SaveChanges();
        }

        public void Update(Telefono telefono)
        {
            _context.Telefonos.Update(telefono);
            _context.SaveChanges();
        }

        public void Delete(string numero)
        {
            var telefono = _context.Telefonos.FirstOrDefault(t => t.Num == numero);
            if (telefono != null)
            {
                _context.Telefonos.Remove(telefono);
                _context.SaveChanges();
            }
        }
    }

}
