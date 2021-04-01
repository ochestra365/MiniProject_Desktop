﻿using System;
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

namespace WpfSMSApp.View.Store
{
    /// <summary>
    /// MyAccountView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StoreList : Page
    {
        public StoreList()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Store 테이블 데이터 읽어와야 함.
                List<Model.Store> stores = new List<Model.Store>();
                List<Model.StockStores> stockstores = new List<Model.StockStores>();
                List<Model.Store> stocks = new List<Model.Store>();
                stores = Logic.DataAccess.GetStores();
                stocks = Logic.DataAccess.GetStocks();
                this.DataContext = stores;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 StoreList Loaded: {ex}");
                throw ex;
            }
        }

        private void BtnEditMyAccount_Click(object sender, RoutedEventArgs e)//여기서 잘못되었다. 네비게이션 기능이 겁나 좋은 기능이다.
        {
            try
            {
                //NavigationService.Navigate(new UserList());//이거 아닌데? MyAccount에서 시스템 자원 새로 할당하는 건데? 뭐지? // 답은 내가 View의 Account에서 생성하지 않고 프로젝트 영역에 cs를 생성해서 그렇다.
                //사소한 실수가 네임스페이스를 다 꼬이게 만들 수 있어..
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnAccount_Click : {ex}");//NLOG를 활용하는 파트이다.
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
                    //0. PDF 사용 폰트 설정.
                    string nanumPath = Path.Combine(Environment.CurrentDirectory, @"NanumGothic.ttf");
                    BaseFont nanumBase = BaseFont.CreateFont(nanumPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    var nanumTitle = new iTextSharp.text.Font(nanumBase, 20f);  //20 타이틀용 나눔폰트폰트라는 폴더가 2개가 있으니 명확하게 사용자가 어느 폰트를 사용할 건지 지정해줘야 한다. 
                    var nanumContent = new iTextSharp.text.Font(nanumBase, 12f);// 12내용 나눔폰트,

                    //iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12);
                    string pdfFilePath = saveDialog.FileName;

                    // 1. pdf 객체생성시작
                    Document pdfDoc = new Document(PageSize.A4);

                    // 2. pdf 내용 만들기
                    /* string nanumttf = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), @"Fonts\NanumGothic.ttf");
                     BaseFont nanumBase = BaseFont.CreateFont(nanumttf, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                     var nanumFont = new iTextSharp.text.Font(nanumBase, 16f);//한글로 안나오는데에 대해서 하는 거임*/
                    Paragraph title = new Paragraph($"부경대 재고 관리 시스템(SMS)\n", nanumTitle);//using문 중 모호한 참조가 있다면 지워줘야 한다.
                    Paragraph subtitle = new Paragraph($"사용자리스트 exported : {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\n\n", nanumContent);//using문 중 모호한 참조가 있다면 지워줘야 한다.
                    PdfPTable pdfTable = new PdfPTable(GrdData.Columns.Count);
                    pdfTable.WidthPercentage = 100;//전체 사이즈 다 쓰는 것이다.
                    // 그리드 헤더 작업
                    var index = 0;
                    foreach (DataGridColumn column in GrdData.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.Header.ToString(), nanumContent));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        pdfTable.AddCell(cell);
                    }
                    float[] columnWdith = new float[] { 7f, 15f, 10f, 15f, 28f, 10f, 10f };
                    pdfTable.SetWidths(columnWdith);
                    //그리드 Row 작업
                    foreach (var item in GrdData.Items)
                    {
                        if(item is Model.User)
                        {
                            var temp = item as Model.User;
                            //UserId
                            PdfPCell cell = new PdfPCell(new Phrase(temp.UserID.ToString(), nanumContent));
                            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            pdfTable.AddCell(cell);
                            //UserIdentityNumber
                            cell = new PdfPCell(new Phrase(temp.UserIdentityNumber.ToString(), nanumContent));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            pdfTable.AddCell(cell);
                            //UserSurName
                            cell = new PdfPCell(new Phrase(temp.UserSurname.ToString(), nanumContent));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            pdfTable.AddCell(cell);
                            //UserName
                            cell = new PdfPCell(new Phrase(temp.UserName.ToString(), nanumContent));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            pdfTable.AddCell(cell);
                            //UserEmail
                            cell = new PdfPCell(new Phrase(temp.UserEmail.ToString(), nanumContent));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            pdfTable.AddCell(cell);
                            //UserAdmin
                            cell = new PdfPCell(new Phrase(temp.UserAdmin.ToString(), nanumContent));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            pdfTable.AddCell(cell);
                            //UserActivated
                            cell = new PdfPCell(new Phrase(temp.UserActivated.ToString(), nanumContent));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            pdfTable.AddCell(cell);
                        }
                    }

                    // 3. pdf 파일생성
                    using (FileStream stream = new FileStream(pdfFilePath, FileMode.OpenOrCreate))
                    {
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        // 2번에서 만든 내용을 추가한다.
                        pdfDoc.Add(title);
                        pdfDoc.Add(subtitle);
                        pdfDoc.Add(pdfTable);
                        pdfDoc.Close();
                        stream.Close();//option
                    }
                    Commons.ShowMessageAsync("PDF변환", "PDF 익스포트 성공했습니다.");
                }
                catch (Exception ex)
                {
                    Commons.LOGGER.Error($"예외발생 BtnExportPdf_Click: {ex}");
                }
            } 
        }

        private void BtnExportPdf_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddStore_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditStore_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnExportEXCEL_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}