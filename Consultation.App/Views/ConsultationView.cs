﻿using Consultation.App.Views.IViews;
using System;
using System.Windows.Forms;
using Consultation.App.Views.Controls.ConsultationManagement;


namespace Consultation.App.ConsultationManagement
{

    public partial class ConsultationView : UserControl, IConsultationView
    {

        private List<ConsultationCard> activeCards = new List<ConsultationCard>();
        private List<ArchiveCard> archivedCards = new List<ArchiveCard>();


        public ConsultationView()
        {
            InitializeComponent();

            for (int i = 0; i < 4; i++)
            {
                var card = new ConsultationCard();
                card.Data = new ConsultationData();
                card.ArchiveRequested += (s, e) => ArchiveCard(card);
                activeCards.Add(card);
            }

            ShowConsultationView();
        }


        
        private void ShowConsultationView()
        {
            LabelHeader.Text = "Active Consultation";
            MoveUnderline(btnConsultation);
            WindowPanelConsultation.Controls.Clear();

            foreach (var card in activeCards)
            {
                WindowPanelConsultation.Controls.Add(card);
            }
        }

        public void ArchiveCard(ConsultationCard card)
        {
            WindowPanelConsultation.Controls.Remove(card);
            activeCards.Remove(card);

            var archiveCard = new ArchiveCard();
            archiveCard.Data = card.Data;
            archiveCard.RestoreClicked += (s, e) => RestoreCard(archiveCard);
            archivedCards.Add(archiveCard);

        }


        private void MoveUnderline(Control targetButton)
        {
            underlinePanel.Width = targetButton.Width;
            underlinePanel.Left = targetButton.Left;
            underlinePanel.Top = targetButton.Bottom - 4;
            underlinePanel.Visible = true;
        }


        private void OnCardArchived(object sender, ConsultationCard card)
        {
            WindowPanelConsultation.Controls.Remove(card);
            archivedCards.Remove(card);
            var consultationCard = new ConsultationCard();
            consultationCard.Data = card.Data;
            consultationCard.ArchiveRequested += (s, e) => ArchiveCard(consultationCard);
            activeCards.Add(consultationCard);

        }

        private void ShowArchivedConsultations()
        {
            LabelHeader.Text = "Archived Consultation";
            MoveUnderline(btnArchive);
            WindowPanelConsultation.Controls.Clear();

            foreach (var archiveCard in archivedCards)
            {
                WindowPanelConsultation.Controls.Add(archiveCard);
            }
        }

        private void btnConsultation_Click_1(object sender, EventArgs e)
        {
            ShowConsultationView();
            LabelHeader.Text = "Active Consultation";

        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            ShowArchivedConsultations();
            LabelHeader.Text = "Archived Consultation";

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (LabelHeader.Text == "Active Consultation")
            {
                ShowConsultationView();
            }
            else if (LabelHeader.Text == "Archived Consultation")
            {
                ShowArchivedConsultations();
            }
        }

        private void materialCard1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}