using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using WpfSMSApp.View;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace WpfSMSApp.View.User
{
    /// <summary>
    /// MyAccountView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserList : Page
    {
        public UserList()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RdoAll.IsChecked = true;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 UserList Loaded: {ex}");
                throw ex;
            }
        }

        private void BtnEditMyAccount_Click(object sender, RoutedEventArgs e)//여기서 잘못되었다. 네비게이션 기능이 겁나 좋은 기능이다.
        {
            try
            {
                NavigationService.Navigate(new UserList());//이거 아닌데? MyAccount에서 시스템 자원 새로 할당하는 건데? 뭐지? // 답은 내가 View의 Account에서 생성하지 않고 프로젝트 영역에 cs를 생성해서 그렇다.
                //사소한 실수가 네임스페이스를 다 꼬이게 만들 수 있어..
            }
            catch (Exception ex)
            {

                Commons.LOGGER.Error($"예외발생 BtnAccount_Click : {ex}");//NLOG를 활용하는 파트이다.
            }
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                NavigationService.Navigate(new AddUser());
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnAddUser_Click : {ex}");
                throw ex;
            }
        }

        private void BtnEditUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new EditUser());//using System.Windows.Navigation;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnEditUser_Click : {ex}");
                throw ex;
            }
        }

        private void BtnDeactivateUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new DeactivaUser());//using System.Windows.Navigation;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnEditUser_Click : {ex}");
                throw ex;
            }
        }

        private void BtnExportPdf_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PDF File(*.pdf)|*.pdf";
            saveDialog.FileName = "";
            saveDialog.ShowDialog();
            //위에는 저장하는 파트
            if (saveDialog.ShowDialog() == true)
            {
                //pdf변환
                try
                {
                    iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12);
                    string pdfFilePath = saveDialog.FileName;
                    
                    Document pdfDoc =new Document(PageSize.A4);

                    // 1. pdf 객체생성시작
                    PdfPTable pdfTable = new PdfPTable(GrdData.Columns.Count);

                    // 2. pdf 내용 만들기
                    string nanumttf = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), @"Fonts\NanumGothic.ttf");
                    BaseFont nanumBase = BaseFont.CreateFont(nanumttf, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    var nanumFont = new iTextSharp.text.Font(nanumBase, 16f);//한글로 안나오는데에 대해서 하는 거임

                    Paragraph title = new Paragraph($"PKNU Stock Management System: { DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");//using문 중 모호한 참조가 있다면 지워줘야 한다.

                    // 3. pdf 파일생성
                    using(FileStream stream = new FileStream(pdfFilePath, FileMode.OpenOrCreate))
                    {
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        // 2번에서 만든 내용을 추가한다.

                        pdfDoc.Close();
                        stream.Close();//option
                    }
                }
                catch (Exception ex)
                {
                    Commons.LOGGER.Error($"예외발생 BtnExportPdf_Click: {ex}");
                }
            } 
        }

        private void RdoAll_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                List<WpfSMSApp.Model.User> users = new List<Model.User>();//네임스페이스의 충돌이 발생해서 그렇다. 개발하다 보니 어쩔 수 없는 것이다. 그래서 개발자가 여기는 따로 경로를 설정해줘야 한다.

                if (RdoAll.IsChecked==true)
                {
                    users = Logic.DataAccess.GetUsesr();
                }

                this.DataContext = users;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 : {ex}");
            }
        }

        private void RdoActive_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                List<WpfSMSApp.Model.User> users = new List<Model.User>();//네임스페이스의 충돌이 발생해서 그렇다. 개발하다 보니 어쩔 수 없는 것이다. 그래서 개발자가 여기는 따로 경로를 설정해줘야 한다.

                if (RdoActive.IsChecked == true)
                {
                    users = Logic.DataAccess.GetUsesr().Where(u => u.UserActivated == true).ToList();
                }

                this.DataContext = users;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 : {ex}");
            }
        }

        private void RdoDeactive_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                List<WpfSMSApp.Model.User> users = new List<Model.User>();//네임스페이스의 충돌이 발생해서 그렇다. 개발하다 보니 어쩔 수 없는 것이다. 그래서 개발자가 여기는 따로 경로를 설정해줘야 한다.

                if (RdoDeactive.IsChecked == true)
                {
                    users = Logic.DataAccess.GetUsesr().Where(u => u.UserActivated == false).ToList();
                }

                this.DataContext = users;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 : {ex}");
            }
        }

        private void BtnExportPdf_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
