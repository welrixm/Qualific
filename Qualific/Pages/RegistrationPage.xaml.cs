using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Qualific.Components;
using System.Data;
using System.Data.SqlClient;

namespace Qualific.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void RegistrBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTb.Text.Trim();
            string password = PasswordTb.Password.Trim();
            string firstname = FirstNameTb.Text.Trim();
            string lastname = LastNameTb.Text.Trim();
            string patronymic = PatronimycTb.Text.Trim();
            string email = EmailTb.Text.Trim();
           // DateTime datebirth = DateOfBirthDayTb..Trim();
            var check = DBConnect.db.User.Where(x => x.Login == login).FirstOrDefault();
            if (check == null)
            {
                if (login.Length > 0 && password.Length > 0 && firstname.Length>0 && lastname.Length>0 && patronymic.Length>0 && email.Length >0 )
                {
                    if(DBConnect.db.User.Local.Any(x => x.Login == login))
                    {
                        MessageBox.Show("Такой пользователь уже существует");
                        return;
                    }
                    else
                    {
                        DBConnect.db.User.Add(new User
                        {
                            Login = login,
                            Password = password,
                            FirstName = firstname,
                            LastName = lastname,
                            Patronymic = patronymic,
                            Email = email,
                            //DateOfBirthDay = datebirth,
                            RoleId = 2
                        });
                        if(password.Length > 6)
                        {
                            bool symbol = false;
                            bool number = false;
                            bool IsAllUpper = false;
                            for(int i = 0; i < password.Length; i++)
                            {
                                if (password[i] >= '0' && password[i] <= '9') number = true;
                                if (password[i] == '!' || password[i] == '@' || password[i] == '#' || password[i] == '$' || password[i] == '%' || password[i] == '^') symbol = true;
                                if (Char.IsUpper(password[i])) IsAllUpper = true;
                            }
                            if (!symbol)
                            {
                                MessageBox.Show("Добавьте один из нескольких символов: ! @ # $ % ^ ");

                            }
                            else if (!number)
                            {
                                MessageBox.Show("Добавьте хотя бы одну цифру");
                            }
                            else if (!IsAllUpper)
                            {
                                MessageBox.Show("Добавьте олну прописную букву");
                            }
                            if(symbol && number && IsAllUpper)
                            {

                            }
                        }
                        else
                        {
                            MessageBox.Show("Пароль слишком короткий");
                        }
                        MessageBox.Show("Пользователь успешно зарегистрирован");
                        DBConnect.db.SaveChanges();
                    }
                }
                else
                {
                    MessageBox.Show("Заполните поля.");
                }
            }

        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginTb.Text = "";
            PasswordTb.Password = "";
            FirstNameTb.Text = "";
            LastNameTb.Text = "";
            PatronimycTb.Text = "";
            EmailTb.Text = "";

        }
    }
}
