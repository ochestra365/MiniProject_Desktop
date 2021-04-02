using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            ImgPoster.Source = ImgPoster.Source = new BitmapImage(new Uri(" No_Picture.jpg", UriKind.RelativeOrAbsolute));

            if (string.IsNullOrEmpty(TxtMovieName.Text))
            {
                StsResult.Content = "검색할 영화명을 입력 후, 검색 버튼을 눌러주세요";
                return;
            }

            // Commons.ShowMessageAsync("결과", $"{TxtMovieName.Text}");
            try
            {
                ProSearchNaverApi(TxtMovieName.Text);
            }
            catch (Exception ex)
            {
                Commons.ShowMessageAsync("예외", $"예외발생 : {ex}");
            }
        }

        private void ProSearchNaverApi(string movieName)
        {
            string clientID = "Mxe3E8mU7xfCj5OHPpeK";//나의 네이버 클라이언트 아이디
            string clientSecret = "yPekeefDmu";
            string openApiUrl = $"https://openapi.naver.com/v1/search/movie?start=1&display=30&query={movieName}";

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
                    Commons.StripHtmlTag(item["title"].ToString()),
                    item["link"].ToString(),
                    item["image"].ToString(),
                    item["subtitle"].ToString(),
                    item["pubDate"].ToString(),
                    Commons.StripHtmlTag(item["director"].ToString()),
                    Commons.StripHtmlTag(item["actor"].ToString()),
                    item["userRating"].ToString());
                movieItems.Add(movie);
            }
            this.DataContext = movieItems;
        }

        private void TxtMovieName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) BtnSearch_Click(sender, e);
        }

        private void GrdData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (GrdData.SelectedItem == null)
            {
                Commons.ShowMessageAsync("오류", "영화를 선택하세요!");
                return;
            }
            if (GrdData.SelectedItem is Movieitem)
            {
                var movie = GrdData.SelectedItem as Movieitem;
                //Commons.ShowMessageAsync("결과", $"{movie.Image}");
                if (string.IsNullOrEmpty(movie.Image))
                {
                    ImgPoster.Source = new BitmapImage(new Uri(" No_Picture.jpg", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    ImgPoster.Source = new BitmapImage(new Uri(movie.Image, UriKind.RelativeOrAbsolute));
                }
            }
        }

        private void BtnAddWatchList_Click(object sender, RoutedEventArgs e)
        {
            /*   if(GrdData.SelectedItems.Count==0)
               {
                   Commons.ShowMessageAsync("오류", "즐겨찾기에 추가할 영화를 선택하세요(복수선택가능)");//이런 간단한 메시지가 사용자를 도와주는 것이다.
                   return;
               }*/

            List<NaverFavoiriteMovies> list = new List<NaverFavoiriteMovies>();

            foreach (Movieitem item in GrdData.SelectedItems)
            {
                NaverFavoiriteMovies temp = new NaverFavoiriteMovies()
                {
                    Title = item.Title,
                    Link = item.Link,
                    Image = item.Image,
                    SubTitle = item.SubTitle,
                    PubDate = item.PubDate,
                    Director = item.Director,
                    actor = item.actor,
                    UserRating = item.UserRating
                };
                list.Add(temp);
            }

            try
            {
                using (var ctx = new OpenApiLabEntities())
                {
                    ctx.Set<NaverFavoiriteMovies>().AddRange(list);
                    ctx.SaveChanges();
                }
                Commons.ShowMessageAsync("저장", $"즐겨찾기에 추가했습니다");
            }
            catch (Exception ex)
            {
                Commons.ShowMessageAsync("예외", $"예외발생 : {ex}");
                Commons.LOGGER.Error($"예외발생 : {ex}");
            }

            GrdData.SelectedItem = null;
        }

        private void BtnViewWatchList_Click(object sender, RoutedEventArgs e)
        {
            List<Movieitem> listData = new List<Movieitem>();
            List<NaverFavoiriteMovies> list = new List<NaverFavoiriteMovies>();
            try
            {
                this.DataContext = null;
                TxtMovieName.Text = "";

                using (var ctx = new OpenApiLabEntities())
                {
                    list = ctx.NaverFavoiriteMovies.ToList();
                }
            }
            catch (Exception ex)
            {
                Commons.ShowMessageAsync("예외", $"예외발생 : {ex}");
                Commons.LOGGER.Error($"예외발생 : {ex}");
            }

            foreach (var item in list)
            {
                listData.Add(new Movieitem(
                    item.Title,
                    item.Link,
                    item.Image,
                    item.SubTitle,
                    item.PubDate,
                    item.Director,
                    item.actor,
                    item.UserRating
                    ));
                this.DataContext = listData;
            }
        }

        private void BtnAddWatchTrailer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddDeleteCopy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddNaverWatchTrailer_Copy_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
