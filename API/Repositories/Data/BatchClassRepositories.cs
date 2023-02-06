using API.Contexts;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data;

public class BatchClassRepositories : GeneralRepository<BatchClass>
{
    private MyContext _context;
    public BatchClassRepositories(MyContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<BatchClass> GetByPIC(int nik)
    {
        var result = _context.BatchClass.Where(e => e.PIC == nik);

        return result;
    }

	public IEnumerable<Employee> GetPesertaKelas(int tokenkelas)
    {
        var result = from emp in _context.Employee
                     join par in _context.Participant on emp.NIK equals par.NIK
                     where par.IdBatchClass == tokenkelas
                     select new Employee
                     {
                         NIK = emp.NIK,
                         FirstName = emp.FirstName,
                         LastName = emp.LastName,
                         BirthDate= emp.BirthDate,
                         Email= emp.Email,
                         Gender= emp.Gender,
                         IsActive= emp.IsActive,
                         Role = emp.Role,
                         Password= emp.Password,
                     };
        return result;
    }


}