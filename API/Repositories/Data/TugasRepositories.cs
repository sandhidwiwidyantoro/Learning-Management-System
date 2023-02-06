using API.Contexts;
using API.Models;
using API.ViewModels;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data;

public class TugasRepositories : GeneralRepository<Tugas>
{
    private MyContext _context;
    public TugasRepositories(MyContext context) : base(context)
    {
        _context = context; 
	}

    public IEnumerable<TugasTokenVM> GetByToken(int tokenkelas)
    {
        var result = _context.Tugas.Join(_context.Materi, e => e.IdMateri, u => u.Id,
            (e, u) => new TugasTokenVM()
            {
                Id = e.Id,
                NamaTugas= e.NamaTugas,
                Judul = e.Judul,
                NamaFile = e.NamaFile,
                DescTugas= e.DescTugas,
                IdMateri = e.IdMateri,
                Tokenkelas = u.TokenKelas
            }).Where(s=> s.Tokenkelas == tokenkelas);

        return result;
    }

    public int UploadTugas(Tugas entity)
	{
		_context.Tugas.Add(entity);
		var result = _context.SaveChanges();
		return result;
	}
}