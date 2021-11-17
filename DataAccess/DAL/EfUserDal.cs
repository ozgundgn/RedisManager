using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models.Concrete;

namespace DataAccess.DAL
{
    public class EfUserDal : EntityRepositoryBase<NorthwindContext, User>, IUserDal
    {
    }
}
