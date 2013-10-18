/*
 * Copyright © 2011-2013 Nokia Corporation. All rights reserved.
 * Nokia and Nokia Connecting People are registered trademarks of Nokia Corporation. 
 * Other product and company names mentioned herein may be trademarks
 * or trade names of their respective owners. 
 * See LICENSE.TXT for license information.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace wprestaurantapp
{
    /// <summary>
    /// Model class for a menu category
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Category()
        {
        }

        /// <summary>
        /// Property for category id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Property for category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Property for URI of category icon
        /// </summary>
        public string IconURI { get; set; }

        /// <summary>
        /// Member variable for Dishes property
        /// </summary>
        private List<Dish> _dishes;

        /// <summary>
        /// Property for different dishes of this category
        /// </summary>
        public List<Dish> Dishes
        {
            get
            {
                if (_dishes == null)
                {
                    _dishes = new List<Dish>();
                }
                return _dishes;
            }
            set
            {
                _dishes = value;
            }
        }

    }
}
