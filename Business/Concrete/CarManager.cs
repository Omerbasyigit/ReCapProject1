using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal carDal;
        IColorService _colorService;
        IBrandService _brandService;

        public CarManager(ICarDal carDal,IColorService colorService,IBrandService brandService)
        {
            this.carDal = carDal;
            _colorService = colorService;
            _brandService = brandService;
        }
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car entity)
        {
            var result = BusinessRules.Run(CheckColorExist(entity.ColorId), CheckBrandExist(entity.BrandId));
            if (result != null)
            {
                return new ErrorResult();
            }
            else
            {
                carDal.Add(entity);
                return new SuccessResult(Messages.Added);
            }
         
        }
        [SecuredOperation("admin")]
        public IResult Delete(Car entity)
        {

            carDal.Delete(entity);
            return new SuccessResult(Messages.Deleted);
        }


        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(carDal.GetAll(), Messages.Listed);
        }

        public IDataResult<List<CarDetailDto>> GetAllDetailByBrand(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(carDal.GetAllCarDetails(c => c.BrandId == brandId));
        }

        public IDataResult<List<CarDetailDto>> GetAllDetailByColor(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(carDal.GetAllCarDetails(c => c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailDto>> GetAllDetail()
        {
            return new SuccessDataResult<List<CarDetailDto>>(carDal.GetAllCarDetails(), Messages.Detail);
        }

        public IDataResult<Car> GetById(int id)
        {
            var result = carDal.Get(c => c.CarId == id);
            if (result != null)
            {
                return new SuccessDataResult<Car>(result);
            }
            else { return new ErrorDataResult<Car>(Messages.CarNotFound);}
            
        }

        public IDataResult<CarDetailDto> GetCarDetail(int carId)
        {
            return new SuccessDataResult<CarDetailDto>(carDal.GetCarDetails(c=>c.CarId==carId));
        }
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car entity)
        {
            var result = BusinessRules.Run(CheckColorExist(entity.ColorId), CheckBrandExist(entity.BrandId));
            if (result != null)
            {
                return new ErrorResult();
            }
            else
            {
                carDal.Update(entity);
                return new SuccessResult(Messages.Modified);
            }
          
        }

        public IDataResult<List<CarDetailDto>> GetAllByBrandAndColorId(int brandId, int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(carDal.GetAllCarDetails(c => c.BrandId == brandId&&c.ColorId==colorId));
        }
        public IResult CheckColorExist(int colorId)
        {
            var result = _colorService.GetById(colorId);
            if(result.success)
            {
                return new SuccessResult(Messages.GetById);
            }
            else { return new ErrorResult(Messages.ColorNotFound); }
        }
        public IResult CheckBrandExist(int brandId)
        {
            var result = _brandService.GetById(brandId);
            if (result.success)
            {
                return new SuccessResult(Messages.GetById);
            }
            else
            {
                return new ErrorResult(Messages.BrandNotFound);
            }
        }
    }
        
}
