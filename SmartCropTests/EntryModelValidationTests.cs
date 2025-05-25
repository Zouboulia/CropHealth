using SmartCrop.Shared.Models;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;

public class EntryModelValidationTests
{
    [Fact]
    public void EntryModel_ShouldFailValidation_WhenRequiredFieldsAreMissing()
    {
        // Arrange
        var entryModel = new EntryModel
        {
            FieldLocation = string.Empty,
            SoilType = string.Empty,
            SoilpH = string.Empty,
            SoilOrganicMatter = string.Empty,
            SoilMoisture = string.Empty,
            CropName = string.Empty,
            CropVariety = string.Empty
        };

        // Act
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(entryModel);
        bool isValid = Validator.TryValidateObject(entryModel, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeFalse(); // Validation should fail

        // Check that the correct error messages are present
        validationResults.Should().Contain(r => r.ErrorMessage == "Field location is required.");
        validationResults.Should().Contain(r => r.ErrorMessage == "Soil type is required.");
        validationResults.Should().Contain(r => r.ErrorMessage == "Soil pH is required.");
        validationResults.Should().Contain(r => r.ErrorMessage == "Soil organic matter level is required.");
        validationResults.Should().Contain(r => r.ErrorMessage == "Soil moisture level is required.");
        validationResults.Should().Contain(r => r.ErrorMessage == "Crop name is required.");
        validationResults.Should().Contain(r => r.ErrorMessage == "Crop variety is required.");
    }
    

    [Fact]
    public void EntryModel_ShouldPassValidation_WhenDataIsValid()
    {
        // Arrange
        var entryModel = new EntryModel
        {
            FieldLocation = "Field 1",
            SoilType = "Loam",
            SoilpH = "6.5",
            SoilOrganicMatter = "3.2",
            SoilMoisture = "18.5",
            CropName = "Wheat",
            CropVariety = "Winter Wheat"
        };

        // Act
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(entryModel);
        bool isValid = Validator.TryValidateObject(entryModel, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeTrue(); // Validation should pass
        validationResults.Should().BeEmpty(); // No validation errors
    }
}