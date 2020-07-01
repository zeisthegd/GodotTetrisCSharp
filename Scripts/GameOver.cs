using Godot;
using System;

public class GameOver : Control
{

	int width = 96, height = 59;
	public override void _Ready()
	{
		SetAppearPosition();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }


	public void SetAppearPosition()
	{
		RectGlobalPosition = new Vector2(AutoLoad.WINDOW_WIDTH/2 - width,
			AutoLoad.WINDOW_HEIGHT / 2 - height);
		GD.Print($"Scene size: {this.RectSize}");
	}
}
