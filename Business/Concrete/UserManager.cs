using System.Collections.Generic;
using Business.Abstract;
using DataAccess.DAL;
using DataAccess.Models.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public User Get(int id)
        {
            return _userDal.Get(x => x.Id == id);
        }

        public List<User> GetAll()
        {
            return _userDal.GetAll();
        }
    }
}
