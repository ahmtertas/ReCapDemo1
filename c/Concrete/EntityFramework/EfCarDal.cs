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
    public class EfCarDal : EfEntityRepositoryBase<Car,RentCarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (RentCarContext context = new RentCarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on
                             c.BrandId equals b.BrandId
                             join co in context.Colors on
                             c.ColorId equals co.ColorId
                             select new CarDetailDto
                             {
                                 Id=c.Id, BrandName=b.BrandName, ColorName=co.ColorName, DailyPrice=c.DailyPrice
                             };
                return result.ToList();
                             
                 
            }
        }
    }
}
