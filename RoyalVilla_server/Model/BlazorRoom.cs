﻿namespace RoyalVilla_server.Model
{
    public class BlazorRoom
    {
        public int Id { get; set; } 
        public string RoomName { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }

        public List<BlazorRoomProp> RoomProp { get; set; }
    }
}
