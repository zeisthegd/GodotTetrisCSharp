using Godot;
using System;
using Database;

public class LoginScreen : Control
{
	TextureRect loginWindow, registerWindow;
	LineEdit username, password;
	PlayerBUS playerBUS;


	public override void _Ready()
	{
		loginWindow = (TextureRect)GetNode("LoginWindow");
		registerWindow = (TextureRect)GetNode("RegisterWindow");
		loginWindow.Hide();
		registerWindow.Hide();
	}


	private void _on_LoginButton_pressed()
	{
		loginWindow.Show();
	}
	private void _on_Login_pressed()
	{
		if (CheckUser())
			GetTree().ChangeScene("res://Scenes/StartMenu.tscn");
	}

	private bool CheckUser()
	{
		playerBUS = new PlayerBUS();
		return playerBUS.CheckPlayerLoginData(username.Text,password.Text);

	}


	private void _on_Cancel_pressed()
	{
		username.Text = "";
		password.Text = "";
	}
	private void _on_Close_pressed()
	{
		loginWindow.Hide();
	}
}










