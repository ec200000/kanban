using System;

/*namespace Kanban.BL
{
    class Tests
    {
        Authantication auth = new Authantication();
        public bool checkSignUp(string email, string password)//only one user
        {
            bool check = true;
            User user = auth.signUp(email, password);//should work
            if(user == null || !user.getEmail().Equals(email))
            {
                Console.WriteLine("failed first check - signup");
                check =  false;
            }
            user = auth.signUp(null, password);//error - email null
            if (user != null)
            {
                Console.WriteLine("failed second check - signup");
                check =  false;
            }
            user = auth.signUp(email, password);//error - user already in the system
            if (user != null)
            {
                Console.WriteLine("failed third check - signup");
                check =  false;
            }
            return check;
        }

        public bool checkLogin(string email, string password)//only one user
        {
            bool check = true;
            User user = Authantication.login(email, password);
            if (user == null || user.getEmail() == null || !user.getEmail().Equals(email))//should work
            {
                check = false;
                Console.WriteLine("failed first check - login");
            }
            user = Authantication.login(null, password);//error - email null
            if (user != null)
            {
                check = false;
                Console.WriteLine("failed second check - login");
            }
            user = Authantication.login(email, null);
            if (user != null)//error - password null
            {
                check = false;
                Console.WriteLine("failed third check - login");
            }
            return check;
        }

        public bool checkChangeTitle(User user, Task taskToChange, string newTitle)
        {
            String str = new String('a', 60);
            bool check = true;
            if (user.changeTitle(null, newTitle))//error - task null
            {
                check = false;
                Console.WriteLine("failed first check - change title");
            }
            if (!user.changeTitle(taskToChange, newTitle))//should work
            {
                check = false;
                Console.WriteLine("failed second check - change title");
            }
            if (user.changeTitle(taskToChange, null))//error - title null
            {
                check = false;
                Console.WriteLine("failed third check - change title");
            }
            if (user.changeTitle(taskToChange, str))//error - title length too long
            {
                check = false;
                Console.WriteLine("failed fourth check - change title");
            }
            return check;
        }

        public bool checkChangeDescription(User user, Task taskToChange, string newDes)
        {
            String str = new String('a', 350);
            bool check = true;
            if (user.changeDescription(null, newDes))//error - task null
            {
                check = false;
                Console.WriteLine("failed first check - change description");
            }
            if (!user.changeTitle(taskToChange, newDes))//should work
            {
                check = false;
                Console.WriteLine("failed second check - change description");
            }
            if (user.changeTitle(taskToChange, null))//error - description null
            {
                check = false;
                Console.WriteLine("failed third check - change description");
            }
            if (user.changeTitle(taskToChange, str))//error - description length too long
            {
                check = false;
                Console.WriteLine("failed fourth check - change description");
            }
            return check;
        }


        public bool checkChangeDueDate(User user, Task taskToChange, string newDueDate)
        {
            bool check = true;
            if (user.changeDescription(null, newDueDate))//error - task null
            {
                check = false;
                Console.WriteLine("failed first check - change due date");
            }
            if (!user.changeTitle(taskToChange, newDueDate))//should work
            {
                check = false;
                Console.WriteLine("failed second check - change due date");
            }
            if (user.changeTitle(taskToChange, null))//error - due date null
            {
                check = false;
                Console.WriteLine("failed third check - change due date");
            }
            //due date will always will be in date format because we will use DateTimePicker
            return check;
        }

        public bool checkCreateTask(User user, string title, string description, string dueDate)
        {
            bool check = true;
            if (!user.CreateTask(title,description,dueDate))//should work
            {
                check = false;
                Console.WriteLine("failed first check - create task");
            }
            if (user.CreateTask(null, description, dueDate))//error - title null
            {
                check = false;
                Console.WriteLine("failed second check - create task");
            }
            if (user.CreateTask(title, null, dueDate))//error - description null
            {
                check = false;
                Console.WriteLine("failed third check - create task");
            }
            if (user.CreateTask(title, description, null))//error - dueDate null
            {
                check = false;
                Console.WriteLine("failed fourth check - create task");
            }
            return check;
        }

        public static bool checkPromoteTaskToNextPhase(User user, Task task)
        {
            bool check = true;
            if (!user.PromoteTaskToNextPhase(task))//should work
            {
                check = false;
                Console.WriteLine("failed first check - promote task to next phase");
            }
            task = new Task(null, "b", "15/5/2019");
            if (user.PromoteTaskToNextPhase(task))//error - title null
            {
                check = false;
                Console.WriteLine("failed second check - promote task to next phase");
            }
            task = new Task("a", null, "15/5/2019");
            if (user.PromoteTaskToNextPhase(task))//error - description null
            {
                check = false;
                Console.WriteLine("failed third check - promote task to next phase");
            }
            task = new Task("a", "b", null);
            if (user.PromoteTaskToNextPhase(task))//error - due date null
            {
                check = false;
                Console.WriteLine("failed fourth check - promote task to next phase");
            }
            return check;
        }
    }
}*/
