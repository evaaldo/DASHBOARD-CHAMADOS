using System.ComponentModel.DataAnnotations;

public class Chamado
{
    [Required]
    public int ID { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    [DataType(DataType.DateTime)]
    public string DataAbertura { get; set; } = string.Empty;
    public string Situacao { get; set; } = string.Empty;
}