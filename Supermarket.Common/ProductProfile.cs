using AutoMapper;
using Supermarket.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Common
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductRest>()
                .ConstructUsing(product => new ProductRest());
            CreateMap<ProductRest, Product>()
                    .ConstructUsing(productRest => new Product());
        }

    }
}
