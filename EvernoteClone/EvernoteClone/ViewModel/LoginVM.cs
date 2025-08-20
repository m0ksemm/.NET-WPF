using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static SQLite.SQLite3;

namespace EvernoteClone.ViewModel
{
    public class LoginVM : INotifyPropertyChanged
    {
        private bool isShowingRegister = false;
		private User user;
		public User User
        {
			get { return user; }
			set 
            { 
                user = value;
                OnPropertyChanged(nameof(User));
            }
		}

        private string username;

        public string Username
        {
            get { return username; }
            set 
            { 
                username = value;
                User = new User
                {
                    Username = username,
                    Password = this.Password,
                    Name = this.Name,
                    Lastname = this.LastName,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged(nameof(Username));
            }
        }

        private string password;
        public string Password 
        { 
            get { return password; } 
            set 
            { 
                password = value;
                User = new User
                {
                    Username = this.Username,
                    Password = password,
                    Name = this.Name,
                    Lastname = this.LastName,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged(nameof(Password));
            } 
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                User = new User
                {
                    Username = this.Username,
                    Password = this.Password,
                    Name = name,
                    Lastname = this.LastName,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged(nameof(Name));
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                User = new User
                {
                    Username = this.Username,
                    Password = this.Password,
                    Name = this.Name,
                    Lastname = lastName,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged(nameof(LastName));
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                User = new User
                {
                    Username = this.Username,
                    Password = this.Password,
                    Name = this.Name,
                    Lastname = this.LastName,
                    ConfirmPassword = confirmPassword
                };
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        private Visibility loginVisibility;
        public Visibility LoginVisibility
        {
            get { return loginVisibility; }
            set 
            { 
                loginVisibility = value; 
                OnPropertyChanged(nameof(loginVisibility));
            }
        }

        private Visibility registerVisibility;
        public Visibility RegisterVisibility
        {
            get { return registerVisibility; }
            set
            {
                registerVisibility = value;
                OnPropertyChanged(nameof(registerVisibility));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler Authenticated;

        public RegisterCommand RegisterCommand { get; set; }
        public LoginCommand LoginCommand {  get; set; }
        public ShowRegisterCommand ShowRegisterCommand { get; set; }

        public LoginVM() 
        {
            LoginVisibility = Visibility.Visible;
            RegisterVisibility = Visibility.Collapsed;

            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
            ShowRegisterCommand = new ShowRegisterCommand(this);

            User = new User();
        }

        public void SwitchViews()
        {
            isShowingRegister = !isShowingRegister;

            if (isShowingRegister)
            {
                RegisterVisibility = Visibility.Visible;
                LoginVisibility = Visibility.Collapsed;
            }
            else
            {
                RegisterVisibility = Visibility.Collapsed;
                LoginVisibility = Visibility.Visible;
            }
        }

        public async void Login()
        {
            bool result = await FirebaseAuthHelper.Login(User);

            if (result) 
            {
                Authenticated.Invoke(this, new EventArgs());
            }
        }
        public async void Register()
        {
            bool result = await FirebaseAuthHelper.Register(User);

            if (result)
            {

            }
        }

        private void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
