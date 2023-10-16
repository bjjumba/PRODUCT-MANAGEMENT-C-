using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Core.Entities
{
    public class UserAuth
    {
        public string  UserName { get; set; }
        public string Password { get; set; }
       // [DataType(DataType.PhoneNumber)]
        public string PhoneNumber  { get; set; }
        public string CountryCode { get; set; }
        //public static explicit operator UserAuth(Task<UserAuth>)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
