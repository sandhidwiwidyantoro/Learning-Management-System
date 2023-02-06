namespace API.Models;

public class RegistrasiVM
{
    public int NIK { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public int TokenKelas { get; set; }
}