Feature: ErrorsValidation

#SIMPLE EXAMPLE
Scenario: Pet Insurance: Personal code is required error validation
	Given I have opened IF insurance home page
	And I have selected 'Pirkt' from category 'Mājdzīvniekam'
	And I have selected pet 'Kaķis'
	And I have selected breed 'Birmietis'
	And I have selected date of birth day '1' of current month
	When I press [Calculate Price] button
	Then Error 'Lauks ir obligāts' for personal code field should be displayed


#TEST DATA EXAMPLE
Scenario Outline: Pet Insurance: Incorrect phone and personal number error validation 
	Given I have opened IF insurance home page
	And I have selected 'Pirkt' from category 'Mājdzīvniekam'
	And I have selected pet '<PetType>'
	And I have filled information about pet using data with name '<DataName>'
	When I press [Calculate Price] button
	Then Error 'Pārbaudi, vai personas kods ievadīts pareizi un mēģini vēlreiz' for personal code field should be displayed
	And Error 'Tālruņa numurs ievadīts nepareizi. Lūdzu, pārbaudi to un mēģini vēlreiz' for phone number field should be displayed

	Examples: 
	| DataName        | PetType |
	| Dog_Akita       | Suns    |
	| Dog_Azavaks     | Suns    |
	| Cat_Asian_Smoke | Kaķis   |

