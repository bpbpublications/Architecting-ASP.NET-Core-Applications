Feature: Weather Forecast API
 
  Scenario: Get Weather Forecasts
    Given the weather forecast service is running
    When I request the weather forecast
    Then I should receive 5 forecasts
    And each forecast should have a date, temperature, and summary
    And the temperature should be between -20 and 55 degrees
    And the summary should be one of the predefined summaries
