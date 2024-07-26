using AutoMapper;
using Business.Repository.IRepository;
using Common;
using DataAcess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class RoomOrderDetailsRepository : IRoomOrderDetailsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public RoomOrderDetailsRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<RoomOrderDetailsDTO> Create(RoomOrderDetailsDTO details)
        {
            try
            {
                details.CheckInDate = details.CheckInDate.Date;
                details.CheckOutDate = details.CheckOutDate.Date;

                var roomOrder = _mapper.Map<RoomOrderDetailsDTO, RoomOrderDetails>(details);
                roomOrder.Status = SD.Status_Pending;
                var result = await _db.RoomOrderDetails.AddAsync(roomOrder);
                await _db.SaveChangesAsync();
                return _mapper.Map<RoomOrderDetails, RoomOrderDetailsDTO>(result.Entity);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<RoomOrderDetailsDTO>> GetAllRoomOrderDetails()
        {
            try
            {
                IEnumerable<RoomOrderDetailsDTO> roomOrders = _mapper.Map<IEnumerable<RoomOrderDetails>, IEnumerable<RoomOrderDetailsDTO>>
                    (_db.RoomOrderDetails.Include(u => u.HotelRoom));

                return roomOrders;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RoomOrderDetailsDTO> GetRoomOrderDetails(int roomOrderId)
        {
            try
            {
                RoomOrderDetails roomOrder = await _db.RoomOrderDetails.Include(u => u.HotelRoom).ThenInclude(x => x.HotelRoomImages).FirstOrDefaultAsync(u => u.Id == roomOrderId);

                RoomOrderDetailsDTO roomOrderDetailsDTO = _mapper.Map<RoomOrderDetails, RoomOrderDetailsDTO>(roomOrder);
                roomOrderDetailsDTO.HotelRoomDTO.TotalDays = roomOrderDetailsDTO.CheckOutDate.Subtract(roomOrderDetailsDTO.CheckInDate).Days;


                return roomOrderDetailsDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> IsRoomBooked(int RoomId, DateTime CheckInDate, DateTime CheckOutDate)
        {
            var status = false;
            var existingBooking = await _db.RoomOrderDetails.Where(x=>x.RoomId == RoomId && x.IsPaymentSucessful && (CheckInDate <x.CheckOutDate && CheckInDate > x.CheckInDate || CheckOutDate.Date>x.CheckInDate.Date && CheckInDate.Date <x.CheckInDate)).FirstOrDefaultAsync();

            if (existingBooking != null) 
            {
                status = true;
            }
            return status;
        }

        public Task<RoomOrderDetailsDTO> MarkPaymentSucessful(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateOrderStatus(int RoomOrderId, string status)
        {
            try
            {
                var roomorder = await _db.RoomOrderDetails.FirstOrDefaultAsync(u => u.Id == RoomOrderId); 
                if(roomorder == null)
                {
                    return false;
                }
                roomorder.Status = status;
                if(status == SD.Status_CheckedIn)
                {
                    roomorder.ActualCheckInDate = DateTime.Now;
                }
                if(status == SD.Status_CheckedOut_Completed)
                {
                    roomorder.ActualCheckOutDate = DateTime.Now;
                }
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
