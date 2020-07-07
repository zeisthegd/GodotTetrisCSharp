using Godot;
using System;
using Database;

public class LoginScreen : Control
{
	TextureRect loginWindow, registerWindow;
	AudioStream loginScreen = (AudioStream)ResourceLoader.Load(@"res://Audio/Music/Home - Toby Fox.ogg");

	public override void _Ready()
	{
		loginWindow = (TextureRect)GetNode("LoginWindow");
		registerWindow = (TextureRect)GetNode("RegisterWindow");
		loginWindow.Hide();
		registerWindow.Hide();
		AutoLoad.PlayMusic(this, loginScreen);
	}


	private void _on_LoginButton_pressed()
	{
		loginWindow.Show();
	}



}










