using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;
[Table("tb_m_batchclass")]
public class BatchClass
{
    [Key, Column("token_kelas", TypeName = "nchar(7)")]
    public int TokenKelas { get; set; }
    public int NoBatch { get; set; }
    public string? NamaBatch { get; set; }
    public string? JenisKelas { get; set; }
    public int PIC { get; set; }

    //Relasi
    [JsonIgnore]
    public ICollection<Participant>? Participant { get; set; }
    [JsonIgnore]
    public ICollection<Materi>? Materi{ get; set; }
    [JsonIgnore]
    [ForeignKey("PIC")]
    public Employee? Employee { get; set; }
}
