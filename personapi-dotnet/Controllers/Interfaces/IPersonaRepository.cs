using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.Interfaces
{
    public interface IPersonaRepository
    {
        IEnumerable<Persona> GetAll();
        Persona? GetById(int id);
        void Add(Persona persona);
        void Update(Persona persona);
        void Delete(int id);
    }
}
