using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces
{
    public interface IEstudioRepository
    {
        IEnumerable<Estudio> GetAll();
        Estudio? GetById(int ccPer, int idProf);
        IEnumerable<Estudio> GetAllByIdProf(int idProf);
        IEnumerable<Estudio> GetAllByCcPer(int CcPer);
        void Add(Estudio estudio);
        void Update(Estudio estudio);
        void Delete(int ccPer, int idprof);
    }
}
