using AutoMapper;
using DataAcess.Data;
using DataAcesss.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapper
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<HotelRoomDTO, HotelRoom>();  
            CreateMap<HotelRoom, HotelRoomDTO>();  
            CreateMap<HotelRoomImage, HotelRoomImageDTO>().ReverseMap();
            CreateMap<HotelAmenity, HotelAmenityDTO>().ReverseMap();
            CreateMap<RoomOrderDetails, RoomOrderDetailsDTO>().ReverseMap();
            CreateMap<RoomOrderDetails, RoomOrderDetailsDTO>().ForMember(x=>x.HotelRoomDTO,opt=>opt.MapFrom(c=>c.HotelRoom));   
        }
    }
}
