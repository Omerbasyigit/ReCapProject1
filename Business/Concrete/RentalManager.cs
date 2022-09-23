using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICarService _carService;

        public RentalManager(IRentalDal rentalDal, ICarService carService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
        }
        [SecuredOperation("user,admin")]
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental entity)
        {
            
            var result = BusinessRules.Run(CheckIfCarExist(entity.CarId),
               CheckIfDateIsAvailable(entity,CheckLastId(entity.CarId)));
            
            if (result!=null)
            {
                return new ErrorResult();
            }
            else
            {
                _rentalDal.Add(entity);
                return new SuccessResult(Messages.Added);
            }
        }

        [SecuredOperation("user,admin")]
        public IResult Delete(Rental entity)
        {
            var result = BusinessRules.Run(CheckIfIdMatch(entity));
            if (result != null)
            {
                _rentalDal.Delete(entity);
                return new SuccessResult(Messages.Deleted);
            }
            else return new ErrorResult(Messages.NameInvalid);

        }


        public IDataResult<List<Rental>> GetAll()
        {

            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.Listed);
        }



        public IDataResult<Rental> GetById(int Id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == Id));
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {

            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(), Messages.Detail);
        }

        [SecuredOperation("user,admin")]
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental entity)
        {
            var result = BusinessRules.Run(CheckIfIdMatch(entity));
            if (result != null)
            {
                _rentalDal.Update(entity);
                return new SuccessResult(Messages.Modified);
            }
            else return new ErrorResult(Messages.NameInvalid);

        }
        private IResult CheckIfIdMatch(Rental rental)
        {
            var result = _rentalDal.Get(r => r.Id == rental.Id);
            if (result != null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        private IResult CheckIfCarExist(int carId)
        {
            var result = _carService.GetById(carId);
            {
                if (result.success)
                {
                    return new SuccessResult();
                }
                return new ErrorResult(Messages.CarNotFound);
            }
        }
        private IResult CheckIfDateIsAvailable(Rental rental,Rental ?rental1)
        {

            if (rental1 == null)
            {
                CheckIfDateIsAvailable(rental);
                return new SuccessResult();
            }
            else if (rental.RentDate > rental1.ReturnDate)
            {
                return new SuccessResult();
            }
            else { return new ErrorResult(); }
            
            
              
            
              
        }
  
        
        private Rental CheckIfDateIsAvailable(Rental rental)
        {
            return rental;
        }
       
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Rental rental)
        {
            Add(rental);
            if (rental.CarId < 5)
            {
                throw new Exception("");
            }
            Add(rental);
            return null;
        }
        public Rental CheckLastId(int carId)
        {
            var result = _rentalDal.GetAll(c => c.CarId == carId);
            if (result.Count > 0)
            {
                result.Reverse();
                return result[0];
            }
            else
            {
                return null;
            }


        }

        public IDataResult<List<Rental>> GetRental(int carId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CarId == carId));
        }
    }
}
