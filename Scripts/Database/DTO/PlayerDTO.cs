using Godot;
using System;

namespace Database
{
    public class PlayerDTO : Node
    {
        string username = "", password = "";
        int highscore = -1, playerID;

        public PlayerDTO()
        {
        }
        public PlayerDTO(int playerID,string username, string password, int highscore)
        {
            this.playerID = playerID;
            this.username = username;
            this.password = password;
            this.highscore = highscore;
        }

        public string UserName { get => username; set => username = value; }
        public string PassWord { get => password; set => password = value; }
        public int HighScore { get => highscore; set => highscore = value; }
        public int PlayerID { get => playerID; set => playerID = value; }

        public bool IsAvailable()
        {
            if (username == "" && password == "" && highscore == -1 && playerID == 0)
                return false;
            return true;
        }
        public override string ToString()
        {
            return $"{playerID} {username} {password} {highscore}";
        }
    }
}
