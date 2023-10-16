using ProductManagement.Application;
using ProductManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class UnitOfWork : IUnitOfWork
    {
       // public readonly IProductRepository _productRepository;
       // public IProductRepository ProductRepository => throw new NotImplementedException();
       public  UnitOfWork(IProductRepository productRepository)
        {
            Products= productRepository;
        }
        public IProductRepository Products { get; }

        //public IProductRepository ProductRepository => throw new NotImplementedException();
    }
}
