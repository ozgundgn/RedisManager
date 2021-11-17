using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models.Concrete;

namespace DataAccess.DAL
{
    public interface IUserDal : IEntityRepository<User>
    {
    }
}
