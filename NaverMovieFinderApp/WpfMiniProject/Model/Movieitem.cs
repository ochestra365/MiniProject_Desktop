namespace WpfMiniProject.Model
{
    public class Movieitem
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public string SubTitle { get; set; }
        public string PubDate { get; set; }
        public string Director { get; set; }
        public string actor { get; set; }
        public string UserRating { get; set; }

        public Movieitem(string title, string link, string image, string subtitle, string pubDate, string director, string actor, string userRating)
        {
            Title = title;
            Link = link;
            Image = image;
            SubTitle = subtitle;//리스폰스를 날릴 때 네이버에서 쓰는 시그니처라서 대소문자 일치를 시켜줘야 한다.
            PubDate = pubDate;
            Director = director;
            this.actor = actor;
            UserRating = userRating;
        }
    }
}
