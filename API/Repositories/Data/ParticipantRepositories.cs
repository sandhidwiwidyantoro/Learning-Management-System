using API.Contexts;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data;

public class ParticipantRepositories : GeneralRepository<Participant>
{
    private MyContext _context;
    public ParticipantRepositories(MyContext context) : base(context)
    {
        _context = context;
    }
	public IEnumerable<Participant> GetByNIK(int nik)
	{
		var result = _context.Participant.Where(e => e.NIK == nik);

		return result;
	}
}