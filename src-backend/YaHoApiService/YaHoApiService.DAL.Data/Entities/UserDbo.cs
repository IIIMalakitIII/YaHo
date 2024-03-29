﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace YaHo.YaHoApiService.DAL.Data.Entities
{
    public class UserDbo : IdentityUser
    {
        public string FirstName { get; set; }

        public int? TelegramId { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public decimal Balance { get; set; }

        public decimal Hold { get; set; }

        public DateTime InitialDate { get; set; }

        public DeliveryDbo Delivery { get; set; }

        public CustomerDbo Customer { get; set; }

        public ICollection<DeliveryReviewDbo> DeliveryReviews { get; set; }

        public ICollection<CustomerReviewDbo> CustomerReviews { get; set; }

        public ICollection<ConfirmExpectedDateDbo> ConfirmsExpectedDate { get; set; }

        public ICollection<ConfirmOrderStatusDbo> ConfirmsOrderStatus { get; set; }

        public ICollection<LiqPayOrderDbo> LiqPayOrders { get; set; }
    }
}
