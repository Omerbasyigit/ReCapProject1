using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCreditCardDal : EfEntityRepositoryBase<CreditCard, NorthwindContext>, ICreditCardDal
    {
        public List<CreditCardDto> GetAllCards(Expression<Func<CreditCardDto, bool>> filter =null)
        {
            using (NorthwindContext northwindContext = new NorthwindContext())
            {
                var result = from cr in northwindContext.CreditCards
                             join c in northwindContext.Customers
                             on cr.CustomerId equals c.CustomerId
                             join u in northwindContext.Users
                             on c.UserId equals u.Id
                             select new CreditCardDto
                             {
                                 CustomerId = c.CustomerId,
                                 CardNumber = cr.CardNumber,
                                 FullName = u.FirstName + " " + u.LastName
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
