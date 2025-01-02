using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace be_flutter_nhom2.Models;

public class Food
{
    [Key]
    public int FdcId { get; set; } 
    public string Description { get; set; } 
    public string ImageUrl { get; set; }
    public string DataType { get; set; }
    public string PublicationDate { get; set; }
    public string FoodCode { get; set; }
    public List<FoodNutrient> FoodNutrients { get; set; } 
}

public class FoodNutrient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]  // Không tự động sinh ID
    public int Id { get; set; } 
    public int Number { get; set; } 
    public string Type { get; set; }
    public string Name { get; set; }
    public string UnitName { get; set; }
    public double Amount { get; set; }
    public Nutrient Nutrient { get; set; }  // Thông tin dinh dưỡng chi tiết

    [JsonIgnore]
    public int FdcId { get; set; }  // Khóa ngoài liên kết với Food

    [JsonIgnore]
    public Food Food { get; set; }
}

public class Nutrient
{
    public int Id { get; set; }  // ID dinh dưỡng (từ API)
    public int Number { get; set; }  // Mã số dinh dưỡng (Ví dụ: 203)
    public string Name { get; set; }  // Tên dinh dưỡng (Ví dụ: Protein)
    public string UnitName { get; set; }  // Đơn vị của dinh dưỡng (g)
    public int Rank { get; set; }  // Rank của dinh dưỡng (nếu có)
}