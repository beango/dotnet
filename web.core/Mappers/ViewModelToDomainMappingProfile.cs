using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using model;
using System.Reflection;
using AutoMapper.Impl;
using web.core.Models;

namespace web.core.Mappers
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
            Mapper.CreateMap<ProductsModel, Products>()
                .ForMember(entity => entity.ProductID, opt => opt.Ignore())
                .ForMember(entity => entity.Categories, opt => opt.Ignore())
                .ForMember(entity => entity.Suppliers, opt => opt.Ignore())
                ;

            Mapper.CreateMap<Categories, CategoryModel>();
            Mapper.CreateMap<CategoryModel, Categories>().ForMember(entity => entity.CategoryID, opt => opt.Ignore());

            Mapper.CreateMap<Suppliers, SupplierModel>();
            Mapper.CreateMap<SupplierModel, Suppliers>().ForMember(entity => entity.SupplierID, opt => opt.Ignore());

            IgnoreDtoPropertyToEntity<ProductsModel, Products>();
        }

        /// <summary>
        /// 对于所有的 DTO 到 Entity 的映射，都忽略 Id 和 Version 属性
        /// <remarks>当从DTO向Entity赋值时，要保持从数据库中加载过来的Entity的Id和Version属性不变！</remarks>
        /// </summary>
        private void IgnoreDtoPropertyToEntity<Dto, Entity>()
        {
            PropertyInfo idProperty = typeof(Entity).GetProperty("Id");
            PropertyInfo cTimeProperty = typeof(Entity).GetProperty("CreateTime");
            foreach (TypeMap map in Mapper.GetAllTypeMaps())
            {
                if (typeof(Dto).IsAssignableFrom(map.SourceType)
                    && typeof(Entity).IsAssignableFrom(map.DestinationType))
                {
                    if(null != idProperty)
                    map.FindOrCreatePropertyMapFor(new PropertyAccessor(idProperty)).Ignore();
                    if (null != cTimeProperty)
                        map.FindOrCreatePropertyMapFor(new PropertyAccessor(cTimeProperty)).Ignore();
                }
            }
        }
    }
}