using ArganaWeedApp.Models;
using ArganaWeedApp.ViewModels;
using Microsoft.Maui.Controls;

namespace ArganaWeedApp.Views
{
    public partial class UserDetailPage : ContentPage
    {
        public UserDetailPage(User selectedUser)
        {
            InitializeComponent();
            BindingContext = new UserDetailViewModel(selectedUser);
        }
    }
}
