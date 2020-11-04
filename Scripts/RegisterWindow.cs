using Godot;
using System;
using Database;

public class RegisterWindow : TextureRect
{
	LineEdit username, password, cfmPassword;
	public override void _Ready()
	{
		username = (LineEdit)GetNode("Username");
		password = (LineEdit)GetNode("Password");
		cfmPassword = (LineEdit)GetNode("ConfirmPassword");
	}

	private void _on_ToLogin_pressed()
	{
		ChangeToLoginWindow();
	}
	private void ChangeToLoginWindow()
	{
		this.Hide();
		TextureRect loginWindow = (TextureRect)GetParent().GetNode("LoginWindow");
		loginWindow.Show();
	}
    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("ui_accept"))
            Register();
    }

    private void _on_Register_pressed()
	{
        Register();
	}

    private void Register()
    {
        if (IsValidated())
        {
            if (AutoLoad.PlayerBUS.RegisterNewPlayer(username.Text, password.Text))
            {
                ChangeToLoginWindow();
            }
        }
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
		else if(cfmPassword.Text == "")
		{
			AutoLoad.FloatingTextSpawner.ShowMessage("Please confirm password!");
			return false;
		}
		else if(password.Text != cfmPassword.Text)
		{
			AutoLoad.FloatingTextSpawner.ShowMessage("Confirm password incorrect!");
			return false;
		}
		return true;
	}

	private void _on_Clear_pressed()
	{
		username.Text = "";
		password.Text = "";
		cfmPassword.Text = "";
	}
	private void _on_Close_pressed()
	{
		this.Hide();
	}
}

