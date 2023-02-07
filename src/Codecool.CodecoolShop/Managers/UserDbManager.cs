using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Managers
{
    public class UserDbManager : BaseDbManager
    {
        private readonly IUserDbDao _userDbDao;

        public UserDbManager(IUserDbDao userDbDao)
        {
            _userDbDao = userDbDao;
        }

    public void RegisterUser(User user)
        {
            _userDbDao.Add(user);
        }
    }
}
