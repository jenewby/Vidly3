using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly3.Models;

namespace Vidly3.ViewModels
{
    public class CustomerFormViewModel
    {
        //public List<MembershipType> MembershipTypes { get; set; }
        //or
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }

    }
}