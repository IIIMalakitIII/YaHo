using System;
using System.Collections.Generic;

namespace YaHo.YaHoApiService.DAL.Data.Entities
{
    public class UserDbo
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public  string Email { get; set; }

        public string Description { get; set; }

        public int? Balance { get; set; }

        public int? Hold { get; set; }

        public DateTime InitialDate { get; set; }

        public DeliveryDbo Delivery { get; set; }

        public CustomerDbo Customer { get; set; }

        public ICollection<DeliveryReviewDbo> DeliveryReviews { get; set; }

        public ICollection<CustomerReviewDbo> CustomerReviews { get; set; }

    }
}
