using API.Contexts;
using API.Handler;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Repositories.Data;

public class EmployeeRepositories : GeneralRepository<Employee>
{
    private MyContext _context;
    private DbSet<Employee> _employee;
    private DbSet<Participant> _participant;

    public EmployeeRepositories(MyContext context) : base(context)
    {
        _context = context;
        _employee = context.Set<Employee>();
        _participant = context.Set<Participant>();
    }

    public int Login(LoginVM login)
    {
        var result = _employee.Select(e => new LoginVM { Email = e.Email, Password = e.Password }).SingleOrDefault(e => e.Email == login.Email);

        if (result == null)
        {
            return 0;
        }
        if (login.Password != result.Password)
        {
            return 1;
        }
        return 2;
    }

    public int Registrasi(RegistrasiVM entity)
    {
        var result = 0;
        var email = _employee.Where(e => e.Email == entity.Email);
        var cektoken = _context.BatchClass.Where(e => e.TokenKelas == entity.TokenKelas);
        if (email.Count() != 0)
        {
            return 0;
        }
        else if (cektoken.Count() == 0)
        {
            return 1;
        }
        var emp = new Employee()
        {
            NIK = entity.NIK,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Password = entity.Password,
            Gender = entity.Gender,
            BirthDate = entity.BirthDate,
            Email = entity.Email,
            Role = Roles.Participant,
            IsActive = true
        };
        _employee.Add(emp);
        _context.SaveChanges();

        var peserta = new Participant()
        {
            NIK = entity.NIK,
            FinalScore = 0,
            IdBatchClass = entity.TokenKelas
        };
        _participant.Add(peserta);
        _context.SaveChanges();

        return 2;
    }

    public string UserRoles(string email)
    {
        var result = _employee.Where(a => a.Email == email).Select(e => e.Role).First();

        var hasil = result.ToString();

        return hasil;

    }

    public string GetNIK(string email)
    {
        var result = _employee.Where(a => a.Email == email).Select(e => e.NIK).First();

        var hasil = result.ToString();

        return hasil;

    }

    public string GetTokenKelas(int nik)
    {
        var result = _context.Participant.Where(a => a.NIK == nik).Select(e => e.IdBatchClass).First();

        var hasil = result.ToString();

        return hasil;

    }
    public IEnumerable<Employee> GetByRole(Roles role)
    {
        var result = _employee.Where(a => a.Role == role)
            .Select(e => new Employee
            {
                NIK = e.NIK,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                BirthDate = e.BirthDate,
                Gender = e.Gender,
                BatchClass = e.BatchClass,
                Role = e.Role,
                Password = e.Password,
                IsActive = e.IsActive 
            });

        return result;

    }
}
