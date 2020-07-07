using Godot;
using System;
using Database;

public class UI : Control
{
	[Export]
	PackedScene gameOverScene = (PackedScene)ResourceLoader.Load("res://Scenes/GameOver.tscn");
	Label lines, hiScore;

	int currentScore = 0;

	public override void _Ready()
	{
		lines = (Label)GetNode("Scoring").GetNode("Lines");
		hiScore = (Label)GetNode("Scoring").GetNode("HighScore");
		hiScore.Text = AutoLoad.PlayerBUS.GetCurrentPlayer().HighScore.ToString();
	}

	public void SetScore(int score)
	{
		currentScore += score;
		lines.Text = currentScore.ToString();
	}

	public void GameOverFunction()
	{
		GameOver newGameOverScene = (GameOver)gameOverScene.Instance();
		AddChild(newGameOverScene);
		if (Convert.ToInt32(lines.Text) > AutoLoad.PlayerBUS.GetCurrentPlayer().HighScore)
			AutoLoad.PlayerBUS.UpdateHighScore(Convert.ToInt32(lines.Text));

	}

	public string TotalLinesScored()
	{
		return lines.Text;
	}
}
