Feature: ErrorsValidation


Scenario: Pet Insurance: Personal code is required error validation
	Given I have opened IF insurance home page
	And I have selected 'Pirkt' from category 'Mājdzīvniekam'
	And I have selected pet 'Kaķis'
	And I have selected breed 'Birmietis'
	And I have selected date of birth day '2' of current month
	When I press [Calculate Price] button
	Then Error 'Lauks ir obligāts' for personal code field should be displayed