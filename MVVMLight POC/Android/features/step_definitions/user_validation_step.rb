Given(/^I try to validate an email\/username that is (\d+) characters long$/) do |arg1|
    touch("EditText index:#{0}")
    touch("button marked:'Log In'")
end

Then(/^I should see the error message "(.*?)"$/) do |error_message|
    sleep (2)
    check_element_exists("DialogTitle marked:'#{error_message}'")
    touch ("button marked:'OK'")
end

Given(/^I enter any value into the textField$/) do
    touch("EditText index:#{0}")
    keyboard_enter_text("testuser@example.com")
    touch("button marked:'Log In'")
end

Then(/^I should be taken to the next screen$/) do
    sleep (30)
    check_element_exists("textview marked:'European Soccer Seasons'")
end