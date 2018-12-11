using System;
using System.Net;

namespace WindowsFormsApp2
{
    class WebClienteLanPlay : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = 1000;
            return w;
        }
    }
}
