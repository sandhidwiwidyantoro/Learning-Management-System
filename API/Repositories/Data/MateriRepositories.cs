using API.Contexts;
using API.Models;
using API.ViewModels;

namespace API.Repositories.Data;

public class MateriRepositories : GeneralRepository<Materi>
{
    private MyContext _context;
    public MateriRepositories(MyContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<Materi> GetByToken(int tokenkelas)
    {
        var result = _context.Materi.Where(e=> e.TokenKelas == tokenkelas);

        return result;
    }

    public int UploadMateri(Materi entity)
    {
		_context.Materi.Add(entity);
		var result = _context.SaveChanges();
		return result;
    }
}
