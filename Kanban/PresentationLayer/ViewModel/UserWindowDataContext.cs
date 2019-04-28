using Kanban.BL;
using System;
using System.ComponentModel;

class UserWindowDataContext : INotifyPropertyChanged
{
    string email = "";
    public string Email
    {
        get
        {
            return email;
        }
        set
        {
            email = value;

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
        }
    }
    string pwd = "";
    public string PWD
    {
        get
        {
            return pwd;
        }
        set
        {
            pwd = value;

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("PWD"));
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;

    public User Login()
    {
        Authantication auth = new Authantication();
        User user = auth.login(email, pwd);
        return user;
    }

    public bool SignUp() {
        Authantication auth = new Authantication();
        User user = auth.signUp(email, pwd);
        return user != null;
    }

}
