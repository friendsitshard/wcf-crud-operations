using MidDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MidtermWcfService.DataContracts
{
    public class OrderDTO
    {
        public int order_id { get; set; }

        public string name { get; set; }

        public string price { get; set; }

        public int? user_id { get; set; }
    }
}