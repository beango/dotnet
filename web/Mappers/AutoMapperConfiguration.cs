using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Web.Mappers;

namespace web.Mappers
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }
    }
}
