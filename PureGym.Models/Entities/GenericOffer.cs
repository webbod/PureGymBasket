using PureGym.Common;
using PureGym.Common.Exceptions;
using PureGym.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PureGym.Models.Entities
{
    public class GenericOffer : AGenericEntity, IIsAnOfferItem
    {
        public string Description { get; private set; }

        public string Message { get; private set; }

        public Money Value { get; private set; }

        public bool CanBeApplied { get; private set; }

        public void WillBeApplied() { CanBeApplied = true; }

        public void WontBeAppliedYet() { CanBeApplied = false; }

        public void MessageIs(string message) { Message = message; }

        public void MessageHasNotBeenSet() { Message = string.Empty; }

        public Func<List<IIsABasketItem>, bool> ValidationTest { get; private set; }

        public bool CanBeAppliedTo(List<IIsABasketItem> basket, Func<List<IIsABasketItem>, bool> ifItPassesThis)
        {
            WontBeAppliedYet();
            MessageHasNotBeenSet();
            return ifItPassesThis(basket);
        }

        public GenericOffer() {   }

        public GenericOffer(string key, string description, Money value, Func<List<IIsABasketItem>, bool> validationTest, Guid id = default(Guid)) :
            base()
        {
            Init(key, description, value, validationTest, id);
        }

        protected void Init(string key, string description, Money value, Func<List<IIsABasketItem>, bool> validationTest, Guid id = default(Guid))
        {
            if (HasBeenInitalised()) { return; }

            WontBeAppliedYet();
            
            if (value < 0) { throw new PriceOutOfRangeException($"{nameof(value)} {SharedStrings.WasNegative}"); }
            Value = value;

            Helper.CheckIfValueIsNull(description, nameof(description));
            Description = description;

            Helper.CheckIfValueIsNull(validationTest, nameof(ValidationTest));
            ValidationTest = validationTest;

            base.Init(key, id);
        }

        public void SeeIfItCanBeAppliedTo(List<IIsABasketItem> basket)
        {
            try
            {
                this.CanBeAppliedTo(basket, ifItPassesThis: ValidationTest);
                this.WillBeApplied();

                MessageIs($"1 x {Value} {Description} Offer Voucher {Key} applied");
            }
            catch (OfferNotValidException error)
            {
                if (error.Reason.IsMissingCategory)
                {
                    MessageIs($"There are no products in your basket applicable to voucher Voucher {Key}");
                }
                else if (error.Reason.IsInsuffientSpend)
                {
                    MessageIs($"You have not reached the spend threshold for voucher {Key}. Spend another {error.Reason.OutstandingBalance} to receive {Value} discount from your basket total.");
                }
            }
            catch (ArgumentNullException)
            {
                MessageIs($"There are no products in your basket applicable to voucher Voucher {Key}");
            }
        }
    }
}
