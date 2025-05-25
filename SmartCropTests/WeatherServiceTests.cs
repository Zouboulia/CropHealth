using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using SmartCrop.Services;


public class WeatherServiceTests
{
    [Fact]
    public async Task GetWeatherAsync_ReturnsWeatherData()
    {
        // Arrange: mock JSON from OpenWeatherMap
        var jsonResponse = @"{
            ""main"": { ""temp"": 20.5, ""humidity"": 65 },
            ""rain"": { ""1h"": 2.3 },
            ""clouds"": { ""all"": 75 }
        }";

        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(mockHandler.Object);
        var logger = Mock.Of<ILogger<WeatherService>>();

        var service = new WeatherService(httpClient, logger);

        // Act
        var result = await service.GetWeatherAsync("Dublin");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(20.5, result.Temperature);
        Assert.Equal(65, result.Humidity);
        Assert.Equal(2.3, result.Rain);
        Assert.Equal(75, result.Clouds);
    }
}