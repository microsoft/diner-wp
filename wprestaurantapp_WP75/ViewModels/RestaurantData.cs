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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Device.Location;

namespace wprestaurantapp
{
    /// <summary>
    /// Model class for restaurant data
    /// </summary>
    public class RestaurantData : INotifyPropertyChanged
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RestaurantData()
        {
        }

        /// <summary>
        /// Member variable for Name property
        /// </summary>
        private string _name;

        /// <summary>
        /// Property for name of the restaurant
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// Member variable for LogoURI property
        /// </summary>
        private string _logoURI;

        /// <summary>
        /// Property for URI to the restaurant logo
        /// </summary>
        public string LogoURI
        {
            get
            {
                return _logoURI;
            }
            set
            {
                if (_logoURI != value)
                {
                    _logoURI = value;
                    NotifyPropertyChanged("LogoURI");
                }
            }
        }

        /// <summary>
        /// Member variable for StreetAddress property
        /// </summary>
        private string _streetAddress;

        /// <summary>
        /// Property for street address of the restaurant
        /// </summary>
        public string StreetAddress
        {
            get
            {
                return _streetAddress;
            }
            set
            {
                if (_streetAddress != value)
                {
                    _streetAddress = value;
                    NotifyPropertyChanged("StreetAddress");
                }
            }
        }

        /// <summary>
        /// Member variable for City property
        /// </summary>
        private string _city;

        /// <summary>
        /// Property for city of the restaurant
        /// </summary>
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    NotifyPropertyChanged("City");
                }
            }
        }

        /// <summary>
        /// Member variable for Country property
        /// </summary>
        private string _country;

        /// <summary>
        /// Property for country of the restaurant
        /// </summary>
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                if (_country != value)
                {
                    _country = value;
                    NotifyPropertyChanged("Country");
                }
            }
        }

        /// <summary>
        /// Member variable for Location property
        /// </summary>
        private GeoCoordinate _location;

        /// <summary>
        /// Property for location of the restaurant
        /// </summary>
        public GeoCoordinate Location
        {
            get
            {
                if (_location == null)
                {
                    // Set a default value other than NaN to location, Silverlight map control
                    // wouldn't like it
                    _location = new GeoCoordinate( 0.0, 0.0 );
                }
                return _location;
            }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    NotifyPropertyChanged("Location");
                }
            }
        }

        /// <summary>
        /// Member variable for Telephone property
        /// </summary>
        private string _telephone;

        /// <summary>
        /// Property for telephone number of the restaurant
        /// </summary>
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
        /// Member variable for URL property
        /// </summary>
        private string _url;

        /// <summary>
        /// Property for the website of the restaurant
        /// </summary>
        public string URL
        {
            get
            {
                return _url;
            }
            set
            {
                if (_url != value)
                {
                    _url = value;
                    NotifyPropertyChanged("URL");
                }
            }
        }

        /// <summary>
        /// Member variable for Description property
        /// </summary>
        private string _description;

        /// <summary>
        /// Property for description text of the restaurant
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        /// <summary>
        /// Member variable for Categories property
        /// </summary>
        private List<Category> _categories;

        /// <summary>
        /// Property for different categories of the restaurant
        /// </summary>
        public List<Category> Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = new List<Category>();
                }
                return _categories;

            }
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    NotifyPropertyChanged("Categories");
                }
            }
        }

        /// <summary>
        /// Member variable for Reservations property
        /// </summary>
        private ObservableCollection<Reservation> _reservations;

        /// <summary>
        /// Property for existing reservations of the restaurant
        /// </summary>
        public ObservableCollection<Reservation> Reservations
        {
            get
            {
                if (_reservations == null)
                {
                    _reservations = new ObservableCollection<Reservation>();
                }
                return _reservations;
            }
            set
            {
                if (_reservations != value)
                {
                    _reservations = value;
                    NotifyPropertyChanged("Reservations");
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
