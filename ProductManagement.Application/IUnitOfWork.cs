using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }

    }
}
