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

namespace WpfSMSApp
{
    /// <summary>
    /// MyAccountView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MyAccountView : Page
    {
        public MyAccountView()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = Commons.LOGINED_USER;
                TxtUserId.Text = user.UserID.ToString();
                TxtUserIdentityNumber.Text = user.UserIdentityNumber.ToString();
                TxtUserSurName.Text = user.UserSurname.ToString();
                TxtUserEmail.Text = user.UserEmail.ToString();
                TxtUserName.Text = user.UserName.ToString();
                TxtUserAdmin.Text = user.UserAdmin.ToString();
                TxtUserActivated.Text = user.UserActivated.ToString();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 MyAccount Loaded: {ex}");
                throw ex;
            }
        }

        private void BtnEditMyAccount_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
