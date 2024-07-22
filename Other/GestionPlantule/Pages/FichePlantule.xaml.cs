using GestionPlantule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using System;
using System.Linq;

namespace GestionPlantule.Pages
{
    public partial class FichePlantule : ContentPage
    {
        private dataManagement dm = new dataManagement();

        public FichePlantule(Plantule plantule = null)
        {
            InitializeComponent();
            LoadPickerData();

            if (plantule != null)
            {
                IdEntry.Text = plantule.PlantuleVariete;
                plantuleVarietePicker.SelectedItem = dm.GetAllVarietes().FirstOrDefault(v => v.VarieteId == plantule.VarieteId)?.VarieteNom;
                plantuleDescriptionEditor.Text = plantule.PlantuleDescription;
                dateReceptionPicker.Date = plantule.DateReception;
                provenancePicker.SelectedItem = dm.GetAllProvenances().FirstOrDefault(p => p.ProvenanceId == plantule.ProvenanceId)?.ProvenanceNom;
                stadePicker.SelectedItem = plantule.Stade;
                santePicker.SelectedItem = plantule.Sante;
                emplacementPicker.SelectedItem = dm.GetAllEmplacements().FirstOrDefault(e => e.EmplacementId == plantule.EmplacementId)?.EmplacementCode;

                if (plantule.SortieDate.HasValue)
                {
                    sortieDatePicker.Date = plantule.SortieDate.Value;
                }
                else
                {
                    sortieDatePicker.Date = DateTime.Today;
                }

                statutCheckBox.IsChecked = plantule.Statut;
            }

            dateReceptionPicker.DateSelected += OnDateReceptionChanged;
            sortieDatePicker.DateSelected += OnSortieDateChanged;
        }

        private void LoadPickerData()
        {
            try
            {
                plantuleVarietePicker.ItemsSource = dm.GetAllVarietes().Select(v => v.VarieteNom).ToList();
                provenancePicker.ItemsSource = dm.GetAllProvenances().Select(p => p.ProvenanceNom).ToList();
                emplacementPicker.ItemsSource = dm.GetAllEmplacements().Select(e => e.EmplacementCode).ToList();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"Failed to load picker data: {ex.Message}", "OK");
            }
        }

        private void OnValidateClicked(object sender, EventArgs e)
        {
            try
            {
                string plantuleId = IdEntry.Text;
                string varieteNom = plantuleVarietePicker.SelectedItem?.ToString();
                string plantuleDescription = plantuleDescriptionEditor.Text;
                DateTime dateReception = dateReceptionPicker.Date;
                string provenanceNom = provenancePicker.SelectedItem?.ToString();
                string stade = stadePicker.SelectedItem?.ToString();
                string sante = santePicker.SelectedItem?.ToString();
                string emplacementCode = emplacementPicker.SelectedItem?.ToString();
                DateTime? sortieDate = (sortieDatePicker.Date == DateTime.Today) ? null : (DateTime?)sortieDatePicker.Date;
                string qrCode = ""; // Assuming qrCode is generated elsewhere

                if (string.IsNullOrWhiteSpace(varieteNom))
                {
                    throw new ArgumentException("Variété is required.");
                }

                if (string.IsNullOrWhiteSpace(provenanceNom))
                {
                    throw new ArgumentException("Provenance is required.");
                }

                if (string.IsNullOrWhiteSpace(emplacementCode))
                {
                    throw new ArgumentException("Emplacement is required.");
                }

                dm.AddOrUpdatePlantule(plantuleId, varieteNom, plantuleDescription, dateReception, provenanceNom, stade, sante, emplacementCode, sortieDate, qrCode);
                DisplayAlert("Success", "Plantule information saved successfully", "OK");
            }
            catch (ArgumentException ex)
            {
                DisplayAlert("Validation Error", ex.Message, "OK");
            }
            catch (DbUpdateException ex)
            {
                DisplayAlert("Database Error", $"An error occurred while saving the entity changes: {ex.InnerException?.Message ?? ex.Message}", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"An error occurred while saving the plantule information: {ex.Message}", "OK");
            }
        }


        private void OnDateReceptionChanged(object sender, DateChangedEventArgs e)
        {
            try
            {
                if (e.NewDate != e.OldDate)
                {
                    statutCheckBox.IsChecked = true;
                    sortieDatePicker.Date = DateTime.Today;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"An error occurred while changing the reception date: {ex.Message}", "OK");
            }
        }

        private void OnSortieDateChanged(object sender, DateChangedEventArgs e)
        {
            try
            {
                if (e.NewDate != e.OldDate)
                {
                    statutCheckBox.IsChecked = false;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"An error occurred while changing the sortie date: {ex.Message}", "OK");
            }
        }

        private void OnPrintQrCodeClicked(object sender, EventArgs e)
        {
            try
            {
                // Print QR code logic
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"An error occurred while printing the QR code: {ex.Message}", "OK");
            }
        }

        private void OnPrintFicheClicked(object sender, EventArgs e)
        {
            try
            {
                // Print fiche logic
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"An error occurred while printing the fiche: {ex.Message}", "OK");
            }
        }

        private void OnCloseClicked(object sender, EventArgs e)
        {
            try
            {
                // Close logic
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"An error occurred while closing the page: {ex.Message}", "OK");
            }
        }
    }
}
