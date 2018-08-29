using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Windows.Input;
namespace SmartPhone.ViewModels
{
	public class MySettingsViewModel: INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public ICommand SaveCommand { protected set; get; }
		public ICommand CancelCommand { protected set; get; }

        ILocalData _db;
		public MySettingsViewModel ()
		{
            _db = DependencyService.Get<ILocalData>();
            var user=_db.GetUserInfo ();
			if (user != null) {
				id = user.Id;
				lastName = user.LastName;
				firstName = user.FirstName;
				email = user.Email;
				phone = user.Phone;
				mobile = user.Mobile;
				receiveEmail = user.ReceiveEmail;
				receiveNotification = user.ReceiveNotification;
                token = user.Token;
                serverEndPoint = user.ServerEndPoint;
            }

			this.SaveCommand = new Command(() =>
				{
					if(SaveUser()>0){
						ReturnMessage="Saved";
                    }
				});
			this.CancelCommand = new Command(() =>
				{
					LastName=null;
					FirstName=null;
					Email=null;
					Phone=null;
					Mobile=null;
					ReceiveEmail=false;
					ReceiveNotification=false;
					if(SaveUser()>0){
						ReturnMessage="Canceled";
					}
				});
		}
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}


		int id;
		string lastName,firstName,email,phone,mobile,returnMessage,token,serverEndPoint;
		bool receiveEmail,receiveNotification;

		public string ReturnMessage { get{ return returnMessage; }
            set {
				if (returnMessage != value)
				{
					returnMessage = value;
					OnPropertyChanged("ReturnMessage");
				}
			}
		}

		public int Id { get{ return id; } set{
				if (id != value)
				{
					id = value;
					OnPropertyChanged("Id");
				} 
			} 
		}
		public string LastName { get{ return lastName; } set{
				if (lastName != value)
				{
					lastName = value;
					OnPropertyChanged("LastName");
				} 
			} 
		}
		public string FirstName { get{ return firstName; } set{
				if (firstName != value)
				{
					firstName = value;
					OnPropertyChanged("FirstName");
				} 
			} 
		}
		public string Email { get{ return email; } set{
				if (email != value)
				{
					email = value;
					OnPropertyChanged("Email");
				} 
			} 
		}
		public string Phone { get{ return phone; } set{
				if (phone != value)
				{
					phone = value;
					OnPropertyChanged("Phone");
				} 
			} 
		}
		public string Mobile { get{ return mobile; } set{
				if (mobile != value)
				{
					mobile = value;
					OnPropertyChanged("Mobile");
				} 
			} 
		}
		public bool ReceiveEmail{ get{ return receiveEmail; } set{
				if (receiveEmail != value)
				{
					receiveEmail = value;
					OnPropertyChanged("ReceiveEmail");
				} 
			} 
		}
		public bool ReceiveNotification{ get{ return receiveNotification; } set{
				if (receiveNotification != value)
				{
					receiveNotification = value;
					OnPropertyChanged("ReceiveNotification");
				} 
			} 
		}
        public string Token
        {
            get { return token; }
            set
            {
                if (token != value)
                {
                    token = value;
                    OnPropertyChanged("Token");
                }
            }
        }
        public string ServerEndPoint
        {
            get { return serverEndPoint; }
            set
            {
                if (serverEndPoint != value)
                {
                    serverEndPoint = value;
                    OnPropertyChanged("ServerEndPoint");
                }
            }
        }
        private int SaveUser(){
            if (String.IsNullOrEmpty(Token))
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhcnRodXIubHY2QGdtYWlsLmNvbSIsImp0aSI6ImRkMjNlOTY1LWM5ZjQtNGEwMC1hYzEyLWQ4YmNmZjY4OTk4OSIsImVtYWlsIjoiYXJ0aHVyLmx2NkBnbWFpbC5jb20iLCJleHAiOjE1NDU0NDcyMjIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDkzNjEvIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0OTM2MS8ifQ.A4RWrc10i_p5cLRPPaesWT_1DYibfJvSCWm8YYhXZGc";

            return _db.SaveUserInfo(new UserInfo()
            {
                Id =Id,
                LastName =LastName,
                FirstName =FirstName,
                Email =Email,
                Phone =Phone,
                Mobile =Mobile,
                ReceiveEmail =ReceiveEmail,
                ReceiveNotification =ReceiveNotification,
                Token =Token,
                ServerEndPoint=ServerEndPoint
            });
		}
	}
}

