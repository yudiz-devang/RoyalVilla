﻿namespace RoyalVilla_Client.Model.ViewModel
{
    public class HomeViewModel
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public int NoOfNights { get; set; } = 1;
    }
}
