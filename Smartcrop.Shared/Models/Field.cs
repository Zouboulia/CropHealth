namespace SmartCrop.Shared.Models;

public class Field
{
    public int FieldId { get; set; }

    public string Location { get; set; }
    public string SoilType { get; set; }
    public int FarmerId { get; set; } // This is the foreign key to the Farmer table
    public Farmer Farmer { get; set; } // This is the navigation property to the Farmer table

    public SoilData SoilData { get; set; } // This will be the soil data associated with the field
    
    public ICollection<Crop> Crops { get; set; } // This will be a collection of crops planted in the field
}