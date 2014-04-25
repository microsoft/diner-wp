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

using System.Device.Location;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

#if WP8
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using System.Collections.ObjectModel;
using System.Reflection;
#else
using Microsoft.Phone.Controls.Maps;
#endif

namespace wprestaurantapp
{
    /// <summary>
    /// Page for showing map of the surroundings of the restaurant
    /// </summary>
    public partial class Map : PhoneApplicationPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Map()
        {
            InitializeComponent();

            // Make sure that data context is loaded
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }

            // Set the data context
            DataContext = App.ViewModel;

            // Set up map view & map pushpin
#if WP8
            MapView = new Microsoft.Phone.Maps.Controls.Map();
            MapPushpin = new Microsoft.Phone.Maps.Toolkit.Pushpin();
            MapPushpin.Name = "MapPushpin";
            MapPushpin.GeoCoordinate = App.ViewModel.Restaurant.Location;

            ObservableCollection<DependencyObject> children = MapExtensions.GetChildren(MapView);
            children.Add(MapPushpin);
            MapExtensionsSetup(MapView);
#else
            MapView = new Microsoft.Phone.Controls.Maps.Map();
            MapView.ZoomBarVisibility = Visibility.Visible;
            MapPushpin = new Microsoft.Phone.Controls.Maps.Pushpin();
            MapPushpin.Location = App.ViewModel.Restaurant.Location;
            MapView.Children.Add(MapPushpin);
#endif
            MapView.ZoomLevel = 12;
            MapView.Center = App.ViewModel.Restaurant.Location;
            ContentPanel.Children.Add(MapView);
        }

#if WP8
        /// <summary>
        /// Setup the map extensions objects.
        /// All named objects inside the map extensions will have its references properly set
        /// </summary>
        /// <param name="map">The map that uses the map extensions</param>
        private void MapExtensionsSetup(Microsoft.Phone.Maps.Controls.Map map)
        {
            ObservableCollection<DependencyObject> children = MapExtensions.GetChildren(map);
            var runtimeFields = this.GetType().GetRuntimeFields();

            foreach (DependencyObject i in children)
            {
                var info = i.GetType().GetProperty("Name");

                if (info != null)
                {
                    string name = (string)info.GetValue(i);

                    if (name != null)
                    {
                        foreach (FieldInfo j in runtimeFields)
                        {
                            if (j.Name == name)
                            {
                                j.SetValue(this, i);
                                break;
                            }
                        }
                    }
                }
            }
        }

#endif

        /// <summary>
        /// Overridden OnNavigatedTo handler
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if( isNewInstance )
            {
                double zoomLevel = 12;
                if (State.ContainsKey("ZoomLevel"))
                {
                   zoomLevel = (double)State["ZoomLevel"];
                }

                if (State.ContainsKey("Center"))
                {
                    MapView.SetView(State["Center"] as GeoCoordinate, zoomLevel);
                }

#if WP8
                if (State.ContainsKey("PushPin"))
                {
                    MapPushpin.GeoCoordinate = State["PushPin"] as GeoCoordinate;
                }
#else
                if (State.ContainsKey("PushPin"))
                {
                    MapPushpin.Location = State["PushPin"] as GeoCoordinate;
                }
#endif

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
                State["Center"] = MapView.Center;
                State["ZoomLevel"] = MapView.ZoomLevel;
#if WP8
                State["PushPin"] = MapPushpin.GeoCoordinate;
#else
                State["PushPin"] = MapPushpin.Location;
#endif
            }
        }

        /// <summary>
        /// Map controls
        /// </summary>
#if WP8
        private Microsoft.Phone.Maps.Controls.Map MapView = null;
        private Microsoft.Phone.Maps.Toolkit.Pushpin MapPushpin = null;
#else
        private Microsoft.Phone.Controls.Maps.Map MapView = null;
        private Microsoft.Phone.Controls.Maps.Pushpin MapPushpin = null;
#endif

        /// <summary>
        /// True when this object instance has been just created, otherwise false
        /// </summary>
        private bool isNewInstance = true;
    }
}