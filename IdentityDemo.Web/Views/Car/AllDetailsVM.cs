﻿namespace IdentityDemo.Web.Views.Car
{
    public class AllDetailsVM
    {

        public class CarVM
        {
            public int Id { get; set; }
            public string Make { get; set; } = string.Empty;
            public string Model { get; set; } = string.Empty;
            public int Year { get; set; }
        }
        public CarVM[] CarVMs { get; set; } = [];
    }
}
