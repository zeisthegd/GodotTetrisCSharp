using Godot;
using System;

public class GameOver : Control
{
	UI ui;
	Label name, lines, hiScore;
	int width = 96, height = 59;
	public override void _Ready()
	{
		SetAppearPosition();
		name = (Label)GetNode("Data").GetNode("Username");
		lines = (Label)GetNode("Data").GetNode("Lines");
		hiScore = (Label)GetNode("Data").GetNode("Highscore");
		ui = (UI)GetParent();
		SetText();
	}

	private void SetText()
	{
		name.Text = AutoLoad.PlayerBUS.GetCurrentPlayer().UserName;
		lines.Text = ui.TotalLinesScored();
		hiScore.Text = AutoLoad.PlayerBUS.GetCurrentPlayer().HighScore.ToString();
	}

	public void SetAppearPosition()
	{
		RectGlobalPosition = new Vector2(AutoLoad.WINDOW_WIDTH/2 - width,
			AutoLoad.WINDOW_HEIGHT / 2 - height);
	}
	private void _on_Restart_pressed()
	{
		GetTree().ReloadCurrentScene();
	}


	private void _on_MainMenu_pressed()
	{
		GetTree().ChangeScene("res://Scenes/StartMenu.tscn");
	}

	private void _on_Ranking_pressed()
	{
		GetTree().ChangeScene("res://Scenes/Ranking.tscn");
	}
}





