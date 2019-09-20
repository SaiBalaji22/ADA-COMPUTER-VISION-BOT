using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Media;
using Microsoft.ProjectOxford.Text;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Newtonsoft;
using System.Net.Http;
using Plugin.TextToSpeech;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
namespace CameraTest
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
           
            await CrossMedia.Current.Initialize();
            try
            {
                if(!CrossMedia.Current.IsTakePhotoSupported&&!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("INFO", "CAMERA NOT AVAILABEL", "OK");
                }
                else
                {
                    var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                    });
                    if (file==null)
                    {
                        
                        await DisplayAlert("ERROR", "FILE NOT FOUND", "OK");
                        return;
                    }
                    img.Source = ImageSource.FromStream(() => file.GetStream());


                    var visionapi = new ComputerVisionClient(new ApiKeyServiceClientCredentials("775a5c37a1d0432795cde90d542cb988"),new System.Net.Http.DelegatingHandler[] { });
                    await DisplayAlert("", "sent", "ok");
                    visionapi.Endpoint = "https://westcentralus.api.cognitive.microsoft.com";

                    List<VisualFeatureTypes> features = new List<VisualFeatureTypes>()
        {
            VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
            VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
            VisualFeatureTypes.Tags, 
            VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
            VisualFeatureTypes.Objects
        };



                    var desc =   await visionapi.AnalyzeImageInStreamAsync(file.GetStream(),features);


                    
                   
                 
                   
                    foreach(var c in desc.Brands)
                        
                    {
                        cap.Text = cap.Text+"CONFIDENCE %:"+""+ c.Name+Math.Round(c.Confidence * 100)+ "\n";
                      //await  CrossTextToSpeech.Current.Speak(c);
                    }


                   
                    
                        foreach (var c in desc.Description.Captions)

                        {
                            descc.Text = descc.Text + c.Text+ "\n";

                        }
                        foreach(var c in desc.Objects)
                    {
                        objs.Text = objs.Text + c.ObjectProperty +""+ "CONFIDENCE %:" + Math.Round(c.Confidence * 100)+ "%"+ "\n";
                    }
                        foreach(var c in desc.Categories)
                    {
                        catog.Text = catog.Text + c.Name + "\n";
                    }
                    IList<string> lsts = desc.Description.Tags;
                    foreach(var c in lsts)
                    {
                        tg.Text = tg.Text + c + "\n";
                    }

                    foreach(var c in desc.Faces)
                    {
                        fc.Text = fc.Text + c.Gender + "\n";
                    }
                        
                       
                        
                       
                   
                    //   await DisplayAlert("DETAILS", cap.Text, "ok");

                     IFirebaseConfig config = new FirebaseConfig
                      {
                         AuthSecret= "LGSKaFzTOVvqU1jFlgGGVdQkSFcyn7dWms9m8GZA",
                          BasePath= "https://realtimedbsample-e77ab.firebaseio.com/"
                      };
                      if(config!=null)
                      {
                          await DisplayAlert("INFO", "CONNECTED TO FIREBASE", "OK");

                      }
                      IFirebaseClient client = new FireSharp.FirebaseClient(config);
                      var obj = new Class1
                      {
                         
                          description =descc.Text,brand=cap.Text,objects=objs.Text,categories=catog.Text,tags=tg.Text,faces=fc.Text
                         
                      };
                      Random rnd = new Random();
                      string  nodeno = Convert.ToString( rnd.Next(1, 10));
                      SetResponse response = await client.SetAsync("Keywords" + "ADA"+nodeno, obj);
                      Class1 result = response.ResultAs<Class1>();
                      if(result!=null)
                      {
                          await DisplayAlert("INFO", "SENT THE RESULT TO FIREBASE", "OK");
                         await  CrossTextToSpeech.Current.Speak("THE RESULT IS SENT TO FIRE BASE");
                      }










                }
            }
            catch(Exception ex )
            {
                await DisplayAlert("", ex.ToString(), "OK");
            }

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            descc.Text = "";
            cap.Text = "";
            objs.Text = "";
             catog.Text = "";
            tg.Text = "";
            fc.Text = "";
            searchtxt.Text = "";
            searchtxt.BackgroundColor = Color.White;
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            BRclass.url = searchtxt.Text;
          await  Navigation.PushAsync(new BROWSER());
        }

        private void Searchtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchtxt.BackgroundColor = Color.Green;
        }
    }
}
