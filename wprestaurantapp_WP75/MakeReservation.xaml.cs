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
using System.Linq;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace wprestaurantapp
{
    /// <summary>
    /// Page for creating or modifying a reservation
    /// </summary>
    public partial class MakeReservation : PhoneApplicationPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MakeReservation()
        {
            InitializeComponent();

            // Set the data context
            DataContext = App.ViewModel;

            // Application bar is not a silverlight component -> it doesn't support data binding, which means
            // that it cannot be localized with XAML. Therefore, create it using C#
            ApplicationBar = new ApplicationBar();
            ApplicationBar.IsVisible = true;
            ApplicationBar.IsMenuEnabled = true;
            _button1 = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Check.png", UriKind.Relative));
            _button1.Text = wprestaurantapp.AppResources.ButtonDone;
            _button1.IsEnabled = false;
            _button1.Click += new EventHandler(button1_click);

            ApplicationBarIconButton button2 = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Cancel.png", UriKind.Relative));
            button2.Text = wprestaurantapp.AppResources.ButtonCancel;
            button2.IsEnabled = true;
            button2.Click += new EventHandler(button2_click);

            ApplicationBar.Buttons.Add(_button1);
            ApplicationBar.Buttons.Add(button2);
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
                _reservationIndex = int.Parse(NavigationContext.QueryString["index"]);

                if (_reservationIndex != -1)
                {
                    Reservation reservation = App.ViewModel.Restaurant.Reservations.ElementAt(_reservationIndex);
                    NameInput.Text = reservation.Reserver;
                    TelephoneInput.Text = reservation.Telephone;
                    PersonsInput.Text = reservation.Persons.ToString();
                    DatePicker.Value = new DateTime(reservation.Time.Year, reservation.Time.Month, reservation.Time.Day,
                                                    reservation.Time.Hour, reservation.Time.Minute, reservation.Time.Second);
                    TimePicker.Value = new DateTime(reservation.Time.Year, reservation.Time.Month, reservation.Time.Day,
                                                    reservation.Time.Hour, reservation.Time.Minute, reservation.Time.Second);
                }

                // Restore transient page state in activation
                if (State.ContainsKey("Reserver"))
                {
                    NameInput.Text = (string)State["Reserver"];
                }

                if (State.ContainsKey("Telephone"))
                {
                    TelephoneInput.Text = (string)State["Telephone"];
                }

                if (State.ContainsKey("Persons"))
                {
                    PersonsInput.Text = (string)State["Persons"];
                }

                if (State.ContainsKey("DatePicker"))
                {
                    DatePicker.Value = (DateTime?)State["DatePicker"];
                }

                if (State.ContainsKey("TimePicker"))
                {
                    TimePicker.Value = (DateTime?)State["TimePicker"];
                }

                if (State.ContainsKey("ReservationIndex"))
                {
                    _reservationIndex = (int)State["ReservationIndex"];
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
                State["Reserver"] = NameInput.Text;
                State["Telephone"] = TelephoneInput.Text;
                State["Persons"] = PersonsInput.Text;
                State["ReservationIndex"] = _reservationIndex;

                // When navigating to DatePicker/TimePicker OnNavigatedFrom() gets called. We should _not_ be saving the states
                // of DatePicker/TimePicker in that case, because we would end up overwriting the value(s) selected by the user
                // in OnNavigatedTo().

                // DatePicker and TimePicker are part of Silverlight Toolkit which does not ship as part of this
                // example. Please see release notes for instructions how to install and use Silverlight Toolkit.
                string uri = e.Uri.ToString();
                if (uri != "/Microsoft.Phone.Controls.Toolkit;component/DateTimePickers/DatePickerPage.xaml" &&
                    uri != "/Microsoft.Phone.Controls.Toolkit;component/DateTimePickers/TimePickerPage.xaml")
                {
                    State["DatePicker"] = DatePicker.Value;
                    State["TimePicker"] = TimePicker.Value;
                }
                else
                {
                    State.Remove("DatePicker");
                    State.Remove("TimePicker");
                }

            }

        }

        /// <summary>
        /// Handler to validate entered user input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validateFields(object sender, TextChangedEventArgs e)
        {
            bool name = ( NameInput.Text != "" );
            bool telephone = ( TelephoneInput.Text != "" );
            int temp = 0;
            bool persons = ( int.TryParse( PersonsInput.Text, out temp ) ) && ( temp > 0 );

            if( name && telephone && persons )
            {
                _button1.IsEnabled = true;
            }
            else
            {
                _button1.IsEnabled = false;
            }
        }

        /// <summary>
        /// Handler for Done button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_click(object sender, EventArgs e)
        {

            Reservation reservation = new Reservation();
            reservation.Reserver = NameInput.Text;
            reservation.Telephone = TelephoneInput.Text;
            reservation.Persons = int.Parse(PersonsInput.Text);
            DateTime date = DatePicker.Value ?? DateTime.Now;
            DateTime time = TimePicker.Value ?? DateTime.Now;
            reservation.Time = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);

            if (_reservationIndex == -1)
            {
                App.ViewModel.Restaurant.Reservations.Add(reservation);
            }
            else
            {
                App.ViewModel.Restaurant.Reservations.RemoveAt(_reservationIndex);
                App.ViewModel.Restaurant.Reservations.Insert(_reservationIndex, reservation);
            }
            NavigationService.GoBack();
        }

        /// <summary>
        /// Handler for Cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        /// <summary>
        /// True when this object instance has been just created, otherwise false
        /// </summary>
        private bool isNewInstance = true;

        /// <summary>
        /// Member variable for Done button
        /// </summary>
        private ApplicationBarIconButton _button1;

        /// <summary>
        /// Member variable storing index of the reservation. -1 if creating a new reservation, otherwise
        /// other than -1
        /// </summary>
        private int _reservationIndex;

    }

}