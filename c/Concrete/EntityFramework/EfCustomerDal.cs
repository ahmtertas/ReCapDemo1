using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, RentCarContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetailsDto()
        {
            using (var context = new RentCarContext())
            {
                var result = from c in context.Customers
                             join b in context.Users on
                             c.Id equals b.Id                           
                             select new CustomerDetailDto
                             {
                                 Id = c.Id,
                                 FirstName = b.FirstName,
                                 LastName = b.LastName,
                                 CompanyName = c.CompanyName
                             };

                return result.ToList();
            }
        }
    }
}
