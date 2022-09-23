using Core.Business;
using Core.Utilities.Results;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalService:IBaseService<Rental>
    {
        IDataResult<List<RentalDetailDto>> GetRentalDetails();
        IResult AddTransactionalTest(Rental rental);
        IDataResult<List<Rental>> GetRental(int carId);
        
    }
}
