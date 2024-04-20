using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

public class ProfesionRepository : IProfesionRepository
{
    private readonly PersonaDbContext _context;

    public ProfesionRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Profesion> GetAll()
    {
        return _context.Profesions.ToList();
    }

    public Profesion? GetById(int id)
    {
        var profesion = _context.Profesions.FirstOrDefault(p => p.Id == id);
        if (profesion != null)
        {
            return profesion;
        }
        return null;
    }

    public void Add(Profesion profesion)
    {
        _context.Profesions.Add(profesion);
        _context.SaveChanges();
    }

    public void Update(Profesion profesion)
    {
        _context.Profesions.Update(profesion);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var profesion = _context.Profesions.FirstOrDefault(p => p.Id == id);
        if (profesion != null)
        {
            _context.Profesions.Remove(profesion);
            _context.SaveChanges();
        }
    }

}