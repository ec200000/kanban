using Kanban.BL;
using Kanban.InterfaceLayer;
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

    public InterfaceLayerUser Login()
    {
        UserService auth = new UserService();
        bool b = auth.login(email, pwd);
        if (b) {
            InterfaceLayerUser user = auth.GetUser(email);
            return user;
        }
        else
        {
            return null;
        }
    }

    public bool SignUp() {
        UserService auth = new UserService();
        bool user = auth.signUp(email, pwd);
        return user;
    }

}
