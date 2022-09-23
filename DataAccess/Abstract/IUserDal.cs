using Core.DataAccess;
using Core.Entities.Concrete;
using Entity.Concrete;
using Core.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUserDal: IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        UserDetailDto GetUser(Expression<Func<UserDetailDto, bool>> filter);
    }
}
