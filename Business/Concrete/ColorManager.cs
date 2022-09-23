using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal colorDal;
        public ColorManager(IColorDal colorDal)
        {
            this.colorDal = colorDal;
        }
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color entity)
        {
            colorDal.Add(entity);
            return new SuccessResult(Messages.Added);
        }
        [SecuredOperation("admin")]
        public IResult Delete(Color entity)
        {
            colorDal.Delete(entity);
            return new SuccessResult(Messages.Deleted);
        }

       

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(colorDal.GetAll(),Messages.Listed);
        }

        public IDataResult<Color> GetById(int colorId)
        {
            var result = colorDal.Get(c => c.ColorId == colorId);
            if (result == null)
            {
                return new ErrorDataResult<Color>(Messages.CarNotFound);
            }
            else
            {
                return new SuccessDataResult<Color>(result);
            }
           
        }
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(Color entity)
        {
            colorDal.Update(entity);
            return new SuccessResult(Messages.Modified);
        }
    }
}
