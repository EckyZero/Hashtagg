Feature: User validation.
  User/Email field must contain a value

  Scenario: Email is too short
    Given I try to validate an email/username that is 0 characters long
    Then I should see the error message "Please Enter a Valid Email Address"

  Scenario: Email is the correct length
  	Given I enter any value into the textField
  	Then I should be taken to the next screen