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
using MahApps.Metro.Controls;
using Newtonsoft.Json.Linq;
using WpfMiniProject.Helper;
using WpfMiniProject.Model;

namespace WpfMiniProject
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            StsResult.Content = "";

            if (string.IsNullOrEmpty(TxtMovieName.Text))
            {
                StsResult.Content = "검색할 영화명을 입력 후, 검색 버튼을 눌러주세요";
                return;
            }

            // Commons.ShowMessageAsync("결과", $"{TxtMovieName.Text}");
            ProSearchNaverApi(TxtMovieName.Text);
        }

        private void ProSearchNaverApi(string movieName)
        {
            string clientID = "Mxe3E8mU7xfCj5OHPpeK";//나의 네이버 클라이언트 아이디
            string clientSecret = "yPekeefDmu";
            string openApiUrl = $"https://openapi.naver.com/v1/search/movie?query={movieName}";

            string resJson = Commons.GetOpenApiResult(openApiUrl, clientID, clientSecret);
            var parsedJson = JObject.Parse(resJson);

            int total = Convert.ToInt32(parsedJson["total"]);
            int display = Convert.ToInt32(parsedJson["display"]);

            StsResult.Content = $"{total} 중 {display} 호출 성공";

            var items = parsedJson["items"];
            var json_array = (JArray)items;

            List<Movieitem> movieItems = new List<Movieitem>();

            foreach (var item in json_array)
            {
                Movieitem movie = new Movieitem(
                    item["title"].ToString(),
                    item["link"].ToString(),
                    item["image"].ToString(),
                    item["subtitle"].ToString(),
                    item["pubDate"].ToString(),
                    item["director"].ToString(),
                    item["actor"].ToString(),
                    item["userRating"].ToString());
                movieItems.Add(movie);
            }
            this.DataContext = movieItems;
        }

        private void TxtMovieName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) BtnSearch_Click(sender, e);
        }
    }
}
