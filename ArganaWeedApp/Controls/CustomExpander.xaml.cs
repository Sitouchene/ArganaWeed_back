using Microsoft.Maui.Controls;
using System;

namespace ArganaWeedApp.Controls
{
    public partial class CustomExpander : ContentView
    {
        public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create(
            nameof(HeaderText), typeof(string), typeof(CustomExpander), default(string), propertyChanged: OnHeaderTextChanged);

        public string HeaderText
        {
            get => (string)GetValue(HeaderTextProperty);
            set => SetValue(HeaderTextProperty, value);
        }

        public CustomExpander()
        {
            InitializeComponent();
            HeaderContainer.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(ToggleContent) });
        }

        private static void OnHeaderTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomExpander)bindable;
            control.HeaderLabel.Text = (string)newValue;
        }

        private void ToggleContent()
        {
            ContentContainer.IsVisible = !ContentContainer.IsVisible;
            ExpandButton.Text = ContentContainer.IsVisible ? "˄" : "˅";
        }
    }
}
