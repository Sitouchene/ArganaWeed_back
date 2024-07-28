using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArganaWeedApp.Views
{
    public class CustomTableSectionHeaderView : StackLayout
    {
        public CustomTableSectionHeaderView(string title)
        {
            BackgroundColor = (Color)Application.Current.Resources["PrimaryDark"];
            Padding = new Thickness(10);
            Children.Add(new Label
            {
                Text = title,
                TextColor = (Color)Application.Current.Resources["PrimaryDarkText"],
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalOptions = LayoutOptions.Center
            });
        }
    }
}
