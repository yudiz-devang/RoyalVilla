using Models;

namespace RoyalVilla_Client.Service.IService
{
    public interface IHotelRoomService
    {
        public Task<IEnumerable<HotelRoomDTO>> GetHotelRooms(string chekInDate, string checkOutDate);

        public Task<HotelRoomDTO> GetHotelRoomDetails(int roomId,string chekInDate, string checkOutDate);
    }

}
 