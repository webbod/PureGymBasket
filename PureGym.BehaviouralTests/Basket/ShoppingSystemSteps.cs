using PureGym.Basket;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Models.Compositions;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;

namespace PureGym.BehaviouralTests
{
    [Binding]
    public class ShoppingSystemSteps
    {
        private GenericShop Shop;
        private IIsAnInvoice Invoice;

        [Given(@"The shop is open")]
        public void GivenTheShopIsOpen()
        {
            Shop = new GenericShop(new TestWarehouse(), new TestOfferFactory(), new ShoppingBasket(), Currency.GBP);
        }
        
        [Given(@"I have added an item with the reference ""(.*)""")]
        public void GivenIHaveAddedAnItemWithTheReference(string productKey)
        {
            Shop.AddToBasket(productKey);
        }

        [Given(@"I have added an item with the reference ""(.*)"" (.*) times")]
        public void GivenIHaveAddedAnItemWithTheReferenceSeveralTime(string productKey, int increment)
        {
            Shop.UpdateBasketQuantity(productKey, increment);
        }

        [Given(@"I remove an item with the reference ""(.*)""")]
        public void GivenIRemoveAnItemWithTheReference(string productKey)
        {
            Shop.RemoveFromBasket(productKey);
        }
        
        [When(@"I request the invoice")]
        public void WhenIRequestTheInvoice()
        {
            Invoice = Shop.GenerateInvoice();
        }
        
        [Then(@"I should see 1 line for ""(.*)"" with a quantity of (.*)")]
        public void ThenIShouldSeeLineForWithAQuantityOf(string productKey, int quantity)
        {
            var matchingLines = Invoice.BasketItems.Where(i => i.Key == productKey);
            Assert.Equal<int>(quantity, matchingLines.FirstOrDefault().Quantity);
            Assert.Single(matchingLines);
        }
        
        [Then(@"I should not see a line for ""(.*)""")]
        public void ThenIShouldNotSeeALineFor(string productKey)
        {
            var matchingLines = Invoice.BasketItems.Where(i => i.Key == productKey);
            Assert.Empty(matchingLines);
        }
    }
}
