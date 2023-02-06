namespace API.ViewModels;

public class TugasTokenVM
{
    public int Id { get; set; }
    public string? NamaTugas { get; set; }
    public string? Judul { get; set; }
    public string? NamaFile { get; set; }
    public string? DescTugas { get; set; }
    public int IdMateri { get; set; }
    public int Tokenkelas { get; set; }
}
