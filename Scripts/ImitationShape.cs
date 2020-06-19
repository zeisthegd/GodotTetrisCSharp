using Godot;
using System;
using System.Collections.Generic;

public class ImitationShape : KinematicBody2D
{
	Shape realShape = new Shape();

	List<Block> imitatedBlocks = new List<Block>();
	List<Block> realBlocks = new List<Block>();



	public override void _Ready()
	{
		realShape = (Shape)GetParent();

		GetImitatedBlocks();
		GetRealBlocks();
	}


	public bool ValidImitation(int newRotation)
	{
		for (int i = 0; i < 4; i++)
		{
			imitatedBlocks[i].GlobalPosition = realBlocks[i].GlobalPosition;
		}

		RotationDegrees = newRotation * 90;

		bool valid = realShape.IsValidPosition(new Vector2(0, 0), imitatedBlocks);

		if (!valid)
			RotationDegrees -= 90;

		return valid;

	}

	#region InitMethods
	private void GetImitatedBlocks()
	{
		for (int i = 0; i < 4; i++)
		{
			Block bl = (Block)GetNode($"Block{i}");

			imitatedBlocks.Add(bl);
		}
	}

	private void GetRealBlocks()
	{
		for (int i = 0; i < 4; i++)
		{
			Block bl = (Block)GetParent().GetNode($"Block{i}");

			realBlocks.Add(bl);
		}
	}


	#endregion



}

