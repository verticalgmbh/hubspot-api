using System;
using System.Collections.Generic;
using System.Text;

namespace Vertical.HubSpot.Api.Pipelines
{
    public class PipelinesApi
    {
        readonly HubSpotRestClient rest;

        internal PipelinesApi(HubSpotRestClient rest) {
            this.rest = rest;
        }


    }
}
