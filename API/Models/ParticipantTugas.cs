using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models;
[Table("tb_r_participanttugas")]
public class ParticipantTugas
{
    [Key, Column("id")]
    public int Id { get; set; }
    public int Nilai { get; set; }
    public string NamaFile { get; set; }
    public int IdTugas { get; set; }
    public int IdPeserta { get; set;}

    //Relasi
    [JsonIgnore]
    [ForeignKey("IdPeserta")]
    public Participant? Participant{ get; set; }
    [JsonIgnore]
    [ForeignKey("IdTugas")]
    public Tugas? Tugas { get; set; }
}
