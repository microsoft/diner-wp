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
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Resources;
using System.Xml.Linq;

namespace wprestaurantapp
{
    /// <summary>
    /// Main model class of the application
    /// </summary>
    public class MainViewModel
    {
        /// <summary>
        ///  Constructor
        /// </summary>
        public MainViewModel()
        {
            this.Restaurant = new RestaurantData();
        }

        /// <summary>
        /// Property for restaurant data
        /// </summary>
        public RestaurantData Restaurant { get; private set; }

        /// <summary>
        /// Property is true if application data has been loaded, otherwise false
        /// </summary>
        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Method for loading application data
        /// </summary>
        public void LoadData()
        {
            try
            {
                // Load constant restaurant data from XML file
                LoadXML();
            }
            catch (Exception /*e*/)
            {
                MessageBox.Show( wprestaurantapp.AppResources.ErrorRestaurantData );
            }

            // Load non-constant reservation data from persistent storage
            LoadReservations();

            this.IsDataLoaded = true;
        }

        /// <summary>
        /// Method for saving application data
        /// </summary>
        public void SaveData()
        {
            // Save reservation data to persistent storage
            SaveReservations();
        }

        /// <summary>
        /// Method for loading restaurant description data from XML file
        /// </summary>
        private void LoadXML()
        {
            Uri uri = new Uri("/wprestaurantapp;component/content/restaurant.xml", UriKind.Relative);
            StreamResourceInfo xml = App.GetResourceStream(uri);
            XDocument doc = XDocument.Load(xml.Stream);

            XElement info = doc.Descendants("info").First();

            Restaurant.Name = info.Element("name").Value;
            Restaurant.LogoURI = info.Element("logo").Value;

            XElement address = info.Element("address");
            Restaurant.StreetAddress = address.Element("street").Value;
            Restaurant.City = address.Element("city").Value;
            Restaurant.Country = address.Element("country").Value;

            XElement coordinates = address.Element("coordinates");

            // Make sure that latitude&longitude values are parsed with US locale. This is because some locales
            // use '.' as decimal point, and some use ','. XML file is formatted according to US locale which uses
            // '.', so we must explicitly set it, otherwise parsing would fail in some locales.
            IFormatProvider format = new CultureInfo("en-US");
            Restaurant.Location.Latitude = double.Parse(coordinates.Element("latitude").Value, format);
            Restaurant.Location.Longitude = double.Parse(coordinates.Element("longitude").Value, format);

            Restaurant.Telephone = info.Element("telephone").Value;
            Restaurant.URL = info.Element("url").Value;
            Restaurant.Description = info.Element("description").Value;

            XElement menu = doc.Descendants("menu").First();
            Restaurant.Categories = (from category in menu.Descendants("category")
                                     select new Category()
                                     {
                                         Id = category.Attribute("id").Value,
                                         Name = category.Attribute("name").Value,
                                         IconURI = category.Attribute("icon").Value,
                                         Dishes = (from dish in category.Descendants("dish")
                                                   select new Dish()
                                                   {
                                                       Name = dish.Attribute("name").Value,
                                                       Text = dish.Value
                                                   }).ToList<Dish>()
                                     }).ToList<Category>();
        }

        /// <summary>
        /// Method for loading reservations from isolated storage
        /// </summary>
        private void LoadReservations()
        {
            try
            {
                using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("reservations.dat", System.IO.FileMode.OpenOrCreate, file))
                    {
                        if (stream.Length > 0)
                        {
                            DataContractSerializer serializer = new DataContractSerializer(typeof(ObservableCollection<Reservation>));
                            Restaurant.Reservations = serializer.ReadObject(stream) as ObservableCollection<Reservation>;
                        }
                    }
                }
            }
            catch (IsolatedStorageException e)
            {
                Console.WriteLine( "Exception occurred while trying to load reservations from isolated storage: " + e.Message);
            }
        }

        /// <summary>
        /// Method for saving reservations to isolated storage
        /// </summary>
        private void SaveReservations()
        {
            try
            {
                using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("reservations.dat", System.IO.FileMode.Create, file))
                    {
                        DataContractSerializer serializer = new DataContractSerializer(typeof(ObservableCollection<Reservation>));
                        serializer.WriteObject(stream, Restaurant.Reservations);
                    }
                }
            }
            catch (IsolatedStorageException e)
            {
                Console.WriteLine( "Exception occurred while trying to save reservations to isolated storage: " + e.Message);
            }
        }

    }
}