using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MaliMali
{
    public partial class Tabbed : TabbedPage
    {
        public Tabbed()
        {
            InitializeComponent();
        }

        private void MainPage_Appearing(object sender, EventArgs e)
        {
            var activeImage = "home1.png";
            homepage.Icon = new FileImageSource() { File = activeImage };
        }

        private void MainPage_Disappearing(object sender, EventArgs e)
        {
            var inactiveImage = "home.png";
            homepage.Icon = new FileImageSource() { File = inactiveImage };
        }
        ////////////////////////////////////////////////////////////////


        private void CartPage_Appearing(object sender, EventArgs e)
        {
            var activeImage = "cart1.png";
            cartpage.Icon = new FileImageSource() { File = activeImage };
        }

        private void CartPage_Disappearing(object sender, EventArgs e)
        {
            var inactiveImage = "cart.png";
            cartpage.Icon = new FileImageSource() { File = inactiveImage };
        }
        ////////////////////////////////////////////////////////////////


        private void AdminPage_Appearing(object sender, EventArgs e)
        {
            var activeImage = "camer1.png";
            adminpage.Icon = new FileImageSource() { File = activeImage };
        }

        private void AdminPage_Disappearing(object sender, EventArgs e)
        {
            var inactiveImage = "camera.png";
            adminpage.Icon = new FileImageSource() { File = inactiveImage };
        }
        ////////////////////////////////////////////////////////////////


        private void HistoryPage_Appearing(object sender, EventArgs e)
        {
            var activeImage = "stats1.png";
            historypage.Icon = new FileImageSource() { File = activeImage };
        }

        private void HistoryPage_Disappearing(object sender, EventArgs e)
        {
            var inactiveImage = "stats.png";
            historypage.Icon = new FileImageSource() { File = inactiveImage };
        }
        ////////////////////////////////////////////////////////////////

    }
}
