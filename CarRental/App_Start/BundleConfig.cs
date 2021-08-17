using System.Web;
using System.Web.Optimization;

namespace CarRental
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryAndJqueryUI").Include(
                        "~/Scripts/jquery-{version}.js"
                       , "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Site.css",
                      "~/Content/themes/base/core.css",
                      "~/Content/themes/base/autocomplete.css",
                      "~/Content/themes/base/theme.css",
                      "~/Content/themes/base/menu.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryAndValidate").Include(
                           "~/Scripts/jquery-{version}.js"
                          , "~/Scripts/jquery.validate.js"
                          , "~/Scripts/jquery.validate.unobtrusive.js"));
        }
    }
}
