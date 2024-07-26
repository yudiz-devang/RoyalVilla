using Business.Repository.IRepository;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using RoyalVilla_API.Helper;
using Stripe.BillingPortal;

namespace RoyalVilla_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class RoomOrderController : Controller
    {
        private readonly IRoomOrderDetailsRepository _repository;
        private readonly IEmailSender _emailSender; 
        public RoomOrderController(IRoomOrderDetailsRepository repository, IEmailSender emailSender)
        {
            _repository = repository;
            _emailSender = emailSender;
    }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomOrderDetailsDTO details)
        {
            if(ModelState.IsValid) 
            {
                var result = await _repository.Create(details);   
                return Ok(result);
            }
            else
            {
                return BadRequest(new ErrorModel(){
                    ErrorMessage = "Error while creating Room Details/Booking"
                });    
            }

        }
         
    }
}
