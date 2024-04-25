using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces
{
    public interface ITelefonoRepository
    {
        IEnumerable<Telefono> GetAll();
        Telefono? GetByNumber(string numero);
        IEnumerable<Telefono> GetByDuenio(int id);
        void Add(Telefono telefono);
        void Update(Telefono telefono);
        void Delete(string numero);
    }

}
