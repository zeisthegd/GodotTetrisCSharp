using Godot;
using System;

public class MiniButton : Node
{
    UI ui;
    PackedScene pauseWindowScene = (PackedScene)ResourceLoader.Load("res://Scenes/PauseWindow.tscn");

    public override void _Ready()
	{
        ui = (UI)GetParent();
	}

	private void _on_Pause_pressed()
	{
        PauseWindow newwPauseWindow = (PauseWindow)pauseWindowScene.Instance();

        ui.AddChild(newwPauseWindow);

        GetTree().Paused = true;
    }


    private void _on_FullScreen_pressed()
	{
		OS.WindowFullscreen = !OS.WindowFullscreen;
	}


	private void _on_Audio_pressed()
	{
        if (Game.AudioPlayer.VolumeDb == -100)
            Game.AudioPlayer.VolumeDb = AutoLoad.MusicVolume;
        else if (Game.AudioPlayer.VolumeDb > 0)
            Game.AudioPlayer.VolumeDb = -100;
    }
}

