namespace be_flutter_nhom2.Models;

public class Food
{
    public int Id { get; set; } // Khóa chính
    public string Name { get; set; } // Tên thực phẩm
    public string ImageUrl { get; set; } // Tên thực phẩm
    public double Calories { get; set; } // Lượng calo (kcal)
    public double Protein { get; set; } // Hàm lượng protein (g)
    public double Carbs { get; set; } // Hàm lượng carbohydrate (g)
    public double Fat { get; set; } // Hàm lượng chất béo (g)
    public double Alcohol { get; set; } // Hàm lượng chất béo (g)
    public bool Status { get; set; }
}