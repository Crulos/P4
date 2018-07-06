using System.Web;
using System.Web.Optimization;

namespace P4
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new ScriptBundle("~/Scripts/all").Include(
                     "~/Scripts/vue.js",
                     "~/Scripts/bootstrap.js",
                      "~/Scripts/resource.js",
                      "~/Scripts/vali.js",
                      "~/Scripts/jquery-3.3.1.js"
                      ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));

        }
    }
}
