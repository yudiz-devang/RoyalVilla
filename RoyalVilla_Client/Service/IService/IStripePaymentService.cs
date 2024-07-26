using Models;

namespace RoyalVilla_Client.Service.IService
{
    public interface IStripePaymentService
    {
        public Task<SuccessModel> CheckOut(StripePaymentDTO model);
    }
}
