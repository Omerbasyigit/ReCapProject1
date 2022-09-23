using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CreditCardManager : ICreditCardService
    {
        ICreditCardDal _creditCardDal;

        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }

        public IResult Add(CreditCard entity)
        {
            _creditCardDal.Add(entity);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(CreditCard entity)
        {
            _creditCardDal.Delete(entity);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<CreditCard>> GetAll()
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll());
        }

        public IDataResult<List<CreditCardDto>> GetAllCreditCards(int customerId)
        {
            return new SuccessDataResult<List<CreditCardDto>>(_creditCardDal.GetAllCards(c=>c.CustomerId==customerId));
        }

        public IDataResult<CreditCard> GetById(int customerId)
        {
            return new SuccessDataResult<CreditCard>(_creditCardDal.Get(c=>c.CustomerId==customerId));
        }


        public IResult Update(CreditCard entity)
        {
            _creditCardDal.Update(entity);
            return new SuccessResult(Messages.Modified);
        }
    }
}
