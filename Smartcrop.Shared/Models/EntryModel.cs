using System.ComponentModel.DataAnnotations;

namespace SmartCrop.Shared.Models;

public class EntryModel
{
    // Field info
    [Required(ErrorMessage = "Field location is required.")]
    public string FieldLocation { get; set; } = string.Empty;
    [Required(ErrorMessage = "Soil type is required.")]
    public string SoilType { get; set; } = string.Empty;

    // Soil data
    [Required(ErrorMessage = "Soil pH is required.")]
    [PercentageString] //This attribute is used to validate that the input is a valid percentage string
    public string SoilpH { get; set; } = string.Empty;
    [Required(ErrorMessage = "Soil organic matter level is required.")]
    [PercentageString]
    public string SoilOrganicMatter { get; set; } = string.Empty;
    [Required(ErrorMessage = "Soil moisture level is required.")]
    [PercentageString]
    public string SoilMoisture { get; set; } = string.Empty;

    // Crop info
    [Required(ErrorMessage = "Crop name is required.")]
    public string CropName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Crop variety is required.")]
    public string CropVariety { get; set; } = string.Empty;
}