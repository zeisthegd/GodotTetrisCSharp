using Godot;
using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Database
{
    class PlayerDAO : Node
    {
        string[] playersData;
        public PlayerDAO()
        {
            if (DbConnection.SaveFileExists())
                GetPlayersFromFile();
            else GD.Print("File not exist");
        }

        public string[] PlayersData { get => playersData; set => playersData = value; }

        private void GetPlayersFromFile()
        {
            try
            {
                playersData = System.IO.File.ReadAllText(AutoLoad.SavePathForCSharp).Split('\n');
            }
            catch (Exception e)
            {
                AutoLoad.FloatingTextSpawner.ShowMessage("Unable to find players' data file!");
                GD.Print(e.Message);
            }
        }

        public void ListToArrayToFile(List<PlayerDTO> playersList)
        {
            string[] players = new string[playersList.Count];

            for (int i = 0; i < players.Length; i++)
            {
                players[i] = playersList[i].ToString();
            }
            DbConnection.WriteToFile(players);
            
        }


    }
}


