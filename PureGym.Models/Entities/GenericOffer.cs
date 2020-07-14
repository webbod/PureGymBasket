using PureGym.Common;
using PureGym.Common.Exceptions;
using PureGym.Interfaces.Common;
using System;
using System.Collections.Generic;

namespace PureGym.Models.Entities
{
    public class GenericOffer : AGenericEntity, IIsAnOfferItem
    {

        public string Reason { get; private set; }

        public bool CanBeApplied { get; private set; }

        /// <summary>
        /// A chain of rules that determines when an offer can be used
        /// </summary>
        public Func<List<IIsABasketItem>, bool> ValidationTest { get; private set; }

        public GenericOffer() {   }

        public GenericOffer(string key, string description, Money value, Func<List<IIsABasketItem>, bool> validationTest, Guid id = default(Guid)) :
            base()
        {
            Init(key, description, value, validationTest, id);
        }

        protected void Init(string key, string description, Money value, Func<List<IIsABasketItem>, bool> validationTest, Guid id = default(Guid))
        {
            if (HasBeenInitalised) { return; }

            WontBeAppliedYet();
          
            Helper.CheckIfValueIsNull(validationTest, nameof(ValidationTest));
            ValidationTest = validationTest;

            base.Init(key, description, value, id);
        }

        private void WillBeApplied() { CanBeApplied = true; }

        private void WontBeAppliedYet() { CanBeApplied = false; }

        public void ReasonIs(string message) { Reason = message; }

        private void ResetReason() { Reason = string.Empty; }
        
        private bool CanBeAppliedTo(List<IIsABasketItem> basket, Func<List<IIsABasketItem>, bool> ifItPassesThis)
        {
            WontBeAppliedYet();
            ResetReason();
            return ifItPassesThis(basket);
        }

        // TODO remove all of these strings
        public void SeeIfItCanBeAppliedTo(List<IIsABasketItem> basket)
        {
            try
            {
                this.CanBeAppliedTo(basket, ifItPassesThis: ValidationTest);
                this.WillBeApplied();

                ReasonIs($"1 x {Value} {Description} Offer Voucher {Key} applied");
            }
            catch (OfferNotValidException error)
            {
                if (error.Reason.IsMissingCategory)
                {
                    ReasonIs($"There are no products in your basket applicable to voucher Voucher {Key}");
                }
                else if (error.Reason.IsInsuffientSpend)
                {
                    ReasonIs($"You have not reached the spend threshold for voucher {Key}. Spend another {error.Reason.OutstandingBalance} to receive {Value} discount from your basket total.");
                }
            }
            catch (ArgumentNullException)
            {
                ReasonIs($"There are no products in your basket applicable to voucher Voucher {Key}");
            }
        }

        public override string ToString() => $"{Description} {(CanBeApplied ? SharedStrings.Applied : Reason)}";
        
    }
}
