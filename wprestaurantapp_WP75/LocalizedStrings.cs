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

namespace wprestaurantapp
{
    /// <summary>
    /// Helper class for accessing application resources from XAML, especially for localization
    /// </summary>
    public class LocalizedStrings
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LocalizedStrings()
        {
        }

        /// <summary>
        /// Class variable for instance of AppResources
        /// </summary>
        private static wprestaurantapp.AppResources localizedResources = new wprestaurantapp.AppResources();

        /// <summary>
        /// Property for accessing AppResources
        /// </summary>
        public wprestaurantapp.AppResources LocalizedResources { get { return localizedResources; } }

    }
}
