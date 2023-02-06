using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;
[Table("tb_m_tugas")]
public class Tugas
{
    [Key, Column("id")]
    public int Id { get; set; }
    public string? NamaTugas { get; set; }
    public string? Judul { get; set; }
    public string NamaFile { get; set; }
    [Column("desc_tugas", TypeName = "text")]
    public string? DescTugas{ get; set; }
    public int IdMateri { get; set; }

    // Relasi
    [JsonIgnore]
    [ForeignKey("IdMateri")]
    public Materi? Materi { get; set; }
    [JsonIgnore]
    public ICollection<ParticipantTugas>? ParticipantTugas { get; set; }
}
