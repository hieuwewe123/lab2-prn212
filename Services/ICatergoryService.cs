using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using BusinessObjects_.Models;

namespace Services
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
         
    }
}
