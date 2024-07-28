
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using ArganaWeedApp.Views;


namespace ArganaWeedApp.Views
{
    public partial class MenuPage : ContentPage
    {
        private Dictionary<string, List<Tuple<string, string, string, string, Type>>> menuItems;

        public MenuPage(List<string> userRoles, string userName)
        {
            InitializeComponent();
            userNameLabel.Text = userName;
            userRolesLabel.Text = string.Join(", ", userRoles);

            menuItems = MenuItems.GetMenuItems();
            PopulateMenu(userRoles);
        }

        private void PopulateMenu(List<string> userRoles)
        {
            var tableRoot = new TableRoot();

            var sectionsOrder = new List<string> { "Consultation", "Exploitation", "Paramétrages", "Administration" };

            foreach (var sectionTitle in sectionsOrder)
            {
                // Custom header for the section
                var header = new ViewCell
                {
                    View = new StackLayout
                    {
                        BackgroundColor = (Color)Application.Current.Resources["PrimaryDark"],
                        Padding = new Thickness(10),
                        Children =
                        {
                            new Label
                            {
                                Text = sectionTitle,
                                TextColor = (Color)Application.Current.Resources["PrimaryDarkText"],
                                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                VerticalOptions = LayoutOptions.Center
                            }
                        }
                    }
                };

                var section = new TableSection();

                tableRoot.Add(section);
                section.Add(header);

                foreach (var role in userRoles)
                {
                    if (menuItems.ContainsKey(role))
                    {
                        foreach (var item in menuItems[role].Where(i => i.Item2 == sectionTitle))
                        {
                            var cell = new ViewCell
                            {
                                View = new StackLayout
                                {
                                    Orientation = StackOrientation.Horizontal,
                                    Padding = new Thickness(10, 5),
                                    Children =
                                    {
                                        new Image
                                        {
                                            Source = item.Item4,
                                            WidthRequest = 40,
                                            HeightRequest = 40,
                                            Margin = new Thickness(0, 0, 10, 0)
                                        },
                                        new StackLayout
                                        {
                                            VerticalOptions = LayoutOptions.Center,
                                            Children =
                                            {
                                                new Label
                                                {
                                                    Text = item.Item1,
                                                    TextColor = (Color)Application.Current.Resources["PrimaryDarkText"]
                                                },
                                                new Label
                                                {
                                                    Text = item.Item3,
                                                    TextColor = (Color)Application.Current.Resources["PrimaryDarkText"],
                                                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
                                                }
                                            }
                                        }
                                    }
                                }
                            };
                            cell.Tapped += async (s, e) =>
                            {
                                await Navigation.PushAsync((Page)Activator.CreateInstance(item.Item5));
                            };

                            section.Add(cell);

                            var separator = new ViewCell
                            {
                                View = new BoxView
                                {
                                    HeightRequest = 1,
                                    Color = Colors.Gray,
                                    HorizontalOptions = LayoutOptions.FillAndExpand
                                }
                            };

                            section.Add(separator);
                        }
                    }
                }
            }

            menuTableView.Root = tableRoot;
        }
    }

}

    



