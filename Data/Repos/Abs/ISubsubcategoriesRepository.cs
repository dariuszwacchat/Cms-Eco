using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repos.Abs
{
    public interface ISubsubcategoriesRepository
    {
        Task<TaskResult<List<Subsubcategory>>> GetAll();
        Task<TaskResult<Subsubcategory>> Get(string id);
        Task<TaskResult<List<Subsubcategory>>> GetAllByCategoryIdAndSubCategoryId (string categoryId, string subcategoryId);
        Task<TaskResult<Subsubcategory>> Create(Subsubcategory model);
        Task<TaskResult<Subsubcategory>> Delete(string id);
        Task<TaskResult<Subsubcategory>> Update(Subsubcategory model);
    }
}
