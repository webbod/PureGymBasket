Feature: Shopping System
	As a customer
	I want to be able to add items to my basket
	I want to be able to remove items from my basket

@Basket
Scenario: Add an item
	Given The shop is open
	And I have added an item with the reference "HAT001"
	When I request the invoice
	Then I should see 1 line for "HAT001" with a quantity of 1

Scenario: Remove an item when it is the only one of its kind
	Given The shop is open
	And I have added an item with the reference "HAT001"
	And I remove an item with the reference "HAT001"
	When I request the invoice
	Then I should not see a line for "HAT001"

Scenario: Add the same item more than once
	Given The shop is open
	And I have added an item with the reference "HAT001"
	And I have added an item with the reference "HAT001" 5 times
	When I request the invoice
	Then I should see 1 line for "HAT001" with a quantity of 6

Scenario: Remove an item when there are several already
	Given The shop is open
	And I have added an item with the reference "HAT001"
	And I have added an item with the reference "HAT001" 3 times
	And I remove an item with the reference "HAT001"
	When I request the invoice
	Then I should not see a line for "HAT001"