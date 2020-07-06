using Godot;
using System;

public class RankingScene : Node2D
{
	public override void _Ready()
	{

	}

	private void _on_TextureButton_pressed()
	{
		GetTree().ChangeScene("res://Scenes/StartMenu.tscn");
	}
}



