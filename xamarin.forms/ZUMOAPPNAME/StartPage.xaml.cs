using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZUMOAPPNAME
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        TodoItemManager Lmanager;
        List<TodoItem> Llog = new List<TodoItem>();
        public StartPage()
        {
            InitializeComponent();
            Lmanager = TodoItemManager.DefaultManager;
        }


        async void Login(object sender, EventArgs e)
        {

            if (entusername.Text == "SKD" && entpassword.Text == "1234")
            {
                ind.IsRunning = true;
                ind.IsVisible = true;
                ind.IsEnabled = true;

                
                //LSup = new List<Supervise>(await Smanager.GetTodoItemsAsync());
                Llog = new List<TodoItem>(await Lmanager.GetTodoItemsAsync());
                await Navigation.PushModalAsync(new TabbedPage1(Llog));
            }

        }
    }
}