using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly3.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public short SignUpFee { get; set; }
        //bc we dont need any values more than 32k

        public byte DurationInMonths { get; set; }
        //bc the largest number we are going to store is 12 for months

        public byte DiscountRate { get; set; }


        //we use short bc we dont need any values more than 32k
        //we use byte bc the largest number we are going to store is 12 for months

        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;

        // based on membershiptypes defined earlier in out database

    }
}
