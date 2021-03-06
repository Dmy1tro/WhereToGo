﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToGoWebApi.Models.EventViewModels
{
    public enum OrderParameter
    {
        Name,
        NameDesc,
        Date,
        DateDesc,
        Price,
        PriceDesc
    }

    public class EventViewModelFilter
    {
        public OrderParameter OrderBy { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
