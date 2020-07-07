using Godot;
using System;

public class PauseWindow : Control
{
	HSlider musicVolume;
	Label mVolumeValue;
	int sizeX = 192, sizeY = 108;

	public override void _Ready()
	{
		musicVolume = (HSlider)GetNode("MusicVolume");
		mVolumeValue = (Label)GetNode("MVolumeValue");

		musicVolume.Value = AutoLoad.MusicVolume;
		mVolumeValue.Text = ((int)musicVolume.Value).ToString();

		SetAppearPosition();
	}

	private void _on_Restart_pressed()
	{
		GetTree().Paused = false;
		GetTree().ReloadCurrentScene();
	}



	private void _on_Menu_pressed()
	{
		GetTree().Paused = false;
		AutoLoad.Global.GotoScene("res://Scenes/StartMenu.tscn");
	}


	private void _on_Ranking_pressed()
	{
		GetTree().Paused = false;
		AutoLoad.Global.GotoScene("res://Scenes/Ranking.tscn");
	}


	private void _on_MusicVolume_value_changed(float value)
	{
		if (musicVolume.Value == 0)
			AutoLoad.MusicVolume = -100;
		else
			AutoLoad.MusicVolume = (int)musicVolume.Value;

		mVolumeValue.Text = ((int)musicVolume.Value).ToString();
		AutoLoad.SaveConfig();
		AutoLoad.LoadConfig();
	}
	private void _on_Close_pressed()
	{
		GetTree().Paused = false;
		QueueFree();
	}
	public void SetAppearPosition()
	{
		RectGlobalPosition = new Vector2(AutoLoad.WINDOW_WIDTH / 2 - sizeX / 2,
			AutoLoad.WINDOW_HEIGHT / 2 - sizeY / 2);
	}
}






