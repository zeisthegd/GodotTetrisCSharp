using Godot;
using System;

public class MiniButton : Node
{
    UI ui;
	public override void _Ready()
	{
        ui = (UI)GetParent();
	}

	private void _on_Pause_pressed()
	{
        ui.GameOverFunction();
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

