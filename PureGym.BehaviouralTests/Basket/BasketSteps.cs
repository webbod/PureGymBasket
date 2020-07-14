using PureGym.Basket;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Models.Compositions;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Xunit;

namespace PureGym.BehaviouralTests.Basket
{
    [Binding]
    public class BasketSteps
    {
        private Shop Shop;
        private IIsAnInvoice Invoice;

        [Given(@"I have an empty basket")]
        public void GivenIHaveAnEmptyBasket()
        {
            Shop = new Shop(new TestWarehouse(), new TestOfferFactory(), new ShoppingBasket(), Currency.GBP);
        }
        
        [Given(@"I have added ""(.*)"" into the basket")]
        public void GivenIHaveAddedIntoTheBasket(string key)
        {
            Shop.AddToBasket(key);
        }
        
        [When(@"I generate an invoice")]
        public void WhenIGenerateAnInvoice()
        {
            Invoice = Shop.GenerateInvoice();
        }

        [Then(@"There should be (.*) line on the invoice")]
        public void ThenThereShouldBeLineOnTheInvoice(int numberOfLines)
        {
            Assert.Equal(Invoice.BasketItems.Count, numberOfLines);
        }
    }
}
