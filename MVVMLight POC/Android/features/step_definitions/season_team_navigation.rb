Given(/^I log in$/) do
    touch("EditText index:#{0}")
    keyboard_enter_text("testuser@example.com")
    touch("button marked:'Log In'")
end

Then(/^I should be taken to the screen titled "(.*?)"$/) do |season_title|
    sleep 30
    check_element_exists("textview marked:'#{season_title}'")
end

Then(/^then select "(.*?)"$/) do |season_name|
    touch("textview marked:'#{season_name}'")
end

Then(/^I should see "(.*?)" in the list$/) do |team_name|
    sleep 30
    check_element_exists("textview marked:'#{team_name}'")
end