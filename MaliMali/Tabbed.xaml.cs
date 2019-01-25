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

    }
}
