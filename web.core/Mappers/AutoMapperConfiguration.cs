using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace web.core.Mappers
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
