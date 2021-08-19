using Task3.DoNotChange;
using Task3.Exceptions;

namespace Task3
{
    public sealed class UserTaskController
    {
        private readonly UserTaskService _taskService;

        public UserTaskController(UserTaskService taskService)
        {
            _taskService = taskService;
        }

        public bool AddTaskForUser(int userId, string description, IResponseModel model)
        {
            string message = GetMessageForModel(userId, description);
            if (message != null)
            {
                model.AddAttribute("action_result", message);
                return false;
            }

            return true;
        }

        private string GetMessageForModel(int userId, string description)
        {
            var task = new UserTask(description);

            string result = null;

            try
            {
                _taskService.AddTaskForUser(userId, task);
            }
            catch (UserTaskNegativeIdException)
            {
                result = "Invalid userId";
            }
            catch (UserTaskAlreadyExistsException)
            {
                result = "User not found";
            }
            catch (UserTaskNotMatchException)
            {
                result = "The task already exists";
            }
           
            return result;
        }
    }
}