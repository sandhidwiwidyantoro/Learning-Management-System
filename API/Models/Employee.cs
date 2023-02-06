using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;

namespace API.Models;
[Table("tb_m_employee")]
public class Employee
{
    [Key, Column("nik", TypeName = "nchar(5)")]
    public int NIK { get; set; }
    [Required, Column("first_name"), MaxLength(30)]
    public string? FirstName { get; set; }
    [Column("last_name"), MaxLength(30)]
    public string? LastName { get; set; }
    [Required, Column("gender")]
    public Gender? Gender { get; set; }
    [Required, Column("birth_date", TypeName = "date")]
    public DateTime? BirthDate { get; set; }
    [Required, Column("email"), MaxLength(50)]
    public string? Email { get; set; }
    [Required, Column("password")]
    public string? Password { get; set; }
    [Required, Column("id_role")]
    public Roles Role { get; set; }
    [Required, Column("is_active")]
    public Boolean IsActive { get; set; }

    //Relasi
    [JsonIgnore]
    public Participant? Participant { get; set; }
    [JsonIgnore]
    public ICollection<BatchClass>? BatchClass { get; set; }
}

public enum Gender
{
    Male,
    Female
}

public enum Roles
{
    Admin,
    Trainer,
    Participant
}