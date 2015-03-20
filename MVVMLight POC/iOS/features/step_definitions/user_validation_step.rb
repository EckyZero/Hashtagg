Given(/^I try to validate an email\/username that is (\d+) characters long$/) do |arg1|
    
    touch("textField index:#{0}")
    wait_for_keyboard
    keyboard_enter_text("")
    
    touch("button marked:'Log In'")
end

Then(/^I should see the error message "(.*?)"$/) do |error_message|
    
    check_element_exists("label marked:'#{error_message}'")
    touch ("view:'_UIAlertControllerActionView' label marked:'OK'")
end

Given(/^I enter any value into the textField$/) do
    
    touch("textField index:#{0}")
    wait_for_keyboard
    keyboard_enter_text("testuser@example.com")
    
    touch("button marked:'Log In'")
end

Then(/^I should be taken to the next screen$/) do
    
    check_element_exists("label marked:'European Soccer Seasons'")
end