﻿

#pragma checksum "C:\Users\asawe_000\Documents\GitHub\RapidisApps\WindowsPhone\RouteDisplayer\RouteDisplayer\Pages\MapPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B0923DDCB874D00CDCE1EF8C7828D397"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RouteDisplayer.Pages
{
    partial class MapPage : global::Windows.UI.Xaml.Controls.Page
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Page pageRoot; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Esri.ArcGISRuntime.Controls.MapView MyMapView; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Esri.ArcGISRuntime.Controls.Map MyMap; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            global::Windows.UI.Xaml.Application.LoadComponent(this, new global::System.Uri("ms-appx:///Pages/MapPage.xaml"), global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            pageRoot = (global::Windows.UI.Xaml.Controls.Page)this.FindName("pageRoot");
            MyMapView = (global::Esri.ArcGISRuntime.Controls.MapView)this.FindName("MyMapView");
            MyMap = (global::Esri.ArcGISRuntime.Controls.Map)this.FindName("MyMap");
        }
    }
}



