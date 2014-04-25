/**
 * Copyright (c) 2011-2014 Microsoft Mobile. All rights reserved.
 *
 * Nokia, Nokia Connecting People, Nokia Developer, and HERE are trademarks
 * and/or registered trademarks of Nokia Corporation. Other product and company
 * names mentioned herein may be trademarks or trade names of their respective
 * owners.
 *
 * See the license text file delivered with this project for more information.
 */

using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace wprestaurantapp
{
    /// <summary>
    /// Page for showing restaurant menu in a pivot control
    /// </summary>
    public partial class Menu : PhoneApplicationPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Menu()
        {
            InitializeComponent();

            // Set the data context
            DataContext = App.ViewModel;
        }

        /// <summary>
        /// Overridden OnNavigatedTo handler
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Make sure that data context is loaded
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }

            if (isNewInstance)
            {
                MenuPivot.SelectedIndex = int.Parse(NavigationContext.QueryString["id"]);

                // Restore transient page state, of set page index based on 'id' property passed to us
                if (State.ContainsKey("MenuPivotIndex"))
                {
                    MenuPivot.SelectedIndex = (int)State["MenuPivotIndex"];
                }

                isNewInstance = false;
            }

        }

        /// <summary>
        /// Overridden OnNavigatedFrom handler
        /// </summary>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (e.NavigationMode != NavigationMode.Back)
            {
                // Save transient page state when moving away
                State["MenuPivotIndex"] = MenuPivot.SelectedIndex;
            }
        }

        /// <summary>
        /// True when this object instance has been just created, otherwise false
        /// </summary>
        private bool isNewInstance = true;
    }
}