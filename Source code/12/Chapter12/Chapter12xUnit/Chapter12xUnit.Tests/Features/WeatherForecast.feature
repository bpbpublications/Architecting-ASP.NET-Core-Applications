Feature: WeatherForecast
  Scenario: Get weather forecasts
    Given I have a weather forecast controller
    When I request the weather forecast
    Then I should receive a list of 5 weather forecasts
