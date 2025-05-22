namespace SmartCrop.Shared.Models;

public class Farmer
{
    public int FarmerId { get; set; }  // Primary key

    public string Name { get; set; }
    public ICollection<Field> Fields { get; set; } //this will be a collection of fields owned by the farmer, may remove later
}