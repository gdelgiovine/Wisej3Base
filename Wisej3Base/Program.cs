using System;
using Wisej.Web;

namespace Wisej3Base
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Application.Desktop = new MyDesktop();

            //Model_Titles.RegisterDapperTypeMap();
            
            frmADONET frmADONET  = new frmADONET();
            frmDapper frmDapper = new frmDapper();
            frmPasseroFramework frmPasseroFramework = new frmPasseroFramework();
            frmADONET .Show();
            frmDapper .Show();  
            frmPasseroFramework .Show();    
        }

        //
        // You can use the entry method below
        // to receive the parameters from the URL in the args collection.
        //
        //static void Main(NameValueCollection args)
        //{
        //}
    }
}