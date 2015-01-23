using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using Microsoft.WindowsAzure.MobileServices;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ProjectHelix.Common;
using ProjectHelix.Core;
using ProjectHelix.Data;
using SharpDX.XAudio2;

namespace ProjectHelix
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainMenu : Page
    {

        private MobileServiceUser _user;
        private int _gold;
        private async System.Threading.Tasks.Task AuthenticateAsync()
        {
            String name = "";

            try
            {
                _user = await App.MobileService
                    .LoginAsync(MobileServiceAuthenticationProvider.Facebook);

                //Shit pour recuperer le nom
                var userId = App.MobileService.CurrentUser.UserId;
                var facebookId = userId.Substring(userId.IndexOf(':') + 1);
                var client = new HttpClient();
                var fbUser = await client.GetAsync("https://graph.facebook.com/" + facebookId);
                var response = await fbUser.Content.ReadAsStringAsync();
                var jo = JsonObject.Parse(response);
                name = jo["name"].GetString();
            }
            catch (InvalidOperationException)
            {
                //TODO:: Ici tu dois enable si il reussit a se connecter
                
                while (true) ;
            }

            //Enregistre le user si c'est la premiere fois
            MobileServiceCollection<User, User> items;
            IMobileServiceTable<User> userTable = App.MobileService.GetTable<User>();


            //On doit enregistrer le joueur
            var users = await userTable.Where(bdUser => bdUser.Id == _user.UserId).ToCollectionAsync();
            if (users.Count == 0)
            {
                await userTable.InsertAsync(new User(_user.UserId, 0, name));
            }
            
            users = await userTable.Where(bdUser => bdUser.Id == _user.UserId).ToCollectionAsync();
            if (users.Count != 1)
                while (true) ; //TODO:: dat bad

            //itemPlayButton.IsEnabled = true;
            itemHostButton.IsEnabled = true;
            itemJoinButton.IsEnabled = true;
            itemShopButton.IsEnabled = true;
        }

        private static MainMenu _instance;
        private NavigationHelper navigationHelper;


        public MainMenu()
        {
            InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            //itemPlayButton.IsEnabled = false;
            itemHostButton.IsEnabled = false;
            itemJoinButton.IsEnabled = false;
            itemShopButton.IsEnabled = false;
        }

        public MainMenu(bool retour)
        {
            InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            //itemPlayButton.IsEnabled = retour;
            itemHostButton.IsEnabled = retour;
            itemJoinButton.IsEnabled = retour;
            itemShopButton.IsEnabled = retour;

            Window.Current.Activate();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Pour se connecter
            if(_user == null)
                await AuthenticateAsync();

            navigationHelper.OnNavigatedTo(e);
            
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        private void LaunchGame(object sender, RoutedEventArgs e)
        {


            GamePage.UsersInGame = new List<User>();
            User dummy = new User("connard", 0, "Yolo");
            User dummy2 = new User("connard2", 0, "Wololo");

            GamePage.UsersInGame.Add(dummy);
            GamePage.UsersInGame.Add(dummy2);
            GamePage.LocalUser = dummy;
            GamePage.GameId = "01010101";

            IMobileServiceTable<User> userTable = App.MobileService.GetTable<User>();

            Window.Current.Content = new GamePage();
            Window.Current.Activate();
        }

        internal class JoinLobbyContainer
        {
            public bool IsHost;
            public String UserId;
        }

        private void OpenGameLobbyHost(bool hostOrNot)
        {
            
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(GameLobby), new JoinLobbyContainer() {UserId = _user.UserId, IsHost =  hostOrNot});
            }
        }

        private void OpenShopPage()
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Shop), _user.UserId);
            }
        }

        private void gridButtons_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem.Equals("PLAY"))
                LaunchGame(sender, e);
            else if (e.ClickedItem.Equals("HOST"))
                OpenGameLobbyHost(true);
            else if (e.ClickedItem.Equals("JOIN"))
                OpenGameLobbyHost(false);
                //CHANGE THE IP STUFF NIGGA
            else if(e.ClickedItem.Equals("SHOP"))
                OpenShopPage();
        }

    }
}
