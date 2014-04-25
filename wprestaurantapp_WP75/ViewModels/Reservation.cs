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
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Data;

namespace wprestaurantapp
{

    // ReservationPersonConverter and ReservationDateConverter are classes containing
    // small amount of code and are related to Reservation class. For this reason they
    // are declared in the same file as Reservation class.

    /// <summary>
    /// Conversion class between number of persons to a string, taking account
    /// possible localization
    /// </summary>
    public class ReservationPersonConverter : IValueConverter
    {
        /// <summary>
        /// Converter from persons to strings
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            int persons = (int)value;
            if (persons > 1)
            {
                string str = wprestaurantapp.AppResources.ReservationForMultiple;
                return String.Format(str, persons);
            }
            else
            {
                return wprestaurantapp.AppResources.ReservationForSingle;
            }
        }

        /// <summary>
        /// Conversion back from string to persons. Needed only for two-way binding
        /// which is not used, so no need to implement this
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Conversion class between reservation date to string
    /// </summary>
    public class ReservationDateConverter : IValueConverter
    {
        /// <summary>
        /// Converter from reservation date to string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return date.ToString( "g");
        }

        /// <summary>
        /// Conversion back from string to reservation date. Needed only for two-way binding
        /// which is not used, so no need to implement this
        /// </summary>
        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Model class for a restaurant reservation
    /// </summary>
    [DataContract]
    public class Reservation : INotifyPropertyChanged
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public Reservation()
        {
        }

        /// <summary>
        /// Member variable for Reserver property
        /// </summary>
        private string _reserver;

        /// <summary>
        /// Property for the name of the reserver
        /// </summary>
        [DataMember]
        public string Reserver
        {
            get
            {
                return _reserver;
            }
            set
            {
                if (_reserver != value)
                {
                    _reserver = value;
                    NotifyPropertyChanged("Reserver");
                }
            }
        }

        /// <summary>
        /// Member variable for Telephone property
        /// </summary>
        private string _telephone;

        /// <summary>
        /// Property for the telephone of reserver
        /// </summary>
        [DataMember]
        public string Telephone
        {
            get
            {
                return _telephone;
            }
            set
            {
                if (_telephone != value)
                {
                    _telephone = value;
                    NotifyPropertyChanged("Telephone");
                }
            }
        }

        /// <summary>
        /// Member variable for Persons property
        /// </summary>
        private int _persons;

        /// <summary>
        /// Property describing for how many persons the reservation is for
        /// </summary>
        [DataMember]
        public int Persons
        {
            get
            {
                return _persons;
            }
            set
            {
                if (_persons != value)
                {
                    _persons = value;
                    NotifyPropertyChanged("Persons");
                }
            }
        }

        /// <summary>
        /// Member variable for Time property
        /// </summary>
        private DateTime _time;

        /// <summary>
        /// Property for starting time of the reservation
        /// </summary>
        [DataMember]
        public DateTime Time
        {
            get
            {
                if( _time == null)
                {
                    _time = new DateTime();
                }
                return _time;
            }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    NotifyPropertyChanged("Time");
                }
            }
        }

        /// <summary>
        /// Implementation of PropertyChanged event of INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Helper method for emitting PropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of the property that has changed</param>
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

}
