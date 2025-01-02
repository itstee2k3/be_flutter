using Newtonsoft.Json;

namespace be_flutter_nhom2.DTOs;

public class FoodResponse
{
    [JsonProperty("fdcId")]
    public int FdcId { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("imageUrl")]
    public string ImageUrl { get; set; }

    [JsonProperty("dataType")]
    public string DataType { get; set; }

    [JsonProperty("publicationDate")]
    public string PublicationDate { get; set; }

    [JsonProperty("foodCode")]
    public string FoodCode { get; set; }
}