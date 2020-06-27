using Godot;
using System;

public class UI : Control
{
	[Export]
	PackedScene gameOverScene = (PackedScene)ResourceLoader.Load("res://Scenes/GameOver.tscn");

	public override void _Ready()
	{
		
	}

	public void GameOverFunction()
	{
		GameOver newGameOverScene = (GameOver)gameOverScene.Instance();
		AddChild(newGameOverScene);       		
	}
}
