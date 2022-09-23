using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
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
    public class BrandManager : IBrandService
    {
        IBrandDal brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            this.brandDal = brandDal;
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("GetAll")]
        public IResult Add(Brand entity)
        {
            brandDal.Add(entity);
            return new SuccessResult(Messages.Added);
        }
        [SecuredOperation("admin")]
        [CacheRemoveAspect("GetAll")]
        public IResult Delete(Brand entity)
        {
            brandDal.Delete(entity);
            return new SuccessResult(Messages.Deleted);
        }

        [CacheAspect]
      //  [PerformanceAspect(5)]
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(brandDal.GetAll(), Messages.Listed);
        }

        public IDataResult<Brand> GetById(int Id)
        {
            var result = brandDal.Get(b => b.BrandId == Id);
            if (result == null)
            {
                return new ErrorDataResult<Brand>(Messages.BrandNotFound);
            }
            return new SuccessDataResult<Brand>(result);
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("GetAll")]
        
        public IResult Update(Brand entity)
        {
            brandDal.Update(entity);
            return new SuccessResult(Messages.Modified);
        }
    }
}
