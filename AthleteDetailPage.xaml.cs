using Summer_games_WebAPI_Client.Data;
using Summer_games_WebAPI_Client.Models;
using Summer_games_WebAPI_Client.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Summer_games_WebAPI_Client
{
    public sealed partial class AthleteDetailPage : Page
    {

        Athlete view;
        IAthleteRepository athleteRepository;
        ISportRepository sportRepository;
        IContingentRepository contingentRepository;
        bool InsertMode;
        public AthleteDetailPage()
        {
            this.InitializeComponent();
            athleteRepository = new AthleteRepository();
            sportRepository = new SportRepository();
            contingentRepository = new ContingentRepository();
            fillDropDown();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            view = (Athlete)e.Parameter;

            if (view.ID == 0) //Inserting
            {
                //Disable the delete button if adding
                btnDelete.IsEnabled = false;
                InsertMode = true;
            }
            else
            {
                InsertMode = false;
            }
        }
        private async void fillDropDown()
        {
            try
            {
                List<Contingent> contingents = await contingentRepository.GetContingents();
                //Bind to the ComboBox
                ContingentCombo.ItemsSource = contingents.OrderBy(d => d.Name);

                List<Sport> sports = await sportRepository.GetSports();
                //Bind to the ComboBox
                SportCombo.ItemsSource = sports.OrderBy(d => d.Name);
                //Now you can assign the DataContext for the page
                this.DataContext = view;
            }
            catch (ApiException apiEx)
            {
                string errMsg = "Errors:" + Environment.NewLine;
                foreach (var error in apiEx.Errors)
                {
                    errMsg += Environment.NewLine + "-" + error;
                }
                Jeeves.ShowMessage("Problem filling Contingent Selection:", errMsg);
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
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (view.ContingentID == 0)
                {
                    Jeeves.ShowMessage("Error", "You must select the Contingent first");
                }
                else
                {
                    if (InsertMode)
                    {
                        await athleteRepository.AddAthlete(view);
                    }
                    else
                    {
                        await athleteRepository.UpdateAthlete(view);
                    }
                    Frame.GoBack();
                }
            }
            catch (AggregateException aex)
            {
                string errMsg = "Errors:" + Environment.NewLine;
                foreach (var exception in aex.InnerExceptions)
                {
                    errMsg += Environment.NewLine + exception.Message;
                }
                Jeeves.ShowMessage("One or more exceptions has occurred:", errMsg);
            }
            catch (ApiException apiEx)
            {
                string errMsg = "Errors:" + Environment.NewLine;
                foreach (var error in apiEx.Errors)
                {
                    errMsg += Environment.NewLine + "-" + error;
                }
                Jeeves.ShowMessage("Problem Saving the Record:", errMsg);
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().Message.Contains("connection with the server"))
                {
                    Jeeves.ShowMessage("Error", "No connection with the server.");
                }
                else
                {
                    Jeeves.ShowMessage("Error", "Could not complete operation.");
                }
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string strTitle = "Confirm Delete";
            string strMsg = "Are you certain that you want to delete " + view.Summary + "?";
            ContentDialogResult result = await Jeeves.ConfirmDialog(strTitle, strMsg);
            if (result == ContentDialogResult.Secondary)
            {
                try
                {
                    await athleteRepository.DeleteAthlete(view);
                    Frame.GoBack();
                }
                catch (AggregateException aex)
                {
                    string errMsg = "Errors:" + Environment.NewLine;
                    foreach (var exception in aex.InnerExceptions)
                    {
                        errMsg += Environment.NewLine + exception.Message;
                    }
                    Jeeves.ShowMessage("One or more exceptions has occurred:", errMsg);
                }
                catch (ApiException apiEx)
                {
                    string errMsg = "Errors:" + Environment.NewLine;
                    foreach (var error in apiEx.Errors)
                    {
                        errMsg += Environment.NewLine + "-" + error;
                    }
                    Jeeves.ShowMessage("Problem Deleting the Record:", errMsg);
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
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}

