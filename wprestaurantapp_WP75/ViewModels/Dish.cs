/*
 * Copyright © 2011-2013 Nokia Corporation. All rights reserved.
 * Nokia and Nokia Connecting People are registered trademarks of Nokia Corporation. 
 * Other product and company names mentioned herein may be trademarks
 * or trade names of their respective owners. 
 * See LICENSE.TXT for license information.
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
