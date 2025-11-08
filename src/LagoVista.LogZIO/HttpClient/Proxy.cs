// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: b37d66fbd3698eec5315980ceeac21b29e1087696de930683df5895f21c113d8
// IndexVersion: 2
// --- END CODE INDEX META ---
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.HttpClient
{
    class Proxy : IWebProxy
    {
        private Uri _proxy;

        public Proxy(Uri proxy)
        {
            this._proxy = proxy;
        }

        public ICredentials Credentials { get; set; }

        public Uri GetProxy(Uri destination)
        {
            return _proxy;
        }

        public bool IsBypassed(Uri host)
        {
            return false;
        }
    }
}
