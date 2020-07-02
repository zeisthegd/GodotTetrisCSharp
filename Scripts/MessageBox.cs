using Godot;
using System;

public class MessageBox : Control
{  
	string message;
	LineEdit rtfLabel;

	public LineEdit RtfLabel { get => rtfLabel; set => rtfLabel = value; }
	public string Message { get => message; set => message = value; }

	public override void _Ready()
	{
		rtfLabel = (LineEdit)GetNode("Message");
		rtfLabel.Text = message;
		Show();
	}

	public MessageBox() { }

	public MessageBox(string msg)
	{
		message = msg;
	}
   
	


}
