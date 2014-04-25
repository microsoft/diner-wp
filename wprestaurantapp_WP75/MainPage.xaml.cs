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

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace wprestaurantapp
{
    /// <summary>
    /// Main panorama page of the application
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MainPage()
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
                // Restore transient page state
                if (State.ContainsKey("MainPanoramaIndex"))
                {
                    MainPanorama.DefaultItem = MainPanorama.Items[(int)State["MainPanoramaIndex"]];
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
                State["MainPanoramaIndex"] = MainPanorama.SelectedIndex;
            }
        }

        /// <summary>
        /// Handler invoked when user taps one of the restaurant menu buttons/icons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Category category = button.DataContext as Category;
            int index = App.ViewModel.Restaurant.Categories.IndexOf(category);
            NavigationService.Navigate(new Uri("/Menu.xaml?id=" + index.ToString(), UriKind.Relative));
        }

        /// <summary>
        /// Handler invoked when user taps the phone number of the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Phonenumber_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Phone.Tasks.PhoneCallTask phonecall = new Microsoft.Phone.Tasks.PhoneCallTask();
            phonecall.PhoneNumber = App.ViewModel.Restaurant.Telephone;
            phonecall.Show();
        }

        /// <summary>
        /// Handler invoked when user taps the "Add reservation" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddReservation_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MakeReservation.xaml?index=-1", UriKind.Relative));
        }

        /// <summary>
        /// Handler invoked when user taps a reservation shown in the reservation list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            Reservation reservation = (sender as StackPanel).DataContext as Reservation;
            int index = App.ViewModel.Restaurant.Reservations.IndexOf(reservation);
            NavigationService.Navigate(new Uri("/MakeReservation.xaml?index=" + index.ToString(), UriKind.Relative));
        }

        /// <summary>
        /// Handler invoked when user taps "Cancel" from the reservation context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reservation_Context(object sender, RoutedEventArgs e)
        {
            Reservation reservation = (sender as MenuItem).DataContext as Reservation;
            App.ViewModel.Restaurant.Reservations.Remove(reservation);
        }

        /// <summary>
        /// Handler invoked when user taps the map icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Map_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Map.xaml", UriKind.Relative));
        }

        /// <summary>
        /// True when this object instance has been just created, otherwise false
        /// </summary>
        private bool isNewInstance = true;
    }
}
