using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, NorthwindContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (NorthwindContext northwindContext=new NorthwindContext())
            {
                var result = from r in northwindContext.Rentals
                             join c in northwindContext.Customers
                             on r.CustomerId equals c.CustomerId
                             join car in northwindContext.Cars
                             on r.CarId equals car.CarId
                             join user in northwindContext.Users
                             on c.UserId equals user.Id




                             select new RentalDetailDto
                             {
                                 CarId = car.CarId,
                                 CustomerId = c.CustomerId,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate,
                                 CustomerName = user.FirstName,
                                 CustomerLastName=user.LastName

                             };
                return result.ToList();

            }
        }
    }
}
