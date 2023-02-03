using AutoMapper;
using CustomerService.DTOs;
using Models;

namespace CustomerService.Maps
{
    public static class CustomerMapper
    {
        private static Mapper? _instance;

        public static Mapper Instance 
        { 
            get
            {
                if (_instance is null)
                {
                    _instance = new Mapper(
                        new MapperConfiguration(cfg =>
                            cfg.CreateMap<Customer, CustomerDTO>()
                            .ReverseMap()
                    ));
                }
                return _instance;
            }

            private set { _instance = value; }
        }
    }
}
