using Godot;
using System;

public class Global : Node
{
	public Node CurrentScene { get; set; }
	public override void _Ready()
	{
		Viewport root = GetTree().Root;
		CurrentScene = root.GetChild(root.GetChildCount() - 1);
	}
}
