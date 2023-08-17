Feature: Navigation

@Home
Scenario: Home: Url is valid
	Given I have opened IF insurance home page
	Then Current url should contains 'https://www.if.lv/privatpersonam'
