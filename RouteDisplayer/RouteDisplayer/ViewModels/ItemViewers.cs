using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using RouteDisplayer.Models;
using Esri.ArcGISRuntime.Layers;

namespace RouteDisplayer.ViewModels
{
    class ItemViewers : ObservableCollection<ItemViewer>
    {
        public ItemViewers()
        {
            //foreach (Graphic element in Routes.Instance.RouteElements)
            if(Routes.Instance.RouteElements != null && Routes.Instance.RouteElements.FeatureSet.Features.Count > 0)
            {
                var features = Routes.Instance.RouteElements.FeatureSet.Features;
          
                foreach (var feature in features)
                {
                    System.Diagnostics.Debug.WriteLine("no of attributes: " + feature.Attributes.Count + " " + feature.Attributes["Description"]);
                    Graphic item = (Graphic)feature;
                    int number = -1;
                    int.TryParse(item.Attributes["SequenceNumber"].ToString(), out number);
                    ItemViewer viewer = new ItemViewer(item.Attributes["Description"].ToString(), item.Attributes["StopType"].ToString(), number);
                    Add(viewer);
                }
            }
        }
    }
}
