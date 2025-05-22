namespace SmartCrop.Shared.Models;

public class Recommendation
{
    public int RecommendationId { get; set; } // This is the primary key for the Recommendation table
    public int CropId { get; set; } // This is the foreign key to the Crop table
    
    public string RecommendationText { get; set; } // This will be the recommendation text
    public DateTime DateIssued { get; set; } // This will be the date when the recommendation was issued
    public string  Priority { get; set; } // This will be the priority of the recommendation (e.g., High, Medium, Low)
    
    public Crop Crop { get; set; } // This is the navigation property to the Crop table
    
    public string? WeatherSummary { get; set; }
    
    //Method sent to crop
    public string GetRecommendation()
    {
        return RecommendationText;
    }
    
}