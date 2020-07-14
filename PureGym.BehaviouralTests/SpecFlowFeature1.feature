Feature: Basket
	In order to buy stuff later
	As a shopper
	I want to be able to manage items in my baske

Scenario: Add an item to the basket
Given I have an empty basket
And I have added "HAT001" into the basket
When I generate an invoice
Then There should be 1 line on the invoice
