using Godot;
using System;

public class Option : Control
{
	Label mVolumeValue;
	CheckBox fullScreen;
	HSlider musicVolume;
	public override void _Ready()
	{
		fullScreen = (CheckBox)GetNode("FullScreen");
		musicVolume = (HSlider)GetNode("MusicVolume");
		mVolumeValue = (Label)GetNode("MVolumeValue");

		fullScreen.Pressed = AutoLoad.FullScreen;
		musicVolume.Value = AutoLoad.MusicVolume;
		mVolumeValue.Text = ((int)musicVolume.Value).ToString();
	}

	private void _on_Back_pressed()
	{
		GetTree().ChangeScene("res://Scenes/StartMenu.tscn");
	}


	private void _on_CheckBox_pressed()
	{
		AutoLoad.FullScreen = fullScreen.Pressed;
		AutoLoad.SaveConfig();
		AutoLoad.LoadConfig();
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
}



