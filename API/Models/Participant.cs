using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;
[Table("tb_m_participant")]
public class Participant
{
    [Key, Column("nik", TypeName = "nchar(5)")]
    public int NIK { get; set; }
    public int FinalScore { get; set; }
    public int IdBatchClass{ get; set; }

    //Relasi
    [JsonIgnore]
    [ForeignKey("NIK")]
    public Employee? Employee{ get; set; }
    [JsonIgnore]
    [ForeignKey("IdBatchClass")]
    public BatchClass? BatchClass { get; set; }
    [JsonIgnore]
    public ICollection<ParticipantTugas>? ParticipantTugas { get; set; }
}
