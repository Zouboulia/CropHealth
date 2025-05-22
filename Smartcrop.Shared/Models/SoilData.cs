namespace SmartCrop.Shared.Models;

public class SoilData
{
    public int SoilDataId { get; set; }  // This is the primary key for the SoilData table
    public int FieldId { get; set; } // This is the foreign key to the Field table
    public Field Field { get; set; } // This is the navigation property to the Field table
    
    public string SoilType { get; set; } // This will be the type of soil (e.g., sandy, clay, loamy)
    public string SoilMoisture { get; set; } // This will be the moisture content of the soil
    public string SoilpH { get; set; } // This will be the pH level of the soil
    public string SoilOrganicMatter { get; set; }
    
    //method "isHealthy" check (example, will change based on research)
    public bool IsHealthy()
    {
        // Check if the soil type is suitable for the crop
        if (SoilType == "sandy" || SoilType == "loamy")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
}