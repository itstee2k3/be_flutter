using System.ComponentModel.DataAnnotations.Schema;

namespace be_flutter_nhom2.Models;

public class Category
{
    public int Id { get; set; } 
    public string ImageUrl { get; set; }
    public string Name { get; set; }
    public string Descripton { get; set; } 
    public bool Status { get; set; }
}