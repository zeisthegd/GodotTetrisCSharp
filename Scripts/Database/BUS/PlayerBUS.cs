using Godot;
using System;
using System.Collections.Generic;
using System.Data;

namespace Database
{
    class PlayerBUS : Node
    {
        PlayerDAO playerDAO;
        int currentPlayerIndex = 0;
        List<PlayerDTO> playersList = new List<PlayerDTO>();

        public List<PlayerDTO> PlayersList { get => playersList; }

        public PlayerBUS()
        {
            playerDAO = new PlayerDAO();
            GetPlayersData();
        }

        private void GetPlayersData()
        {
            string[] dataFields;
            if (playerDAO.PlayersData != null)
            {
                for (int i = 0; i < playerDAO.PlayersData.Length; i++)
                {
                    dataFields = playerDAO.PlayersData[i].Trim().Split(' ');
                    GD.Print(i);
                    playersList.Add(new PlayerDTO(int.Parse(dataFields[0]), dataFields[1], dataFields[2], int.Parse(dataFields[3])));
                }
            }
        }

        public bool RegisterNewPlayer(string username,string password)
        {
            foreach(PlayerDTO player in playersList)
            {
                if (username == player.UserName)
                {
                    AutoLoad.FloatingTextSpawner.ShowMessage("Username unavailable. Please choose another one!");
                    return false;
                }
            }
            playersList.Add(new PlayerDTO(playersList.Count + 1, username, password, 0));
            playerDAO.ListToArrayToFile(playersList);
            AutoLoad.FloatingTextSpawner.ShowMessage($"Added Player: {username}!");
            return true;
        }



        public bool CheckPlayerLoginData(string username, string password)
        {
            if(CheckUsername(username))
            {
                if(CheckPassword(password))
                {
                    return true;
                }
            }
            return false;

        }

        private bool CheckUsername(string username)
        {
            for (int i = 0; i < playersList.Count; i++)
            {
                if (username == playersList[i].UserName)
                {
                    currentPlayerIndex = i;
                    return true;
                }
            }
            AutoLoad.FloatingTextSpawner.ShowMessage("Unable to find this player!");
            return false;
        }
        private bool CheckPassword(string password)
        {

            if (password == playersList[currentPlayerIndex].PassWord)
            {
                return true;
            }

            AutoLoad.FloatingTextSpawner.ShowMessage("Wrong Password!");
            return false;
        }
        public void UpdatePlayersData()
        {
            playerDAO.ListToArrayToFile(playersList);
        }
    }
}


