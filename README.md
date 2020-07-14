# PureGymBasket
A solution for a technical test. I have tried to keep the naming fairly informal, so don't expect any references to specific patterns, I wanted the code to be human readable

Definitely have a look at the way the TestOfferFactory works, particularly the fluent voucher rule system, I was trying to give you a high degree of reuse in a declarative fashion

I haven't had a chance to consider persistence beyond JSON and there is no notion of a basket identity at the moment - in a production system I would expect the Warehouse and OfferWarehouse to interact with your LOB systems, 
for now it's enough that they are hardcoded

Statrt with PureGym.Models.Compositions.Shop

## PureGym.Common 
Defines the Money type, exceptions, enumerations and constants

## PureGym.Basket
Is a concrete representation of the system that I am using for testing

## PureGym.Interfaces
Interfaces for entities, containers, strategies and participants in the shopping system

## PureGym.Models
Defines compositions, containers, entities, business rules and the invoice model

## PureGym.ShoppingConsole
I'm migrating this into BehaviouralTests, and have been using it for debugging and to explore different degrees of coupling

## PureGym.UnitTests 
Will have the refactored XUnit tests

## PureGym.BehaviouralTests
Will have more complex integration tests