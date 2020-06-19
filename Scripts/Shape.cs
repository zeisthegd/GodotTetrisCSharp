using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class Shape : KinematicBody2D
{

	AudioStreamPlayer2D audioPlayer;
	Control userInterface;
	Game game;
	ImitationShape imitation;
	Tween invisibleTween;
	Timer dropTimer;

	AudioStreamSample shapeDropSound, lineBreakSound, gameOverSound;
	[Export]
	uint shape;

	bool active = false;
	int rotations = 0;

	List<Block> blocks = new List<Block>();
	Godot.Collections.Array blockPositions = new Godot.Collections.Array();

	Vector2[,] SHAPES = new Vector2[,]
	{
		{new Vector2(1, -1), new Vector2(1, 0), new Vector2(1, 1), new Vector2(1, 2) },//I
		{new Vector2(-1, 1),new Vector2(-1, 0),new Vector2(0, 0),new Vector2(1, 0) },//L
		{new Vector2(0, 1),new Vector2(1, 0), new Vector2(0, 0), new Vector2(-1, 0) },//T
		{new Vector2(-1, 1),new Vector2(0, 1),new Vector2(0, 0),new Vector2(1, 0)},//S
		{new Vector2(0, 0),new Vector2(1, 0),new Vector2(1, 1),new Vector2(0, 1)},//O
		{new Vector2(1, 1),new Vector2(1, 0),new Vector2(0, 0),new Vector2(-1, 0)},//J
		{new Vector2(1, 1), new Vector2(0, 1), new Vector2(0, 0),new Vector2(-1, 0) }//Z
	};

	public uint CurrentShape { get => shape; set => shape = value; }

	public override void _Ready()
	{
		GetBlocks();

		imitation = (ImitationShape)GetNode("ImitationShape");

		dropTimer = (Timer)GetNode("DropTimer");

		dropTimer.WaitTime = AutoLoad.ShapeDropSpeed;

		game = (Game)GetParent().GetParent();

		//shapeDropSound = (AudioStreamSample)ResourceLoader.Load("");
		//lineBreakSound = (AudioStreamSample)ResourceLoader.Load("");
		//gameOverSound = (AudioStreamSample)ResourceLoader.Load("");

		//chua add het tat cac cac field


	}

	public override void _PhysicsProcess(float delta)
	{
		Vector2 tempPosition = this.Position;
		if (active)
		{
			if (Input.IsActionJustPressed("ui_down"))
			{
				dropTimer.WaitTime = 0.04f;
				dropTimer.Start();
			}
			else if (Input.IsActionJustReleased("ui_down"))
				dropTimer.WaitTime = AutoLoad.ShapeDropSpeed;
			if (Input.IsActionJustReleased("ui_left"))
			{
				if (IsValidPosition(Vector2.Left, blocks))
					tempPosition.x -= AutoLoad.CellSize;
			}
			if (Input.IsActionJustReleased("ui_right"))
			{
				if (IsValidPosition(Vector2.Right, blocks))
					tempPosition.x += AutoLoad.CellSize;
			}
			if (Input.IsActionJustReleased("ui_up"))
			{
				ChangeRotation();
			}
			this.Position = tempPosition;
		}
	}

	private void GetBlocks()
	{
		for (int i = 0; i < 4; i++)
		{
			blocks.Add((Block)GetNode($"Block{i}"));
		}
	}


	public void Activate()
	{
		//Moves the shape from the next shape list to the spawn position, 
		//then runs a quick delay that 
		//gives the game board time to be created at the start of the game, 
		//preventing a fatal error
		//caused by calling the valid_position function too early.

		Position = new Vector2(200, 24);

		//yield return (GetTree().CreateTimer(0.001F), "Timeout");
		//await ToSignal(GetTree().CreateTimer(0.001F), "Timeout");

		//Checks if the spawn position is already occupied by another shape, 
		//sets game_over as true if 
		//it is and moves the shape upwards until it 
		//no longer overlaps any other shapes.

		if (!IsValidPosition(Vector2.Zero, blocks))
		{
			Position = new Vector2(200, 0);
			game.GameOver = true;

			if (!IsValidPosition(Vector2.Zero, blocks))
				Position = new Vector2(200, 24);
		}


		if (game.GameOver)
		{
			//userInterface.GameOverFunction();
			audioPlayer.Stream = gameOverSound;
			audioPlayer.Play();
		}
		else
		{
			active = true;
			// userInterface.ActiveShape = this;
			dropTimer.Start();
		}
	}


	public bool IsValidPosition(Vector2 direction, List<Block> blocks)
	{
		foreach (Block block in blocks)
		{
			try
			{
				Vector2 nextPos = block.GlobalPosition + direction * AutoLoad.CellSize;
				if (Math.Round(nextPos.x) < 112 || Math.Round(nextPos.x) > 272 || Mathf.Round(nextPos.y) > 416
					|| game.Board[((int)Math.Round(nextPos.y) / AutoLoad.CellSize) - 1][((int)Math.Round(nextPos.x) / AutoLoad.CellSize) - 7] != "[]")
				{
					return false;
				}
			}
			catch
			{
				GD.Print(block.GlobalPosition + direction * AutoLoad.CellSize);
			}

		}
		return true;
	}

	private void ChangeRotation()
	{
		if (active && shape != 4)
		{
			rotations += 1;
			if (rotations == 4)
				rotations = 0;
			if (imitation.ValidImitation(rotations))
			{
				foreach (Block block in blocks)
					block.RotationDegrees = -90 * rotations;
				this.RotationDegrees = 90 * rotations;
			}
			else
			{
				if (Rotation == 0)
					rotations = 3;
				else
					rotations -= 1;
			}
		}
	}


	public void ChangeShape(uint newShape)
	{
		//Change the shape and the sprite of the shape block
		shape = newShape;
		for (int i = 0; i < blocks.Count; i++)
		{
			blocks[i].ChangeSprite(shape);
			blocks[i].Position = SHAPES[shape, i] * AutoLoad.CellSize;
		}
	}

	public void DeleteRow(List<int> rows)
	{
		//Delete blocks from fullRows
		foreach (int row in rows)
		{
			var rowYPos = row * AutoLoad.CellSize;
			for (int block = blocks.Count - 1; block <= -1; block--)
			{
				if (Math.Round(blocks[block].GlobalPosition.y) == rowYPos)
				{
					blocks[block].Free();
					blocks.RemoveAt(block);
				}
			}
		}

		//audioPlayer.Stream = lineBreakSound;
		//audioPlayer.Play();

		//Delete shapes that dont contain any block
		if (blocks.Count <= 0)
		{
			QueueFree();
			return;
		}

		//Lower the remaining dropped shapes that are above removed rows
		foreach (Block block in blocks)
		{
			int blockY = (int)block.GlobalPosition.y / AutoLoad.CellSize;
			if (blockY < IntListMaxValue(rows))
			{
				Vector2 tempGloPos = block.GlobalPosition;
				if (blockY == IntListMaxValue(rows) - 1 || blockY == IntListMaxValue(rows) - 2 && !rows.Contains(IntListMaxValue(rows) - 1))
				{
					tempGloPos.y += AutoLoad.CellSize;
				}
				else if (blockY == IntListMaxValue(rows) - 2 && rows.Count > 1)
					tempGloPos.y += AutoLoad.CellSize * 2;
				else
					tempGloPos.y += AutoLoad.CellSize * rows.Count;

				block.GlobalPosition = tempGloPos;
			}

		}
	}
	private void _on_DropTimer_timeout()
	{
		//block drop down
		if (active)
			if (IsValidPosition(Vector2.Down, blocks))
				Position += new Vector2(0, AutoLoad.CellSize);
			else
			{
				active = false;
				dropTimer.Stop();
				AddToGroup("DroppedShapes");

				foreach (Block block in blocks)
				{
					blockPositions.Add(block.GlobalPosition);
				}
				game.ShapeToBoard(blockPositions);

				//play the block dropped sound: not added
			}
	}
	private int IntListMaxValue(List<int> array)
	{
		if (array.Count <= 0)
			return 0;
		else
		{
			int max = (int)array[0];
			for (int i = 1; i < array.Count; i++)
			{
				if (max <= (int)array[i])
					max = (int)array[i];
			}
			return max;
		}
	}

	public void PrintName(string name)
	{
		GD.Print(name);
	}



}



