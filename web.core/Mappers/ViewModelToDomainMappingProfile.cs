using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using model.ef;
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
                .ForMember(entity => entity.Suppliers, opt => opt.Ignore());

            Mapper.CreateMap<Categories, CategoryModel>();
            Mapper.CreateMap<CategoryModel, Categories>().ForMember(entity => entity.CategoryID, opt => opt.Ignore());

            Mapper.CreateMap<Suppliers, SupplierModel>();
            Mapper.CreateMap<SupplierModel, Suppliers>().ForMember(entity => entity.SupplierID, opt => opt.Ignore());

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