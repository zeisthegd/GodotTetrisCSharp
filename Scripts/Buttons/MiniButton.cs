using Godot;
using System;

public class MiniButton : Node
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	private void _on_Pause_pressed()
	{
		// Replace with function body.
	}


	private void _on_FullScreen_pressed()
	{
		OS.WindowFullscreen = !OS.WindowFullscreen;
	}


	private void _on_Audio_pressed()
	{
		// Replace with function body.
	}
}

