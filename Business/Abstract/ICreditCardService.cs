using Core.Business;
using Core.Utilities.Results;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICreditCardService:IBaseService<CreditCard>
    {
        IDataResult<List<CreditCardDto>> GetAllCreditCards(int customerId);
    }
}
