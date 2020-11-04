using Godot;
using System;
using Database;

public class LoginScreen : Control
{
	LoginWindow loginWindow;
	RegisterWindow registerWindow;
	AudioStream loginScreen = (AudioStream)ResourceLoader.Load(@"res://Audio/Music/Home - Toby Fox.ogg");

	public override void _Ready()
	{
		loginWindow = (LoginWindow)GetNode("LoginWindow");
		registerWindow = (RegisterWindow)GetNode("RegisterWindow");
		loginWindow.Hide();
		registerWindow.Hide();
		AutoLoad.PlayMusic(this, loginScreen);
	}

	private void _on_LoginButton_pressed()
	{
		loginWindow.Show();
		loginWindow.Username.GrabFocus();
	}

	private void _on_Quit_pressed()
	{
		GetTree().Quit();
	}


}












