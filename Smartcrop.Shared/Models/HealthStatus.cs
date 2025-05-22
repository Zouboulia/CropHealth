namespace SmartCrop.Shared.Models;
using System.ComponentModel.DataAnnotations;


public class HealthStatus
{
    public int HealthStatusId { get; set; } // This is the primary key for the HealthStatus table
    public int CropId { get; set; } // This is the foreign key to the Crop table
    
    public string Status { get; set; } // This will be a string representing the health status of the crop
    public DateTime DateChecked { get; set; } // This will be the date when the health status was checked
    public string Notes { get; set; } // This will be any additional notes regarding the health status
    
    public Crop Crop { get; set; } // This is the navigation property to the Crop table
}