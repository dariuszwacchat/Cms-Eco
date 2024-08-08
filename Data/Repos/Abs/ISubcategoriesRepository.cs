using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repos.Abs
{
    public interface ISubcategoriesRepository
    {
        Task<TaskResult<List<Subcategory>>> GetAll();
        Task<TaskResult<Subcategory>> Get(string id);
        Task<TaskResult<List<Subcategory>>> GetAllByCategoryId(string categoryId);
        Task<TaskResult<Subcategory>> Create(Subcategory model);
        Task<TaskResult<Subcategory>> Delete(string id);
        Task<TaskResult<Subcategory>> Update(Subcategory model);
    }
}
