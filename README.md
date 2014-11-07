Diner
=====

This example application demonstrates how to build a simple application based on
panorama control for Windows Phone using Microsoft Silverlight.

Support for Windows Phone 8 was made according to guidelines given in article
http://www.developer.nokia.com/Resources/Library/Lumia/#!co-development-and-porting-guide.html.
The solution consists of two top-level projects sharing the same source code,
one targeting WP 7 and the other targeting WP 8. Bing Maps control is used in
WP 7 project and the new Map control introduced in Windows Phone is used in WP 8
project.

This example application is hosted in GitHub:
https://github.com/Microsoft/diner-wp

Developed with:

 * Microsoft Visual Studio 2010 Express for Windows Phone
 * Microsoft Visual Studio Express for Windows Phone 2012.

Compatible with:

 * Windows Phone 7
 * Windows Phone 8

Tested to work on:

 * Samsung Omnia 7
 * Nokia Lumia 820 
 * Nokia Lumia 920

Instructions
------------

Make sure you have the following installed:

 * Windows 8
 * Windows Phone SDK 8.0
 * Latest NuGet Package Manager (>2.7.1) from https://nuget.org/ to enable NuGet
   Package Restore

To build and run the sample:

 * Open the SLN file
   * File > Open Project, select the file wprestaurantapp.sln
 * Install Windows Phone Toolkit for the project.
   * Right click solution wprestaurantapp in Solution Explorer -> select Manage
     NuGet Packages for Solution
   * Search for 'wptoolkit' and install the 'Windows Phone toolkit' package 
 * Depending on whether you want to run the WP7 or WP8 version of the
   application, select either wprestaurantapp_WP7 or wprestaurantapp_WP8 as a
   StartUp Project.    
 * Select the target, for example 'Emulator WVGA'.
 * Press F5 to build the project and run it on the Windows Phone Emulator.

To deploy the sample on Windows Phone device:
 * See the official documentation for deploying and testing applications on
   Windows Phone devices at
   http://msdn.microsoft.com/en-us/library/windowsphone/develop/ff402565(v=vs.105).aspx


About the implementation
------------------------

Important folders:

| Folder | Description |
| ------ | ----------- |
| / | Contains the project file, the license information and this file (README.md) |
| wprestaurantapp_WP75 | Root folder for the WP7 implementation files. |
| wprestaurantapp_WP8 | Root folder for the WP8 implementation files. |
| wprestaurantapp_WP75/content | Graphic files. |
| wprestaurantapp_WP75/Properties | WP7 Application property files. |
| wprestaurantapp_WP75/SampleData | Sample data file. |
| wprestaurantapp_WP75/ViewModels | ViewModel implementation files. |
| wprestaurantapp_WP8/Properties | WP8 Application property files. |

Important files:

| File | Description |
| ---- | ----------- |
| MainPage.xaml.cs | Class responsible for displaying the panorama view of the application. |
| MainViewModel.cs | Class responsible for loading restaurant data from disc. |
| RestaurantData.cs | Class describing the restaurant properties shown. |


Known issues
------------

No known issues.


License
-------

See the license file delivered with this project.


Version history
---------------

 * 1.5.0 Support for 720p resolution and NuGet package restore.
 * 1.4.0 Using Pushpin from WPToolkit for WP 8.
 * 1.3.0 Support for Windows Phone 8.
 * 1.2.0 Bug fixes and changes based on reviews; published on the Nokia Developer website.
 * 1.1.1 Support for Windows Phone OS version 7.1.
 * 1.1.0 New feature: modifying existing reservations.
 * 1.0.0 First version.