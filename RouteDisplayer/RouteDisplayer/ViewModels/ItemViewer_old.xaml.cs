using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Esri.ArcGISRuntime.Layers; 

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace RouteDisplayer.ViewModels
{
    public sealed partial class ItemViewer_old : UserControl 
    {
        public ItemViewer_old(string description, string stopType, int sequenceNumber)
        {
            this.Description = description;
            this.StopType = stopType;
            this.SequenceNumber = sequenceNumber;
            this.InitializeComponent();
            this.DataContext = this;
        }

        public ItemViewer_old()
        {
            this.InitializeComponent();
            this.Description = "test description";
            this.StopType = "stoptype";
            this.SequenceNumber = 1;
            this.DataContext = this;
        }


        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(ItemViewer), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty StopTypeProperty =
             DependencyProperty.Register("StopType", typeof(string), typeof(ItemViewer), new PropertyMetadata(string.Empty));
        public String Description 
        {
            get { return (string)GetValue(DescriptionProperty);}
            set { SetValue(DescriptionProperty, value); }
        }
        public String StopType 
        {
            get { return (string)GetValue(StopTypeProperty); }
            set { SetValue(StopTypeProperty, value); }
        }
        public int SequenceNumber { get; set; }


    } 
}
