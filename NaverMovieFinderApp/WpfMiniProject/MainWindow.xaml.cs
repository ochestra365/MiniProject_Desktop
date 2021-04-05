using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                Commons.ShowMessageAsync("검색","검색할 영화명을 입력 후, 검색버튼을 눌러주세요");
                return;
            }

            // Commons.ShowMessageAsync("결과", $"{TxtMovieName.Text}");
            try
            {
                ProSearchNaverApi(TxtMovieName.Text);
                Commons.ShowMessageAsync("검색","영화검색 완료");
            }
            catch (Exception ex)
            {
                Commons.ShowMessageAsync("예외", $"예외발생 : {ex}");
                Commons.LOGGER.Error($"예외발생 : {ex}");
            }
            Commons.isFavorite = false; //즐겨찾기 아님
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
          /*  if (GrdData.SelectedItem == null)
            {
                Commons.ShowMessageAsync("오류", "영화를 선택하세요!");
                return;
            }*/
            if (GrdData.SelectedItem is Movieitem)
            {
                var movie = GrdData.SelectedItem as Movieitem;
                //Commons.ShowMessageAsync("결과", $"{movie.Image}");
                if (string.IsNullOrEmpty(movie.Image))
                {
                    ImgPoster.Source = new BitmapImage(new Uri("No_Picture.jpg", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    ImgPoster.Source = new BitmapImage(new Uri(movie.Image, UriKind.RelativeOrAbsolute));
                }
            }

            if (GrdData.SelectedItem is NaverFavoiriteMovies)
            {
                var movie = GrdData.SelectedItem as NaverFavoiriteMovies;
                //Commons.ShowMessageAsync("결과", $"{movie.Image}");
                if (string.IsNullOrEmpty(movie.Image))
                {
                    ImgPoster.Source = new BitmapImage(new Uri("No_Picture.jpg", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    ImgPoster.Source = new BitmapImage(new Uri(movie.Image, UriKind.RelativeOrAbsolute));
                }
            }
        }

        private void BtnAddWatchList_Click(object sender, RoutedEventArgs e)
        {
            if (GrdData.SelectedItems.Count == 0)
            {
                Commons.ShowMessageAsync("오류", "즐겨찾기에 추가할 영화를 선택하세요(복수선택가능)");//이런 간단한 메시지가 사용자를 도와주는 것이다.
                return;
            }

            if(Commons.isFavorite)
            {   //이미 즐겨찾기한 내용을 막아주기 위해서이다.
                Commons.ShowMessageAsync("즐겨찾기", "즐겨찾기 조회내용을 다시 즐겨찾기할 수 없습니다.");
                return;
            }

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
                    actor = item.Actor,
                    UserRating = item.UserRating,
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
        }

        private void BtnViewWatchList_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = null;
            TxtMovieName.Text = "";

           // List<Movieitem> listData = new List<Movieitem>();
            List<NaverFavoiriteMovies> list = new List<NaverFavoiriteMovies>();
            Commons.isFavorite = false;//한번더 명시적으로 처리.
            try
            {
                using (var ctx = new OpenApiLabEntities())
                {
                    list = ctx.NaverFavoiriteMovies.ToList();
                }
                this.DataContext = list;
                StsResult.Content= $"즐겨찾기 {list.Count}개 조회";
                if (Commons.isDelete)
                    Commons.ShowMessageAsync("즐겨찾기", "즐겨찾기 삭제완료");
                else
                    Commons.ShowMessageAsync("즐겨찾기", "즐겨찾기 조회 완료");

                Commons.isFavorite = true;
            }
            catch (Exception ex)
            {
                Commons.ShowMessageAsync("예외", $"예외발생 : {ex}");
                Commons.LOGGER.Error($"예외발생 : {ex}");
                Commons.isFavorite = false;
            }
           /* foreach (var item in list) --> 변환 필요 없음.
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
                StsResult.Content=$"즐겨찾기 {listData.Count}개 조회";
                Commons.ShowMessageAsync("즐겨찾기", "즐겨찾기 조회완료");
                Commons.isFavorite = true;//즐겨찾기 맞음.
            }*/
        }

        private void BtnAddWatchTrailer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddDeleteCopy_Click(object sender, RoutedEventArgs e)//삭제할 때도 pk값이 삭제되는 것이 중요하다.
        {
            if (Commons.isFavorite == false)
            {
                Commons.ShowMessageAsync("즐겨찾기,", "즐겨찾기 내용이 아니면 삭제할 수 없습니다.");
                return;
            }
            if(GrdData.SelectedItems.Count==0)
            {
                Commons.ShowMessageAsync("즐겨찾기,", "삭제할 즐겨찾기 영화를 선택하세요.");
                return;
            }
            //List<NaverFavoiriteMovies> removelist = new List<NaverFavoiriteMovies>();
            foreach (NaverFavoiriteMovies item in GrdData.SelectedItems)
            {
                using (var ctx = new OpenApiLabEntities())
                {
                    // ctx.NaverFavoiriteMovies.RemoveRange(removelist);
                    var itemDelete = ctx.NaverFavoiriteMovies.Find(item.idx);//삭제할 영화 객체 검색 후 생성
                    ctx.Entry(itemDelete).State = EntityState.Deleted;//검색객체 상태를 삭제로 변경
                    ctx.SaveChanges();//commit
                }
            }
            Commons.isDelete = true;
            // 조회쿼리 다시
            BtnViewWatchList_Click(sender, e);
        }
        private void BtnAddNaverWatchTrailer_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (GrdData.SelectedItems.Count == 0)
            {
                Commons.ShowMessageAsync("유튜브 영화", "영화를 선택하세요.");
                return;
            }
            if (GrdData.SelectedItems.Count > 1)
            {
                Commons.ShowMessageAsync("유튜브 영화", "영화를 하나만 선택하세요.");
                return;
            }

            string movieName = "";
            if (Commons.isFavorite)//즐겨찾기
            {
                var item = GrdData.SelectedItem as NaverFavoiriteMovies;
                //MessageBox.Show(item.Link);
                movieName = item.Title;
            }
            else//네이버API
            {
                var item = GrdData.SelectedItem as Movieitem;
                //MessageBox.Show(item.Link);
                movieName = item.Link;
            }

            var trailerWindow = new TrailerWindow(movieName);
            trailerWindow.Owner = this;
            trailerWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            trailerWindow.ShowDialog();
            //선택된 영화의 네이버영화 URL 가져오기
            string linUrl = "";
            if(Commons.isFavorite)//즐겨찾기
            {
                var item = GrdData.SelectedItem as NaverFavoiriteMovies;
                //MessageBox.Show(item.Link);
            }
            else//네이버API
            {
                var item = GrdData.SelectedItem as Movieitem;
                //MessageBox.Show(item.Link);
                linUrl = item.Link;
            }
            Process.Start(linUrl);// 웹 브라우저 띄우기.
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"즐겨찾기 여부는 : {Commons.isFavorite}");
        }
    }
}
