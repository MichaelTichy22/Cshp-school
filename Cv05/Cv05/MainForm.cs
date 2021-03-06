﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cv05
{
    public partial class MainForm : Form
    {
        Players players;

        bool events = false;

        public MainForm()
        {
            InitializeComponent();
            players = new Players(100);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            PlayerForm playerForm = new PlayerForm();
            if (playerForm.ShowDialog() == DialogResult.OK)
            {
                players.Add(playerForm.NewPlayer);
                playerTable.Rows.Add(playerForm.NewPlayer.Name, playerForm.NewPlayer.Club, playerForm.NewPlayer.NumberOfGoals);
                if (events)
                {
                    logList.Items.Add("Hráč " + playerForm.NewPlayer.Name + " byl vložen.");

                }
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            players.Remove(playerTable.CurrentCell.RowIndex);
            playerTable.Rows.RemoveAt(playerTable.CurrentCell.RowIndex);
            if (events)
            {
                logList.Items.Add("Hráč byl smazán");
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            PlayerForm playerForm = new PlayerForm();
            playerForm.SetForm(players[playerTable.CurrentCell.RowIndex]);
            //playerForm.NewPlayer = players[playerTable.CurrentCell.RowIndex];
            if (playerForm.ShowDialog() == DialogResult.OK)
            {
                players[playerTable.CurrentCell.RowIndex] = playerForm.NewPlayer;
                this.RefreshList();
                if (events)
                {
                    logList.Items.Add("Hráč " + playerForm.NewPlayer.Name + " byl editován.");
                }
            }
        }

        private void bestButton_Click(object sender, EventArgs e)
        {
            BestClubForm bestClubForm = new BestClubForm();
            List<FootballClub> bestClubs = new List<FootballClub>();
            int numberOfGoals = 0;
            players.FindTheBestClubs(bestClubs, out numberOfGoals);
            bestClubForm.BestClubs = bestClubs;
            bestClubForm.NumberOfGoals = numberOfGoals;
            bestClubForm.SetForm();
            bestClubForm.ShowDialog();

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            events = true;
            logList.Items.Add("Handler událostí registorván");
        }

        private void cancleButton_Click(object sender, EventArgs e)
        {
            events = false;
            logList.Items.Add("Handler událostí zrušen");
        }

        private void endButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RefreshList()
        {
            playerTable.Rows.Clear();
            foreach (Player player in players.players)
            {
                if (player == null)
                {
                    break;
                }
                playerTable.Rows.Add(player.Name, player.Club, player.NumberOfGoals);
            }
        }
    }
}
