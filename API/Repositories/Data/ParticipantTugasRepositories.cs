using API.Contexts;
using API.Models;
using API.ViewModels;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Repositories.Data;

public class ParticipantTugasRepositories : GeneralRepository<ParticipantTugas>
{
    private readonly MyContext _context;
    public ParticipantTugasRepositories(MyContext context) : base(context)
    {
        _context = context;
    }
    public int UploadKumpulTugas(ParticipantTugas entity)
    {
        _context.ParticipantTugas.Add(entity);
        var result = _context.SaveChanges();
        return result;
    }
    public IEnumerable<Tugas> TugasBelum(int idpeserta, int tokenkelas)
    {
        var data = _context.ParticipantTugas.Where(a => a.IdPeserta == idpeserta).Select(a => a.IdTugas).ToList();

        var result = from tug in _context.Tugas
                     join mat in _context.Materi on tug.IdMateri equals mat.Id
                     join bat in _context.BatchClass on mat.TokenKelas equals bat.TokenKelas
                     where !data.Contains(tug.Id) && mat.TokenKelas == tokenkelas
                     select new Tugas
                     {
                         Id = tug.Id,
                         NamaTugas = tug.NamaTugas,
                         Judul = tug.Judul,
                         NamaFile = tug.NamaFile,
                         DescTugas = tug.DescTugas,
                         IdMateri = tug.IdMateri
                     };

        return result;
    }

    public IEnumerable<TugasBelumVM> TugasBeres(int idpeserta, int tokenkelas)
    {
        var data = _context.ParticipantTugas.Where(a => a.IdPeserta == idpeserta).Select(a => a.IdTugas).ToList();

        var result = from tug in _context.Tugas
                     join ptug in _context.ParticipantTugas on tug.Id equals ptug.IdTugas
                     join mat in _context.Materi on tug.IdMateri equals mat.Id
                     join bat in _context.BatchClass on mat.TokenKelas equals bat.TokenKelas
                     where data.Contains(tug.Id) && mat.TokenKelas == tokenkelas
                     select new TugasBelumVM
                     {
                         Id = ptug.Id,
                         NamaTugas = tug.NamaTugas,
                         Judul = tug.Judul,
                         NamaFile = ptug.NamaFile,
                         DescTugas = tug.DescTugas,
                         IdMateri = tug.IdMateri,
                         Tokenkelas = mat.TokenKelas
                     };

        return result;
    }

    public IEnumerable<ParticipantTugas> GetByIdTugas(int idTugas)
    {
        var result = _context.ParticipantTugas.Where(a => a.IdTugas== idTugas).ToList();

        return result;
    }
}