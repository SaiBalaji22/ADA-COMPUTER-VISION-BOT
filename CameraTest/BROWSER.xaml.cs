using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CameraTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BROWSER : ContentPage
    {
        public BROWSER()
        {
            InitializeComponent();
            //var s = BRclass.url;
            wbv.Source = "https://www.google.com/search?q="+BRclass.url;
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(wbv.CanGoBack)
            {
                wbv.GoBack();
            }
        }
    }
}