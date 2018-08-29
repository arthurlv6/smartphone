using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SmartPhone.Helper
{
    public class ListViewGrouping<T>: ObservableCollection<T>
    {
        public string Title { get; set; }
        public string ShortName { get; set; }
        public ListViewGrouping(string title,string shortName)
        {
            Title = title;
            ShortName = shortName;
        }
    }
}
