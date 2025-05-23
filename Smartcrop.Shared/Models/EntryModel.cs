namespace SmartCrop.Shared.Models;

public class EntryModel
{
    // Field info
    public string FieldLocation { get; set; } = string.Empty;
    public string SoilType { get; set; } = string.Empty;

    // Soil data
    public string SoilpH { get; set; } = string.Empty;
    public string SoilOrganicMatter { get; set; } = string.Empty;
    public string SoilMoisture { get; set; } = string.Empty;

    // Crop info
    public string CropName { get; set; } = string.Empty;
    public string CropVariety { get; set; } = string.Empty;
}