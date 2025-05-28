# Smart Crop Health Management application
This system analyzes crop health data and provides smart recommendations based on real-time weather data to maintain healthy crops. 

# Technologies used:
- ASP.NET Core
- Entity Framework Core (SQLite)
- OpenWeatherMap API
- xUnit (unit testing)
- SonarQube Cloud (code formatting)
- CodeQL
- Snyk

# Features:
1. Add Crop data manually.
2. Store data in an SQLite database.
3. View all crops
4. Generate crop health recommendations based on crop location and real-time weather data

# Running:
1. Clone the repository
2. Open terminal at server level (SmartCrop)
3. Build and run the application:

- dotnet build 
- dotnet run

4. Wait for browser page to open and load
5. From left pane menu select navigate to Data Entry
6. Add your crop data based on placeholder's hints
7. Submit data
8. Verify that the data was successfully added by going to AllCrops from left pane menu
9. Navigate to Recommendations
10. Input location or City based on "AllCrops" data
11. Receive recommendations


# License
- This application is currently unlicensed.
