using System;

using Task3.DoNotChange;
using Task3.Exceptions;

namespace Task3
{
    public sealed class UserTaskService
    {
        private readonly IUserDao _userDao;

        public UserTaskService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public void AddTaskForUser(int userId, UserTask task)
        {
            if (userId < 0)
                throw new UserTaskNegativeIdException();

            var user = _userDao.GetUser(userId);
            if (user == null)
                throw new UserTaskAlreadyExistsException();

            var tasks = user.Tasks;
            foreach (var t in tasks)
            {
                if (string.Equals(task.Description, t.Description, StringComparison.OrdinalIgnoreCase))
                    throw new UserTaskNotMatchException();
            }

            tasks.Add(task);
        }
    }
}