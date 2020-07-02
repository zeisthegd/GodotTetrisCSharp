using Godot;
using System;

public class PlayerDTO : Node
{
    string userName, passWord;
    int highScore, playerID;

    public string UserName { get => userName; set => userName = value; }
    public string PassWord { get => passWord; set => passWord = value; }
    public int HighScore { get => highScore; set => highScore = value; }
    public int PlayerID { get => playerID; set => playerID = value; }
}
