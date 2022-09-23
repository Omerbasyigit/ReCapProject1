using Core.Business;
using Core.DataAccess;
using Core.Utilities.Results;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService:IBaseService<Car>
    {
        IDataResult<List<CarDetailDto>> GetAllDetail();
        IDataResult<CarDetailDto> GetCarDetail(int carId);
        IDataResult<List<CarDetailDto>> GetAllDetailByBrand(int brandId);
        IDataResult<List<CarDetailDto>> GetAllDetailByColor(int colorId);
        IDataResult<List<CarDetailDto>> GetAllByBrandAndColorId(int brandId, int colorId);
        
    }
}
