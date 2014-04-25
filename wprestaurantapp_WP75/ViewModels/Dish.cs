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

namespace wprestaurantapp
{
    /// <summary>
    /// Model class for a dish. Categories contains one or more dishes
    /// </summary>
    public class Dish
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public Dish()
        {
        }

        /// <summary>
        /// Property for name of this dish
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Property for the description of this dish
        /// </summary>
        public string Text { get; set; }

    }
}
