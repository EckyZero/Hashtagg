Feature: Team Navigation
	The user must be able to navigate to different teams in different seasons

	Scenario: User selects a season
		Given I log in
		Then I should be taken to the screen titled "European Soccer Seasons"
		And then select "Premier League 2014/15"
		Then I should see "Manchester United FC" in the list