using Core.DataAccess;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICreditCardDal: IEntityRepository<CreditCard>
    {
        List<CreditCardDto> GetAllCards(Expression<Func<CreditCardDto, bool>> filter = null);
    }
}
