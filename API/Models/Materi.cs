
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models;
[Table("tb_m_materi")]
public class Materi
{
    [Key, Column("id")]
    public int Id { get; set; }
    public string NamaMateri { get; set; }
    public string Judul { get; set; }
    public string NamaFile { get; set; }
    [Column("desc_materi", TypeName = "text")]
    public string DescMateri { get; set; }
    public int TokenKelas { get; set; }

    //Relasi
    [JsonIgnore]
    [ForeignKey("TokenKelas")]
    public BatchClass? BatchClass { get; set; }
    [JsonIgnore]
    public ICollection<Tugas>? Tugas { get; set; }
}
