namespace StoreApp.web.Models;
using AutoMapper;
using StoreApp.Data.Concrete;
public class MapperProfile : AutoMapper.Profile
{
    public MapperProfile()
    {
        CreateMap<Data.Concrete.Product, ProductViewModel>();
        CreateMap<Data.Concrete.Category, CategoryViewModel>();
    }
}