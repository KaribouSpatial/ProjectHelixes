using Windows.System.Threading;
using Windows.UI;
using Windows.UI.Core;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.Xna.Framework;
using ProjectHelix.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237
using ProjectHelix.Core;
using ProjectHelix.Data;
using SharpDX.Collections;
using System.ComponentModel;

namespace ProjectHelix
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class GameLobby : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        private List<User> _usersInLobby = new List<User>();

        private bool _isHost;
        private bool _isAlreadyStarted = false;
        private String _gameId;
        private String _userId;

        private ThreadPoolTimer _periodicTimer;

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public GameLobby()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;

        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            _isHost = ((MainMenu.JoinLobbyContainer) e.NavigationParameter).IsHost;
            _userId = ((MainMenu.JoinLobbyContainer)e.NavigationParameter).UserId;

            //listPlayers.ItemsSource = _usersInLobby;

            if (_isHost)
                CreateGame(_userId);
            else
                JoinLobby(_userId);

            TimeSpan period = TimeSpan.FromSeconds(10);

            _periodicTimer = ThreadPoolTimer.CreatePeriodicTimer(async (source) =>
            {
                IMobileServiceTable<GameData> gamesTable = App.MobileService.GetTable<GameData>();
                IMobileServiceTable<User> usersTable = App.MobileService.GetTable<User>();

                GameData currentGame = (await gamesTable.Where(game => game.Id == _gameId).ToListAsync())[0];
                List<User> usersInLobby = await usersTable.Where(user => user.Id == currentGame.UserId1 ||
                                                                        user.Id == currentGame.UserId2 ||
                                                                        user.Id == currentGame.UserId3 ||
                                                                        user.Id == currentGame.UserId4 ||
                                                                        user.Id == currentGame.UserId5 ||
                                                                        user.Id == currentGame.UserId6 ||
                                                                        user.Id == currentGame.UserId7 ||
                                                                        user.Id == currentGame.UserId8).ToListAsync();
                // 
                // Update the UI thread by using the UI core dispatcher.
                // 
                Dispatcher.RunAsync(CoreDispatcherPriority.High,
                    () =>
                    {
                        _usersInLobby.Clear();
                        listPlayers.Items.Clear();
                        _usersInLobby = usersInLobby;
                        foreach (var item in _usersInLobby)
                        {
                            //testbox.Text += item.Name;
                            listPlayers.Items.Add(new GridViewItem { Background = new SolidColorBrush(Colors.DeepSkyBlue), Content = item.Name, Width = 1000, Height = 100 });
           
                        }

                        startButton.IsEnabled = (_isHost && usersInLobby.Count >= 2);

                        if(!_isHost)
                            _isHost = (_usersInLobby.Count == 1);
                    });

                if (!_isAlreadyStarted && currentGame.Started)
                {
                    _isAlreadyStarted = true;

                    Dispatcher.RunAsync(CoreDispatcherPriority.High, StartGame);

                    return;
                }

            }, period);

            
        }

        public async void CreateGame(String hostId)
        {
            IMobileServiceTable<GameData> gamesTable = App.MobileService.GetTable<GameData>();

            //On doit enregistrer la partie
            await gamesTable.InsertAsync(new GameData(hostId, App.Version));

            List<GameData> gameToJoin = await gamesTable.Where(game => game.Started == false && hostId == game.UserId1).ToListAsync();
            if (gameToJoin.Count == 0)
            {
                //TODO:: CRY!!!!!
            }
            else
            {
                _gameId = gameToJoin[0].Id;
            }
        }

        public async void JoinLobby(String userId)
        {
            IMobileServiceTable<GameData> gamesTable = App.MobileService.GetTable<GameData>();

            List<GameData> gamesAvailable = await gamesTable.Where(game => game.Started == false &&
                                     (game.UserId1 == null || game.UserId2 == null || game.UserId3 == null ||
                                      game.UserId4 == null || game.UserId5 == null || game.UserId6 == null ||
                                      game.UserId7 == null || game.UserId8 == null)).ToListAsync();

            if (gamesAvailable.Count == 0)
            {
                //TODO:: ALEX AMUSE TOI
                this.Frame.Navigate(typeof(MainMenu));
            }
            else
            {
                GameData gameToJoin = gamesAvailable[0];
                _gameId = gameToJoin.Id;

                if (gameToJoin.UserId1 == null)
                    gameToJoin.UserId1 = userId;
                else if (gameToJoin.UserId2 == null)
                    gameToJoin.UserId2 = userId;
                else if (gameToJoin.UserId3 == null)
                    gameToJoin.UserId3 = userId;
                else if (gameToJoin.UserId4 == null)
                    gameToJoin.UserId4 = userId;
                else if (gameToJoin.UserId5 == null)
                    gameToJoin.UserId5 = userId;
                else if (gameToJoin.UserId6 == null)
                    gameToJoin.UserId6 = userId;
                else if (gameToJoin.UserId7 == null)
                    gameToJoin.UserId7 = userId;
                else
                    gameToJoin.UserId8 = userId;

                await gamesTable.UpdateAsync(gameToJoin);
            }
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private async void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            if(_periodicTimer != null)
                _periodicTimer.Cancel();

            IMobileServiceTable<GameData> gamesTable = App.MobileService.GetTable<GameData>();

            var lel = (await gamesTable.Where(game => game.Id == _gameId).ToListAsync());
            
            if (lel.Count() == 0)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("Sorry no lobby are open at this point. Retry later or try hosting one.");
                messageDialog.ShowAsync();
                return;
            }
            GameData gameToLeave = lel[0];

            if (!gameToLeave.Started)
            {
                if (gameToLeave.UserId1 == _userId)
                    gameToLeave.UserId1 = null;
                else if (gameToLeave.UserId2 == _userId)
                    gameToLeave.UserId2 = null;
                else if (gameToLeave.UserId3 == _userId)
                    gameToLeave.UserId3 = null;
                else if (gameToLeave.UserId4 == _userId)
                    gameToLeave.UserId4 = null;
                else if (gameToLeave.UserId5 == _userId)
                    gameToLeave.UserId5 = null;
                else if (gameToLeave.UserId6 == _userId)
                    gameToLeave.UserId6 = null;
                else if (gameToLeave.UserId7 == _userId)
                    gameToLeave.UserId7 = null;
                else if (gameToLeave.UserId8 == _userId)
                    gameToLeave.UserId8 = null;

                await gamesTable.UpdateAsync(gameToLeave);
            }
            
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
            
        }

        #endregion

        private async void NotifyStartGame(object sender, RoutedEventArgs e)
        {
            IMobileServiceTable<GameData> gamesTable = App.MobileService.GetTable<GameData>();

            GameData gameToPlay = (await gamesTable.Where(game => game.Id == _gameId).ToListAsync())[0];

            if (!gameToPlay.Started)
            {
                gameToPlay.Started = true;

                int nbJoueurs = 0;
                if (gameToPlay.UserId1 != null)
                    ++nbJoueurs;
                if (gameToPlay.UserId2 != null)
                    ++nbJoueurs;
                if (gameToPlay.UserId3 != null)
                    ++nbJoueurs;
                if (gameToPlay.UserId4 != null)
                    ++nbJoueurs;
                if (gameToPlay.UserId5 != null)
                    ++nbJoueurs;
                if (gameToPlay.UserId6 != null)
                    ++nbJoueurs;
                if (gameToPlay.UserId7 != null)
                    ++nbJoueurs;
                if (gameToPlay.UserId8 != null)
                    ++nbJoueurs;
                gameToPlay.NbPlayers = nbJoueurs;

                await gamesTable.UpdateAsync(gameToPlay);
            }
            
        }

        private void StartGame()
        {
            GamePage.LocalUser = _usersInLobby.Find(user => user.Id == _userId);
            GamePage.UsersInGame = _usersInLobby;
            GamePage.GameId = _gameId;

            Window.Current.Content = new GamePage();
            Window.Current.Activate();
        }
}

}
