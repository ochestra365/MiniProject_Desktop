﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMiniProject.Model
{
    using System;
    using System.Collections.Generic;
    class NaverFavoriteMovies
    {
        public int Idx { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public string SubTitle { get; set; }
        public string PubDate { get; set; }
        public string Director { get; set; }
        public string Actor { get; set; }
        public string UserRating { get; set; }
        public Nullable<System.DateTime> RegDate { get; set; }
    }
}
