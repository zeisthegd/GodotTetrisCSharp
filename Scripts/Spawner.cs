using Godot;
using System;

public class Spawner : Node2D
{
	Vector2 spawnPos;
	PackedScene shapeScene = (PackedScene)GD.Load("res://Scenes/Shape.tscn");

	bool firstShape = true;
	bool secondShape = true;
	Shape nextShape = null;
	Shape lastShape = null;


	public override void _Ready()
	{
		base._Ready();
		Game game = (Game)GetParent();
		game.InitGameBoard();
		Position2D UISpawnPos = (Position2D)GetParent().GetNode("UI/NextBlock/Panel/SpawnPos");

		spawnPos = UISpawnPos.GlobalPosition;

		GD.Randomize();
		Spawn();
	}

	public void Spawn()
	{
		if (lastShape != null)
			lastShape.Position -= new Vector2(0, 72);

		if (!secondShape)
			nextShape.Activate();

		//Creates a new random shape and places it at the bottom of the shape queue."""


		Shape shape = (Shape)shapeScene.Instance();
		var newShape = GD.Randi() % 7;

		AddChild(shape);
		shape.ChangeShape(newShape);

		if (newShape == 0)
        {
            shape.Position = spawnPos - new Vector2(8, 0);
        }
        else if(newShape == 4)
            shape.Position = spawnPos - new Vector2(8, 0);
        else
		{
            shape.Position = spawnPos;
		}
		nextShape = lastShape;
		lastShape = shape;

		//Tells the function to spawn another shape if this is the first spawned shape."""

		if (firstShape)
		{
			firstShape = false;
			Spawn();
		}
		else if(secondShape)
		{
			//Tells the function to spawn another shape if this is the second spawned shape."""
			secondShape = false;
			Spawn();
		}    

	}


}
