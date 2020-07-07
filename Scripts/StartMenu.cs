using Godot;
using System;

public class StartMenu : Control
{
	AudioStream loginScreen = (AudioStream)ResourceLoader.Load(@"res://Audio/Music/Home - Toby Fox.ogg");
    Global global;
	public override void _Ready()
	{
        AutoLoad.PlayMusic(this,loginScreen);
	}


	private void _on_Start_pressed()
	{
        AutoLoad.Global.GotoScene("res://Scenes/Game.tscn");
	}

	
	private void _on_Ranking_pressed()
	{
        AutoLoad.Global.GotoScene("res://Scenes/Ranking.tscn");
    }


	private void _on_Options_pressed()
	{
        AutoLoad.Global.GotoScene("res://Scenes/Option.tscn");
	}

	private void _on_Exit_pressed()
	{
		GetTree().Quit();
	}
	private void _on_Logout_pressed()
	{
        AutoLoad.Global.GotoScene("res://Scenes/LoginScreen.tscn");
	}

}












