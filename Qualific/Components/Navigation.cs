using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Qualific.Components
{
    class Navigation
    {
        public static User AuthUser = null;
        public static bool isAuth = false;
        public static List<Nav> navs = new List<Nav>();
        public static MainWindow main;

        private static void Update(Nav nav)
        {
            main.TitlePage.Text = nav.Title;
            main.FrameMain.Navigate(nav.Page);
        }
        public static void NexPage(Nav nav)
        {
            navs.Add(nav);
            Update(nav);
        }
        public static void BackPage()
        {
            if(navs.Count > 1)
            {
                navs.RemoveAt(navs.Count - 1);
            }
            Update(navs[navs.Count - 1]);
        }
    }
    class Nav
    {
        public string Title { get; set;  }
        public Page Page { get; set;  }
        public Nav(string title, Page page)
        {
            Title = title;
            Page = page;
        }
    }
}
