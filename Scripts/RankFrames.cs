using Godot;
using System;

public class RankFrames : TextureButton
{
	Label rank, username, highscore;

	public override void _Ready()
	{
		rank = (Label)GetNode("Rank");
		username = (Label)GetNode("Username");
		highscore = (Label)GetNode("Highscore");
		if (rank == null || username == null || highscore == null)
			GD.Print("null");
	}

	public void DisplayPlayerData(string rank, string username, string highscore)
	{
		this.rank.Text = rank;
		this.username.Text = username;
		this.highscore.Text = highscore;
	}
	public Label Rank { get => rank; set => rank = value; }
	public Label Username { get => username; set => username = value; }
	public Label Highscore { get => highscore; set => highscore = value; }
}
