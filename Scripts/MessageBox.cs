using Godot;
using System;

public class MessageBox : Popup
{  
	string message;
	RichTextLabel rtfLabel;

	public RichTextLabel RtfLabel { get => rtfLabel; set => rtfLabel = value; }
	public string Message { get => message; set => message = value; }

	public override void _Ready()
	{
		rtfLabel = (RichTextLabel)GetNode("Message");
		rtfLabel.Text = message;
		PopupCentered();
	}

	public MessageBox() { }

	public MessageBox(string msg)
	{
		message = msg;
	}
   
	


}
