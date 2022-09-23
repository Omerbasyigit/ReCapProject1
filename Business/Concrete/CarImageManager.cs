using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        IFileHelper _fileHelper;
       

        public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
           
        }
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            var result = BusinessRules.Run(CheckIfImageLimit(carImage.CarId),CheckIfExtension(Path.GetExtension(file?.FileName)));
            if (result!=null)
            {
                return new ErrorResult();
            }
            carImage.ImagePath = _fileHelper.Upload(file, PathConstant.ImagesPath);
            carImage.Date = DateTime.Now.Date;
            _carImageDal.Add(carImage);
            return new SuccessResult("Resim başarıyla yüklendi");
        }
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult AddMultiple(IFormFile[] files, CarImage carImage)
        {
            var result = BusinessRules.Run(CheckIfImageLimit(carImage.CarId), CheckIfExtensions(files));
            if (result != null)
            {
                return new ErrorResult();
            }
            foreach (var file in files)
            {
                carImage.ImagePath = _fileHelper.Upload(file, PathConstant.ImagesPath);
                carImage.Date = DateTime.Now.Date;
                _carImageDal.Add(carImage);
                return new SuccessResult("Resim başarıyla yüklendi");
            }
            return new SuccessResult("");
            
        }

        [SecuredOperation("admin")]
        public IResult Delete(CarImage carImage)
        {
            _fileHelper.Delete(PathConstant.ImagesPath + carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c=>c.CarId==carId));
        }

        public IDataResult<CarImage> GetByImageId(int imageId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.Id == imageId));
        }
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            carImage.ImagePath = _fileHelper.Update(file, PathConstant.ImagesPath + carImage.ImagePath, PathConstant.ImagesPath);
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }
      
        private IResult CheckIfImageLimit(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result < 5)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        private IResult CheckIfExtension(string extension)
        {
            var result = Path.GetExtension(extension);
            if(extension == ".jpg"||extension==null)
            {
                return new SuccessResult();
            }
            return new ErrorResult(); 
        }
        private IResult CheckIfExtensions(IFormFile []files)
        {
      
            foreach (var file in files)
            {
                
                var result = Path.GetExtension(file.FileName);
                if (file.FileName == ".jpg" || file.FileName == null)
                {
                    return new SuccessResult();
                }
                
            }
            return new ErrorResult();
        }
    }
}
