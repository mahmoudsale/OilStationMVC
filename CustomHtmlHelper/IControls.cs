using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.CustomHtmlHelper
{
  public  interface IControls
    {


        IControls ID(string Id);

        IControls Name(string Name);

        IControls Value(string value);

        IControls ReadOnly(bool value);

        IControls PlaceHolder(string PlaceHolder);

        IControls CssClass(string CssClass);

        IHtmlString Render();

    }
}
