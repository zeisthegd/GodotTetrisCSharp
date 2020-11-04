using Godot;
using System;

public class MessageBox : Control
{
	string message;
	LineEdit rtfLabel;
	Timer timer;

	public LineEdit RtfLabel { get => rtfLabel; set => rtfLabel = value; }
	public string Message { get => message; set => message = value; }

	public override void _Ready()
	{
		timer = (Timer)GetNode("Timer");
		rtfLabel = (LineEdit)GetNode("Message");
		rtfLabel.Text = message;
		
		Show();
		timer.Start();
	}

	public MessageBox() { }

	public MessageBox(string msg)
	{
		message = msg;
	}

	private void _on_Timer_timeout()
	{
		QueueFree();
	}


}



