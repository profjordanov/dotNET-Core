namespace TennisBookings.Web.Domain
{
    public class MembershipAdvert : IMembershipAdvert
    {
        public MembershipAdvert(decimal offerPrice, decimal discount)
        {
            OfferPrice = offerPrice;
            Saving = discount;
        }

        public decimal OfferPrice { get; }

        public decimal Saving { get; }
    }
}