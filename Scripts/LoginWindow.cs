using Godot;
using System;
using Database;

public class LoginWindow : TextureRect
{
	LineEdit username, password;
	PlayerBUS playerBUS;
	public override void _Ready()
	{
		username = (LineEdit)GetNode("Username");
		password = (LineEdit)GetNode("Password");

	}

	private void _on_Login_pressed()
	{
		if(IsValidated())
		{
			if (CheckUser())
				GetTree().ChangeScene("res://Scenes/StartMenu.tscn");
		}
	}
	private bool CheckUser()
	{
		playerBUS = new PlayerBUS();
		return playerBUS.CheckPlayerLoginData(username.Text, password.Text);

	}
	private void _on_ToRegister_pressed()
	{
		this.Hide();
		TextureRect registerWindow = (TextureRect)GetParent().GetNode("RegisterWindow");
		registerWindow.Show();
	}

	private void _on_Clear_pressed()
	{
		username.Text = "";
		password.Text = "";
	}
	private void _on_Close_pressed()
	{
		this.Hide();
	}

	private bool IsValidated()
	{
		if (username.Text == "")
		{
			AutoLoad.FloatingTextSpawner.ShowMessage("Please input username!");
			return false;
		}
		else if (password.Text == "")
		{
			AutoLoad.FloatingTextSpawner.ShowMessage("Please input password!");
			return false;
		}
		return true;
	}

}









