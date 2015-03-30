using System;
using Esri.ArcGISRuntime.Tasks.Geoprocessing;

namespace RouteDisplayer.Models
{
    /// <summary>
    /// Description of Routes
    /// </summary>
    public sealed class Routes
    {
        private static Routes instance = new Routes();
        private GPFeatureRecordSetLayer route;
        private GPFeatureRecordSetLayer routeElements;

        public GPFeatureRecordSetLayer RouteElements
        {
            get { return routeElements; }
            set { routeElements = value; }
        }

        public GPFeatureRecordSetLayer Route
        {
            get { return route; }
            set { route = value; }
        }

        public static Routes Instance
        {
            get
            {
                if (instance == null)
                    instance = new Routes();
                return instance;
            }
        }

        

        private Routes()
        {
        }
    }
}
