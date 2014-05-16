# Care Center
The care center project is an example of a functional web application built on top of the [SiteBase](http://github.com/digitalbeacon/sitebase) foundation. The default configuration includes demo logins to allow easy access to the available features of the application. A live demo of the CareCenter project is available at [http://digitalbeacon.net/carecenterdemo](http://digitalbeacon.net/carecenterdemo).

## License
This project is licensed under the [GNU General Public License, version 2](http://www.gnu.org/licenses/gpl-2.0.html) (GPLv2).

## Prerequisites for Building
1. .NET 4.5 Runtime
2. Git
3. (Optional) Visual Studio 2012/2013
4. (Optional) [7-Zip](http://www.7-zip.org/download.html) for creating deployment zip files

To address the build warnings about missing reference assemblies, install Visual Studio 2012/2013 or the [.NET 4.5 SDK](http://msdn.microsoft.com/en-us/windows/desktop/hh852363.aspx), both of which include the reference assemblies for the .NET 4.5 framework.

## Build Instructions
1. Clone repository. `git clone https://github.com/digitalbeacon/carecenter.git`
2. Initialize [SiteBase](http://github.com/digitalbeacon/sitebase) submodule. `git submodule update --init`
3. Create symbolic links. `setup-base.cmd` (This script must be run with administrative privileges.)
4. Build. `build.cmd`
5. Create deployment files. `publish.cmd`

## Prerequisites for Running
1. .NET 4.5 Runtime
2. IIS 7
3. SQL Server 2012 (SQL Server 2005 and up will work with some assembly binding configuration changes to the web.config file.)   
4. (Optional) [wkhtmltopdf](http://wkhtmltopdf.org/downloads.html) for PDF report generation

## Deployment Instructions
1. Create the database. `Database\init-db.cmd` This script creates a new database called *CareCenterDemo*, initializes it with the ASP.NET SQL Membership objects and creates a login called *web* with the appropriate access. If you have trouble running this script with the default integrated security context, try running it with the `/user {user}` flag to specify an explicit SQL user account.
2. Initialize the database. `Database\reset-db.cmd` This script can be called with the `/initpostalcodes` flag to populate the table of postal codes. 
3. Extract or copy the deployment files to a new folder on the web server. If extracting the zip files, all three zip files in the *Publish* folder should be extracted to the same folder.  
4. Add a new IIS web application called *carecenterdemo* pointing to the web site folder configured with a .NET 4/integrated pipeline mode IIS application pool.
5. Access the new web application.

## Optional Things
- **PDF Generation**. Install [wkhtmltopdf](http://wkhtmltopdf.org/downloads.html) and uncomment the *HtmlToPdfExePath* setting in the *Config\appSettings.config* file. Update the setting to the path of the wkhtmltopdf executable.
- **View Precompilation**. Uncomment the *PrecompileViews* setting in the *Config\appSettings.config* file. The views wil be precompiled into a new assembly in the site Bin folder upon initial access. 
- **Recaptcha**. Specify the *recaptchaPrivateKey* and *recaptchaPublicKey* settings in the *Config\appSettings.config* file. Captcha support is currently implemented for password resets.
- **Google Analytics**. Specify the *GoogleAnalyticsId* settings in the *Config\appSettings.config* file.

## Attribution
This project includes or incorporates the following third party projects.

- [AngularJS](https://angularjs.org)
- [AutoMapper](http://automapper.org/)
- [Blade](https://github.com/vannatech/blade)
- [Bootstrap](http://getbootstrap.com)
- [Common.Logging](https://github.com/net-commons/common-logging)
- [excanvas](http://excanvas.sourceforge.net/)
- [FluentValidation](http://fluentvalidation.codeplex.com)
- [jQuery](http://jquery.com)
- [jQuery BeautyTips](http://www.lullabot.com/blog/articles/announcing-beautytips-jquery-tooltip-plugin)
- [jQuery bgiframe](https://github.com/brandonaaron/bgiframe)
- [jQuery hoverIntents](http://cherne.net/brian/resources/jquery.hoverIntent.html)
- [jQuery modalBox](http://code.google.com/p/jquery-modalbox-plugin)
- [jQuery Validation](http://bassistance.de/jquery-plugins/jquery-plugin-validation)
- [JSON.NET](http://james.newtonking.com/json)
- [log4net](http://logging.apache.org/log4net)
- [MarkdownSharp](https://code.google.com/p/markdownsharp)
- [MvcContrib](http://mvccontrib.codeplex.com)
- [MyGeneration](http://sourceforge.net/projects/mygeneration)
- [NHibernate](http://www.nhforge.org)
- [Recaptcha.NET](http://recaptchanet.codeplex.com)
- [SecuritySwitch](https://code.google.com/p/securityswitch)
- [Spark View Engine](https://github.com/SparkViewEngine/spark)
- [Spring.NET](http://springframework.net)
- [Telerik MVC Extensions](http://telerikaspnetmvc.codeplex.com)
- [YUICompressor.NET](http://yuicompressor.codeplex.com)
