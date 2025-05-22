namespace SmartCrop.Shared.Models;

public class Crop
{
    public int CropId { get; set; }  // This is the primary key for the Crop table

    public string Name { get; set; }
    public string Variety { get; set; }

    public int FieldId { get; set; }
    public Field Field { get; set; }
}