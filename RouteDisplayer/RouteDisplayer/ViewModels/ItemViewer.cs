using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace RouteDisplayer.ViewModels
{
    class ItemViewer : DependencyObject
    {
        public ItemViewer(string description, string stopType, int sequenceNumber)
        {
            this.Description = description;
            this.StopType = stopType;
            this.SequenceNumber = sequenceNumber;
        }

        public ItemViewer()
        {
            this.Description = "test description";
            this.StopType = "stoptype";
            this.SequenceNumber = 1;
        }


        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(ItemViewer), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty StopTypeProperty =
             DependencyProperty.Register("StopType", typeof(string), typeof(ItemViewer), new PropertyMetadata(string.Empty));
        public String Description
        {
            get { return (string)GetValue(DescriptionProperty); }
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
