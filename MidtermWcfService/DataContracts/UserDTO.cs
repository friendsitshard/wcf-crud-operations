using MidDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MidtermWcfService.DataContracts
{
    public class UserDTO
    {
        public int user_id { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public int? orders_quantity { get; set; }
    }
}