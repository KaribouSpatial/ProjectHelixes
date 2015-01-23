using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MonoGame.Framework;
using System;
using ProjectHelix.Common;
using ProjectHelix.Data;

namespace ProjectHelix
{
    /// <summary>
    /// The root page used to display the game.
    /// </summary>
    public sealed partial class GamePage
    {
        public static string LaunchArguments { get; set;}

        private ProjectHelix _game;

        public static User LocalUser { get; set; }
        public static List<User> UsersInGame { get; set; }
        public static string GameId { get; set; }

        public GamePage()
        {
            InitializeComponent();

            // Create the game.
            _game = XamlGame<ProjectHelix>.Create(LaunchArguments, Window.Current.CoreWindow, this);
             
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.Current.Content = new MainMenu(true);
        }

    }
}
