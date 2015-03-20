Given(/^I press the "(.*?)" cell$/) do |cell_text|
    touch("UITableViewCell text:'#{cell_text}'")
end

Then(/^all the tests should run$/) do
    sleep 2
    screenshot_embed
end