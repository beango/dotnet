using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using model;
using web.Controllers.PRD;
using System.Reflection;
using AutoMapper.Impl;

namespace Web.Mappers
{
    class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Products, ProductsModel>();
            Mapper.CreateMap<ProductsModel, Products>().ForMember(entity => entity.ProductID, opt => opt.Ignore());

            //IgnoreDtoIdAndVersionPropertyToEntity();
        }

        /// <summary>
        /// 对于所有的 DTO 到 Entity 的映射，都忽略 Id 和 Version 属性
        /// <remarks>当从DTO向Entity赋值时，要保持从数据库中加载过来的Entity的Id和Version属性不变！</remarks>
        /// </summary>
        private void IgnoreDtoIdAndVersionPropertyToEntity<Dto,Entity>()
        {
            PropertyInfo idProperty = typeof(Entity).GetProperty("Id");
            PropertyInfo versionProperty = typeof(Entity).GetProperty("Version");
            foreach (TypeMap map in Mapper.GetAllTypeMaps())
            {
                if (typeof(Dto).IsAssignableFrom(map.SourceType)
                    && typeof(Entity).IsAssignableFrom(map.DestinationType))
                {
                    map.FindOrCreatePropertyMapFor(new PropertyAccessor(idProperty)).Ignore();
                    map.FindOrCreatePropertyMapFor(new PropertyAccessor(versionProperty)).Ignore();
                }
            }
        }
    }
}