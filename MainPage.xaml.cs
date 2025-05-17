using Summer_games_WebAPI_Client.Data;
using Summer_games_WebAPI_Client.Models;
using Summer_games_WebAPI_Client.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Summer_games_WebAPI_Client
{
    public sealed partial class MainPage : Page
    {
        private readonly IAthleteRepository athleteRepository;
        private readonly ISportRepository sportRepository;
        private readonly IContingentRepository contingentRepository;

        public MainPage()
        {
            InitializeComponent();
            athleteRepository = new AthleteRepository();
            sportRepository = new SportRepository();
            contingentRepository = new ContingentRepository();
            ShowAthletes(null, null); // Show all athletes initially
            FillDropDown();
        }

        private async void ShowAthletes(int? SportID, int? ContingentID)
        {
            // Show Progress
            progRing.IsActive = true;
            progRing.Visibility = Visibility.Visible;

            try
            {
                List<Athlete> athletes;

                // Check if SportID is provided and greater than 0
                if (SportID.GetValueOrDefault() > 0)
                {
                    athletes = await athleteRepository.GetAthletesBySportID(SportID.Value);
                    
                }
                // Check if only ContingentID is provided
                else if (ContingentID.GetValueOrDefault() > 0)
                {
                    athletes = await athleteRepository.GetAthletesByContingentID(ContingentID.Value);
                }

                // If neither is provided, fetch all athletes
                else
                {
                    athletes = await athleteRepository.GetAthletes();
                }

                athletesList.ItemsSource = athletes;
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().Message.Contains("connection with the server"))
                {
                    Jeeves.ShowMessage("Error", "No connection with the server.");
                }
                else if(ex.GetBaseException().Message.Contains("Could not access Athletes by Sport."))
                {
                    Jeeves.ShowMessage("Error", "Could not access Athletes by that Sport.");

                }
                else if (ex.GetBaseException().Message.Contains("Could not access Athletes by Contingent."))
                {
                    Jeeves.ShowMessage("Error", "Could not access Athletes by that Contingent.");

                }
                else
                {
                    Jeeves.ShowMessage("Error", "Could not complete operation");
                }
            }
            finally
            {
                progRing.IsActive = false;
                progRing.Visibility = Visibility.Collapsed;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            ShowAthletes(null, null); // Refresh the list with no filters
        }

        private async void FillDropDown()
        {
            // Show Progress
            progRing.IsActive = true;
            progRing.Visibility = Visibility.Visible;

            try
            {
                //List<Athlete> athletes = await athleteRepository.GetAthletes();
                List<Sport> sports = await sportRepository.GetSports();
                List<Contingent> contingents = await contingentRepository.GetContingents();

                // Add the "All" Option for both filters
                sports.Insert(0, new Sport { ID = 0, Name = "All Sports" });
                contingents.Insert(0, new Contingent { ID = 0, Name = "All Contingents" });

                btnAdd.IsEnabled = true;

                // Bind to the ComboBoxes
                AthleteComboSportID.ItemsSource = sports;
                AthleteComboContingentID.ItemsSource = contingents;
            }
            catch (ApiException apiEx)
            {
                string errMsg = "Errors:" + Environment.NewLine;
                foreach (var error in apiEx.Errors)
                {
                    errMsg += Environment.NewLine + "-" + error;
                }
                Jeeves.ShowMessage("Problem filling Athlete Selection:", errMsg);
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().Message.Contains("connection with the server"))
                {
                    Jeeves.ShowMessage("Error", "No connection with the server.");
                }
                else
                {
                    Jeeves.ShowMessage("Error", "Could not complete operation");
                }
            }
            finally
            {
                progRing.IsActive = false;
                progRing.Visibility = Visibility.Collapsed;
            }
        }

        private void AthleteComboSportID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                Sport selectedSport = (Sport)AthleteComboSportID.SelectedItem;
                ShowAthletes(selectedSport?.ID, null);
            
        }

        private void AthleteComboContingentID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                Contingent selectedContingent = (Contingent)AthleteComboContingentID.SelectedItem;
                ShowAthletes(null, selectedContingent?.ID);

        }

        private void athleteGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the detail page
            Frame.Navigate(typeof(AthleteDetailPage), (Athlete)e.ClickedItem);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Athlete newPat = new Athlete();
            newPat.DOB = DateTime.Now;

            // Navigate to the detail page
            Frame.Navigate(typeof(AthleteDetailPage), newPat);
        }

    }
}
