using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using System.Linq;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {                                                                                           // ANALYZE IT LATER....
            using (var context = new NorthwindContext())
            {
                //var result = new List<OperationClaim>();
                //var userOperationClaims = context.UserOperationClaims.Where(o=>o.UserId==user.Id).ToList();
                //var operationClaims = userOperationClaims.Select(c => c.OperationClaimId).ToList();
                //foreach(var operationClaim in operationClaims)
                //{
                //    result.Add(context.OperationClaims.FirstOrDefault(o => o.Id == operationClaim));
                //}
                //return result;

                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();
            }
        }
    }
}