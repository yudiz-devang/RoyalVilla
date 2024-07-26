﻿using DataAcess.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAcess.Data
{
    public class HotelRoomImage
    {
        public int Id { get; set; }
        public int RoomId { get; set; }

        public string RoomImageUrl { get; set; }
        [ForeignKey("RoomId")]
        public virtual HotelRoom HotelRoom { get; set; }
    }
}
