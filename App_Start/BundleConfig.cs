using System.Web;
using System.Web.Optimization;

namespace OilStationMVC
{
    public class BundleConfig
    {

        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles1(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
              .Include("~/assets/lib/jquery-validation/dist/jquery.validate.js")
              .Include("~/assets/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
        }


        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862

        public static string JsBundle = "~/Scripts/bundle";
        public static string CssBundle = "~/Style/bundle";
        public static string SignalRJsBundle = "~/SignalRScripts/bundle";
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            bundles.UseCdn = true;   //enable CDN support
            var scriptBundle = new ScriptBundle(JsBundle);
            var styleBundle = new StyleBundle(CssBundle);
            var SignalRscriptBundle = new ScriptBundle(SignalRJsBundle);
            styleBundle
                .Include("~/assets/plugins/fontawesome-free/css/all.min.css")
                //.Include("https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css")
                .Include("~/assets/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css")
                .Include("~/assets/plugins/datatables-responsive/css/responsive.bootstrap4.min.css")
                .Include("~/assets/plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css")
                .Include("~/assets/plugins/icheck-bootstrap/icheck-bootstrap.min.css")
                .Include("~/assets/plugins/jqvmap/jqvmap.min.css")
                .Include("~/assets/dist/css/adminlte.min.css")
                .Include("~/assets/plugins/overlayScrollbars/css/OverlayScrollbars.min.css")
                .Include("~/assets/plugins/daterangepicker/daterangepicker.css")
                .Include("~/assets/plugins/summernote/summernote-bs4.css")
                //.Include("https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700")
                //.Include("https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css")
                .Include("~/assets/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css")
                .Include("~/assets/plugins/toastr/toastr.min.css")
                .Include("~/assets/plugins/select2/css/select2.min.css")
                .Include("~/assets/plugins/select2-bootstrap4-theme/select2-bootstrap4.min.css")
                .Include("~/assets/dist/css/adminlte.min.css");

            scriptBundle
                .Include("~/assets/plugins/jquery/jquery.min.js")
                .Include("~/assets/plugins/jquery-ui/jquery-ui.min.js")
                .Include("~/assets/plugins/bootstrap/js/bootstrap.bundle.min.js")
                .Include("~/assets/plugins/select2/js/select2.full.min.js")
                .Include("~/assets/plugins/datatables/jquery.dataTables.min.js")
                .Include("~/assets/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js")
                .Include("~/assets/plugins/datatables-responsive/js/dataTables.responsive.min.js")
                .Include("~/assets/plugins/datatables-responsive/js/responsive.bootstrap4.min.js")
                .Include("~/assets/plugins/chart.js/Chart.min.js")
                .Include("~/assets/plugins/sparklines/sparkline.js")
                .Include("~/assets/plugins/jqvmap/jquery.vmap.min.js")
                .Include("~/assets/plugins/jqvmap/maps/jquery.vmap.usa.js")
                .Include("~/assets/plugins/jquery-knob/jquery.knob.min.js")
                .Include("~/assets/plugins/moment/moment.min.js")
                .Include("~/assets/plugins/daterangepicker/daterangepicker.js")
                .Include("~/assets/plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js")
                .Include("~/assets/plugins/summernote/summernote-bs4.min.js")
                .Include("~/assets/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js")
                .Include("~/assets/plugins/sweetalert2/sweetalert2.min.js")
                .Include("~/assets/plugins/toastr/toastr.min.js")
                .Include("~/assets/dist/js/adminlte.js")
                .Include("~/assets/dist/js/pages/dashboard.js")
                .Include("~/assets/dist/js/demo.js")
                .Include("~/assets/src/ChangeLanguage.js")
                .Include("~/assets/src/ManagerJs.js");


            SignalRscriptBundle
              .Include("~/Scripts/jquery.signalR-2.2.2.js")
              .Include("~/assets/lib/signalr/dist/browser/signalr.js")
              .Include("~/signalr/Hubs");




            bundles.Add(scriptBundle);
            bundles.Add(SignalRscriptBundle);
            bundles.Add(styleBundle);
            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
          .Include("~/assets/lib/jquery-validation/dist/jquery.validate.js")
          .Include("~/assets/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"));


        }
    }
}
