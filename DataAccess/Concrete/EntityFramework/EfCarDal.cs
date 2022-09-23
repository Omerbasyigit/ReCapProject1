using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, NorthwindContext>, ICarDal
    {
       

        public List<CarDetailDto> GetAllCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (NorthwindContext northwindContext=new NorthwindContext())
            {
                var result = from c in northwindContext.Cars
                             join b in northwindContext.Brands
                             on c.BrandId equals b.BrandId
                             join co in northwindContext.Colors
                             on c.ColorId equals co.ColorId
                             
                             select new CarDetailDto
                             {
                                 CarId=c.CarId,
                                 BrandId=b.BrandId,
                                 ColorId=co.ColorId,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 ModelYear = c.ModelYear,
                                 CarImages = (from i in northwindContext.CarImages where i.CarId == c.CarId select i).ToList(),
                                 DailyPrice=c.DailyPrice,
                                 Description=c.Description
                                 

                             };
                return filter == null ? result.ToList(): result.Where(filter).ToList();
            }
            
        }

        CarDetailDto ICarDal.GetCarDetails(Expression<Func<CarDetailDto, bool>> filter)
        {
            using (NorthwindContext northwindContext = new NorthwindContext())
            {
                var result = from c in northwindContext.Cars
                             join b in northwindContext.Brands
                             on c.BrandId equals b.BrandId
                             join co in northwindContext.Colors
                             on c.ColorId equals co.ColorId

                             select new CarDetailDto
                             {
                                 CarId = c.CarId,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 ModelYear = c.ModelYear,
                                 CarImages = (from i in northwindContext.CarImages where i.CarId == c.CarId select i).ToList(),
                                 DailyPrice= c.DailyPrice,
                                 Description=c.Description
                                 

                             };
                return   result.Where(filter).FirstOrDefault();
            }
        }
    }
}
