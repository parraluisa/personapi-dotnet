using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        private readonly PersonaDbContext _context;

        public EstudioRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Estudio> GetAll()
        {
            return _context.Estudios.ToList();
        }
        public Estudio? GetById(int ccPer, int idProf)
        {
            var estudio = _context.Estudios.FirstOrDefault(e => e.CcPer == ccPer && e.IdProf == idProf);
            if (estudio != null)
            {
                return estudio;
            }
            return null;
        }

        public IEnumerable<Estudio> GetAllByIdProf(int idProf)
        {
            var estudios = _context.Estudios.Where(e => e.IdProf == idProf).ToList();
            if (estudios.Any())
            {
                return estudios;
            }
            return null;
            
        }
        public IEnumerable<Estudio> GetAllByCcPer(int CcPer)
        {
            var estudios = _context.Estudios.Where(e => e.CcPer == CcPer).ToList();
            if (estudios.Any())
            {
                return estudios;
            }
            return null;

        }

        public void Add(Estudio estudio)
        {
            _context.Estudios.Add(estudio);
            _context.SaveChanges();
        }

        public void Update(Estudio estudio)
        {
            _context.Estudios.Update(estudio);
            _context.SaveChanges();
        }

        public void Delete(int ccPer, int idProf)
        {
            var estudio = _context.Estudios.FirstOrDefault(e => e.CcPer == ccPer && e.IdProf == idProf);
            if (estudio != null)
            {
                _context.Estudios.Remove(estudio);
                _context.SaveChanges();
            }
        }
    }
}
