using System.Linq.Expressions;
using Windows.UI;
using Microsoft.WindowsAzure.MobileServices;
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
using ProjectHelix.Data;
using SharpDX.DirectWrite;

namespace ProjectHelix
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Shop : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private string _tooltip;

        private enum Rarity
        {
            Common = 0,
            Uncommon,
            Rare,
            Legendary
        };

        private enum Type
        {
            Hull = 0,
            Canon,
            Bullet,
            Sails,
            Wheel,
            Crate
        };

        private Rarity _rarity;
        private Type _type;
        private int _gold;
        private int[,] _parts = { { 1, 1, 1, 1, 1 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
        private string _shipHullR;
        private string _shipCanonR;
        private string _shipBulletsR;
        private string _shipSailsR;
        private string _shipWheelR;

        private string _userId;
        private string _itemName;
        private string _description;
          
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


        public Shop()
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
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            _userId = (String)e.NavigationParameter;
            IMobileServiceTable<User> userTable = App.MobileService.GetTable<User>();
            var userCourant = (await userTable.Where(user => user.Id == _userId).ToListAsync())[0];
            _gold = userCourant.Gold;
            _shipHullR = userCourant.HullR;
            _shipCanonR = userCourant.CanonR;
            _shipBulletsR = userCourant.BulletR;
            _shipSailsR = userCourant.SailsR;
            _shipWheelR = userCourant.WheelR;
            UpdateGold();
            updateInfoEquiped();

            //Intialise l'inventaire
            _parts[1, 0] = userCourant.HullUN;
            _parts[1, 1] = userCourant.CanonUN;
            _parts[1, 2] = userCourant.BulletUN;
            _parts[1, 3] = userCourant.SailsUN;
            _parts[1, 4] = userCourant.WheelUN;
            _parts[2, 0] = userCourant.HullRN;
            _parts[2, 1] = userCourant.CanonRN;
            _parts[2, 2] = userCourant.BulletRN;
            _parts[2, 3] = userCourant.SailsRN;
            _parts[2, 4] = userCourant.WheelRN;
            _parts[3, 0] = userCourant.HullLN;
            _parts[3, 1] = userCourant.CanonLN;
            _parts[3, 2] = userCourant.BulletLN;
            _parts[3, 3] = userCourant.SailsLN;
            _parts[3, 4] = userCourant.WheelLN;
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {

        }

        //#region NavigationHelper registration

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

        private void UpdateInfo(object sender, ItemClickEventArgs e)
        {
            string itemName = "";
            string description = "";
            int buyPrice = 0;
            //update itemInfo NAME
            if (!e.ClickedItem.ToString().EndsWith("Image"))
            {
                _itemName = e.ClickedItem.ToString();

                //update desctiption
                string name = e.ClickedItem.ToString();
                if (name.EndsWith("Hull"))
                {
                    _type = Type.Hull;
                    _description = "The hull is the main part of your ship. Hulls dramaticly increase your durability. Each new hull upgrade you get will increase your Defense stat by 1. Each point in Defense gives you 3 more HP to your ship. Making you a harder target to kill.";
                }
                else if (name.EndsWith("Sails"))
                {
                    _type = Type.Sails;
                    _description = "The sails increases your speed. This means you can travel a lot more distance per turn. Each sail upgrade will increase your travel distance by XX, allowing you to escape mighty opponents, or chase a fleeing coward.";
                }

                else if (name.EndsWith("Cannons"))
                {
                    _type = Type.Canon;
                    _description = "Cannons are essential to win a game of Corsairs. Each new cannon upgrade increase your firerate. Firing more bullets per minute makes your life way easier.";
                }
                else if (name.EndsWith("Bullets"))
                {
                    _type = Type.Bullet;
                    _description = "Good quality bullets for improved piercing! Each new bullet upgrade increase the power of each shot by 1 damage. With good bullets and a good set of cannons, the other corsairs will tremble in fear within sight.";
                }
                else if (name.EndsWith("Wheel"))
                {
                    _type = Type.Wheel;
                    _description = "A better wheel gives more control on the boat, giving you the chance to do sharp manoeuvers. Equip yourself with a good wheel and sails, and you'll fly like the wind.";
                }
                else if (name.EndsWith("Crate"))
                {
                    _type = Type.Crate;
                    _description = "The Mystery loot crate can contain uncommon, rare and even legendary items. What are you going to loot in that chest? Who know’s ! It could be of any type and any rarity. Will you get your money’s worth?";
                }
                //update price
                _tooltip = name;
                //update TextBoxes
                updateInfoEquiped();
                UpdateInfoText();
            }
        }

        private int GetPriceForRarity()
        {
            if (_tooltip == null)
                return 0;
            if (_tooltip.StartsWith("Uncommon"))
            {
                _rarity = Rarity.Uncommon;
                return 100;
            }
            else if (_tooltip.StartsWith("Rare"))
            {
                _rarity = Rarity.Rare;
                return 400;
            }
            else if (_tooltip.StartsWith("Legendary"))
            {
                _rarity = Rarity.Legendary;
                return 1600;
            }
            else if (_tooltip.StartsWith("MYSTERY"))
                return 250;
            else return 0;
        }

        private void OptionButtons(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem.Equals("Equip Item"))
            {
                if (_type == Type.Crate) { }

                else if (_parts[(int) _rarity, (int) _type] > 0)
                {
                    switch (_type)
                    {
                        case Type.Hull:
                            switch (_rarity)
                            {
                                case Rarity.Common:
                                    _shipHullR = "Common";
                                    break;
                                case Rarity.Uncommon:
                                    _shipHullR = "Uncommon";
                                    break;
                                case Rarity.Rare:
                                    _shipHullR = "Rare";
                                    break;
                                case Rarity.Legendary:
                                    _shipHullR = "Legendary";
                                    break;
                            }
                            UpdateShip("Hull");
                            break;
                        case Type.Canon:
                            switch (_rarity)
                            {
                                case Rarity.Common:
                                    _shipCanonR = "Common";
                                    break;
                                case Rarity.Uncommon:
                                    _shipCanonR = "Uncommon";
                                    break;
                                case Rarity.Rare:
                                    _shipCanonR = "Rare";
                                    break;
                                case Rarity.Legendary:
                                    _shipCanonR = "Legendary";
                                    break;
                            }
                            UpdateShip("Canon");
                            break;
                        case Type.Bullet:
                            switch (_rarity)
                            {
                                case Rarity.Common:
                                    _shipBulletsR = "Common";
                                    break;
                                case Rarity.Uncommon:
                                    _shipBulletsR = "Uncommon";
                                    break;
                                case Rarity.Rare:
                                    _shipBulletsR = "Rare";
                                    break;
                                case Rarity.Legendary:
                                    _shipBulletsR = "Legendary";
                                    break;
                            }
                            UpdateShip("Bullet");
                            break;
                        case Type.Sails:
                            switch (_rarity)
                            {
                                case Rarity.Common:
                                    _shipSailsR = "Common";
                                    break;
                                case Rarity.Uncommon:
                                    _shipSailsR = "Uncommon";
                                    break;
                                case Rarity.Rare:
                                    _shipSailsR = "Rare";
                                    break;
                                case Rarity.Legendary:
                                    _shipSailsR = "Legendary";
                                    break;
                            }
                            UpdateShip("Sails");
                            break;
                        case Type.Wheel:
                            switch (_rarity)
                            {
                                case Rarity.Common:
                                    _shipWheelR = "Common";
                                    break;
                                case Rarity.Uncommon:
                                    _shipWheelR = "Uncommon";
                                    break;
                                case Rarity.Rare:
                                    _shipWheelR = "Rare";
                                    break;
                                case Rarity.Legendary:
                                    _shipWheelR = "Legendary";
                                    break;
                            }
                            UpdateShip("Wheel");
                            break;
                    }
                }
                updateInfoEquiped();
            }
            
            else if (e.ClickedItem.Equals("Buy Item"))
            {
                if((_gold - GetPriceForRarity()) > 0)
                    {
                        _gold -= GetPriceForRarity();
                        if (_type == Type.Crate)
                        {   //BUYIN A CRATE MAN
                            
                            var messageDialog = new Windows.UI.Popups.MessageDialog("You just won "+ RollCrate().ToUpper() + ". Enjoy!");

                            messageDialog.ShowAsync();
                        }

                        else
                        {
                            _parts[(int)_rarity, (int)_type]++;
                            
                        }
                        UpdateInfoText();    
                    }
                    
                UpdateGold();
            }

            else if (e.ClickedItem.Equals("Sell Item"))
            {
                //CHECK QUANTITY
                if (_type == Type.Crate) { }
                else if (_parts[(int)_rarity, (int)_type] > 1)
                {
                    _gold += (GetPriceForRarity() / 4);
                    _parts[(int)_rarity, (int)_type]--;
                    UpdateInfoText();
                }

                UpdateGold();
            }
        }

        public string RollCrate()
        {
            Random random = new Random();
            int randRarity = random.Next(0, 100); //0 a 99
            int randItem = random.Next(0,5);//0,1,2,3,4
            int rarity;
            string rarityString = "";
            string part = "";

            //rarity
            if (randRarity < 55){
                rarity = 1;//"Uncommon";
                rarityString = "Uncommon";
            }
            else if (randRarity > 90){
                rarity = 3; //"Legendary";
                rarityString = "Legendary";
            }
            else{ 
                rarity = 2; //"Rare";
                rarityString = "Rare";
            }

            //quantity++
            switch (randItem)
            {
                case 0:
                    part = "Hull";
                    break;
                case 1:
                    part = "Cannons";
                    break;
                case 2:
                    part = "Bullets";
                    break;
                case 3:
                    part = "Sails";
                    break;
                case 4:
                    part = "Wheel";
                    break;
                default:
                    break;
            }

            string item = " " + rarityString + " " + part;
            _parts[rarity, randItem]++;
            
            return item;
        }

        public async void UpdateShip(string part)
        {
            IMobileServiceTable<User> userTable = App.MobileService.GetTable<User>();
            User currentUser = (await userTable.Where(user => user.Id == _userId).ToListAsync())[0];
            switch (part)
            {
                case "Hull":
                    currentUser.HullR = _shipHullR;
                    break;
                case "Canon":
                    currentUser.CanonR = _shipCanonR;
                    break;
                case "Bullet":
                    currentUser.BulletR = _shipBulletsR;
                    break;
                case "Sails":
                    currentUser.SailsR = _shipSailsR;
                    break;
                case "Wheel":
                    currentUser.WheelR = _shipWheelR;
                    break;
            }
            await userTable.UpdateAsync(currentUser);
        }

        public async void UpdateGold()
        {
            textGold.Text = _gold.ToString();
            IMobileServiceTable<User> userTable = App.MobileService.GetTable<User>();
            User currentUser = (await userTable.Where(user => user.Id == _userId).ToListAsync())[0];
            currentUser.Gold = _gold;
            await userTable.UpdateAsync(currentUser);
        }

        public async void updateInfoEquiped()
        {
            itemEquiped.Text = "PARTS CURRENTLY EQUIPPED";
            itemEquiped.Text += "\n\nHULL: " + _shipHullR;
            itemEquiped.Text += "\nSAILS: " + _shipSailsR;
            itemEquiped.Text += "\nCANNONS: " + _shipCanonR;
            itemEquiped.Text += "\nBULLETS: " + _shipBulletsR;
            itemEquiped.Text += "\nWHEEL: " + _shipWheelR;
        }

        public async void updateParts()
        {
            //TODO: Update les parts
            IMobileServiceTable<User> userTable = App.MobileService.GetTable<User>();
            User currentUser = (await userTable.Where(user => user.Id == _userId).ToListAsync())[0];
            switch (_rarity)
            {
                case Rarity.Uncommon:
                    switch (_type)
                    {
                        case Type.Hull:
                            currentUser.HullUN = _parts[(int)_rarity, (int)_type];
                            break;
                        case Type.Canon:
                            currentUser.CanonUN = _parts[(int)_rarity, (int)_type];
                            break;
                        case Type.Bullet:
                            currentUser.BulletUN = _parts[(int)_rarity, (int)_type];
                            break;
                        case Type.Sails:
                            currentUser.SailsUN = _parts[(int)_rarity, (int)_type];
                            break;
                        case Type.Wheel:
                            currentUser.WheelUN = _parts[(int)_rarity, (int)_type];
                            break;
                    }
                    break;
                case Rarity.Rare:
                    switch (_type)
                    {
                        case Type.Hull:
                            currentUser.HullRN = _parts[(int)_rarity, (int)_type];
                            break;
                        case Type.Canon:
                            currentUser.CanonRN = _parts[(int)_rarity, (int)_type];
                            break;
                        case Type.Bullet:
                            currentUser.BulletRN = _parts[(int)_rarity, (int)_type];
                            break;
                        case Type.Sails:
                            currentUser.SailsRN = _parts[(int)_rarity, (int)_type];
                            break;
                        case Type.Wheel:
                            currentUser.WheelRN = _parts[(int)_rarity, (int)_type];
                            break;
                    }
                    break;
                case Rarity.Legendary:
                    switch (_type)
                    {
                        case Type.Hull:
                            currentUser.HullLN = _parts[(int)_rarity, (int)_type];
                            break;
                        case Type.Canon:
                            currentUser.CanonLN = _parts[(int)_rarity, (int)_type];
                            break;
                        case Type.Bullet:
                            currentUser.BulletLN = _parts[(int)_rarity, (int)_type];
                            break;
                        case Type.Sails:
                            currentUser.SailsLN = _parts[(int)_rarity, (int)_type];
                            break;
                        case Type.Wheel:
                            currentUser.WheelLN = _parts[(int)_rarity, (int)_type];
                            break;
                    }
                    break;
            }
            await userTable.UpdateAsync(currentUser);
        }

        public async void UpdateInfoText()
        {
            int buyPrice = GetPriceForRarity();
            itemInfo.Text = "ITEM: " + _itemName;
            itemInfo.Text += "\n\nBUY PRICE: " + buyPrice;
            if (_type == Type.Crate) {
                itemInfo.Text += "\n";
                itemInfo.Text += "\n\n";
            }
            else{
                itemInfo.Text += "\nSELL PRICE: " + buyPrice/4;
                itemInfo.Text += "\n\nQUANTITY: " + _parts[(int) _rarity, (int) _type]; //get database quantity
            }
            itemInfo.Text += "\n\nDESCRIPTION: " + _description;

            updateParts();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        //#endregion

    }
}
