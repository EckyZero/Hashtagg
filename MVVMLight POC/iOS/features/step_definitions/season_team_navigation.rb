Given(/^I log in$/) do
    
    touch("textField index:#{0}")
    wait_for_keyboard
    keyboard_enter_text("testuser@example.com")
    
    touch("button marked:'Log In'")
end

Then(/^I should be taken to the screen titled "(.*?)"$/) do |season_title|
    
    sleep 2
    check_element_exists("label marked:'#{season_title}'")
end

Then(/^then select "(.*?)"$/) do |season_name|
    
    touch("UITableViewCell text:'#{season_name}'")
end

Then(/^I should see "(.*?)" in the list$/) do |team_name|
    
    sleep 2
    check_element_exists("label marked:'#{team_name}'")
end