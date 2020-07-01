using Godot;
using System;

public class LoginScreen : Control
{
	MessageBox messageBox;
	TextureRect loginWindow;
	LineEdit username, password;

	public override void _Ready()
	{
		loginWindow = (TextureRect)GetNode("LoginWindow");
		username = (LineEdit)loginWindow.GetNode("Username");
		password = (LineEdit)loginWindow.GetNode("Password");
		loginWindow.Hide();
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
		if (username.Text != "" && password.Text != "")
			return true;
		return false;
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







