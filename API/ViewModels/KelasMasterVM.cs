using API.Models;

namespace API.ViewModels;

public class KelasMasterVM
{
    public int TokenKelas { get; set; }
    public int IdTugas { get; set; }
    public int IdMateri{ get; set; }
    public int NIKPeserta{ get; set; }
    public int NIKPIC{ get; set; }
    public string NoBatch { get; set; }
    public string NamaBatch { get; set; }
    public string JenisKelas { get; set; }
}