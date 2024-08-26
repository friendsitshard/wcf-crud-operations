using AutoMapper;
using MidDAL;
using MidtermWcfService.DataContracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MidtermWcfService.Mapper
{
    public static class AutoMapperConfig
    {
        private static readonly IMapper _mapper;

        static AutoMapperConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<Order, OrderDTO>();
                cfg.CreateMap<OrderDTO, Order>();
            });
            _mapper = config.CreateMapper();
        }

        public static IMapper Mapper => _mapper;
    }
}