using System.ComponentModel.DataAnnotations;

namespace AMassage.Models;

public class Massage
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Необходимо ввести название.")]
    public string Name { get; set; }
    public string Description { get; set; }
}