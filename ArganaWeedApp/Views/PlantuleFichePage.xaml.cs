using ArganaWeedApp.Models;
using ArganaWeedApp.ViewModels;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using CommunityToolkit.Maui.Core;

namespace ArganaWeedApp.Views
{
    public partial class PlantuleFichePage : ContentPage
    {
        public PlantuleFichePage(PlantuleDetail selectedPlantule)
        {
            InitializeComponent();

            var viewModel = new PlantuleFicheViewModel(selectedPlantule);
            BindingContext = viewModel;

            var santeOptions = new List<string> { "Bon", "Moyen", "Mauvais", "En danger" };
            santePicker.ItemsSource = santeOptions;
            santePicker.SelectedItem = selectedPlantule.Sante;

            var stadeOptions = new List<string> { "Initiation", "Micro dissection", "Magenta", "Double magenta", "Hydroponie" };
            stadePicker.ItemsSource = stadeOptions;
            stadePicker.SelectedItem = selectedPlantule.Stade;

            viewModel.LoadEmplacementsCommand.Execute(null);
        }

        private void OnNavigateBackButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as PlantuleFicheViewModel;
            viewModel?.NavigateBackCommand.Execute(null);
        }

        private void OnExpander1ExpandedChanged(object sender, ExpandedChangedEventArgs e)
        {
            UpTriangle1.IsVisible = e.IsExpanded;
            DownTriangle1.IsVisible = !e.IsExpanded;
        }

        private void OnExpander2ExpandedChanged(object sender, ExpandedChangedEventArgs e)
        {
            UpTriangle2.IsVisible = e.IsExpanded;
            DownTriangle2.IsVisible = !e.IsExpanded;
        }

        private void OnExpander3ExpandedChanged(object sender, ExpandedChangedEventArgs e)
        {
            UpTriangle3.IsVisible = e.IsExpanded;
            DownTriangle3.IsVisible = !e.IsExpanded;
        }

        private void OnExpander4ExpandedChanged(object sender, ExpandedChangedEventArgs e)
        {
            UpTriangle4.IsVisible = e.IsExpanded;
            DownTriangle4.IsVisible = !e.IsExpanded;
        }

        private void OnExpanderAddNoteExpandedChanged(object sender, ExpandedChangedEventArgs e)
        {
            UpTriangleAddNote.IsVisible = e.IsExpanded;
            DownTriangleAddNote.IsVisible = !e.IsExpanded;
        }


        private void OnPrintQrButton_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as PlantuleFicheViewModel;
            viewModel?.GeneratePdfCommand.Execute(null);
        }

        private void OnPrintFicheButton_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as PlantuleFicheViewModel;
            //viewModel?.UpdateCommand.Execute(null);
        }

        private void OnSaveEditPlantule_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as PlantuleFicheViewModel;
            viewModel?.SaveCommand.Execute(null);

        }
        private void OnCancelEditPlantule_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as PlantuleFicheViewModel;
            viewModel?.CancelCommand.Execute(null);

        }
        
        private async void OnAddEmplacemntButton_Clicked(object sender, EventArgs e)
        {
            //var viewModel = BindingContext as PlantuleFicheViewModel;
            //viewModel?.CancelCommand.Execute(null);
            await Navigation.PushAsync(new EmplacementNewPage());
        }

        private void OnSortirButton_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as PlantuleFicheViewModel;
            viewModel?.SortirPlantuleCommand.Execute(null);
        }

        private void OnCancelSortieButton_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as PlantuleFicheViewModel;
            viewModel?.CancelSortieCommand.Execute(null);
        }

        // ***
        private void OnAddNoteButton_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as PlantuleFicheViewModel;
            viewModel?.AddNoteCommand.Execute(null);
        }

        private void OnCancelNoteButton_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as PlantuleFicheViewModel;
            viewModel?.CancelNoteCommand.Execute(null);
        }

        
    }
}
