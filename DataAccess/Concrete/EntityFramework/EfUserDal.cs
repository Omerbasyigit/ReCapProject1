using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.Entities.Dto;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal:EfEntityRepositoryBase<User,NorthwindContext>,IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context= new NorthwindContext())
            {
                var result = from oc in context.OperationClaims
                             join uo in context.UserOperationClaims
                             on oc.Id equals uo.OperationClaimId
                             where uo.UserId == user.Id
                             select new OperationClaim
                             {
                                 Id = oc.Id,
                                 Name = oc.Name
                             };
                return result.ToList();
            }
        }

        public UserDetailDto GetUser(Expression<Func<UserDetailDto, bool>> filter)
        {
            using (var context = new NorthwindContext())
            {
                var result = from u in context.Users
                             join uo in context.UserOperationClaims
                             on u.Id equals uo.UserId
                             join oc in context.OperationClaims
                             on uo.OperationClaimId equals oc.Id
                             select new UserDetailDto
                             {
                                 UserId = u.Id,
                                 UserName=u.FirstName,
                                 UserLastName=u.LastName,
                                 Email=u.Email,
                                 UserOperationClaim= oc.Name
                             };
                return result.Where(filter).SingleOrDefault();
            }
        }
    }
}
