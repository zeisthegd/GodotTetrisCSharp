using Godot;
using System;

public class FloatingTextSpawner : Node2D
{
	PackedScene messageBoxScene = (PackedScene)GD.Load("res://Scenes/MessageBox.tscn");

	public void ShowMessage(string message)
	{
		MessageBox messageBox = (MessageBox)messageBoxScene.Instance();
		messageBox.Message = message;
        AutoLoad.Global.CurrentScene.AddChild(messageBox);

	}
}
