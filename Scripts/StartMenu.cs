using Godot;
using System;

public class StartMenu : Control
{
	public override void _Ready()
	{

	}


	public override void _Process(float delta)
	{

	}
	private void _on_Start_pressed()
	{
		GD.Print("CHUYEN SANG MAN CHOI CHINH!");
		GetTree().ChangeScene("res://Scenes/Game.tscn");
	}

	
	private void _on_Ranking_pressed()
	{
		GetTree().ChangeScene("res://Scenes/Ranking.tscn");
	}


	private void _on_Options_pressed()
	{
		GD.Print("CHUYEN SANG PHAN CAI DAT!");
	}

	private void _on_Exit_pressed()
	{
		GD.Print("THOAT GAME!");
		GetTree().Quit();
	}

}









