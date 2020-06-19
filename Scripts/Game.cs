using Godot;
using Godot.Collections;
using System;
using System.Collections;
using System.Collections.Generic;

public class Game : Node2D
{
	Spawner spawner;
	UI userInterface;


	private const int BOARD_HEIGHT = 26;
	private const int BOARD_WIDTH = 10;

	List<List<string>> board = new List<List<string>>();
	bool gameOver = false;

	

	public override void _Ready()
	{
		spawner = (Spawner)GetNode("Spawner");
		userInterface = (UI)GetNode("UI");
		AutoLoad.ShapeDropSpeed = AutoLoad.DEFAULT_SHAPE_DROP_SPEED_PROP;	
	}

	public void InitGameBoard()
	{
		for (int row = 0; row < BOARD_HEIGHT; row++)
		{
			board.Add(new List<string>() { });
			for (int col = 0; col < BOARD_WIDTH; col++)
			{
				board[row].Add("[]");
			}
		}
	}

	public void ShapeToBoard(Godot.Collections.Array blockPositions)
	{
		//Turns Vector2 positions that contain blocks into X on the game board.
		foreach(Vector2 pos in blockPositions)
		{
			board[(int)pos.y / AutoLoad.CellSize - 1][(int)pos.x / AutoLoad.CellSize - 7] = "[X]";
		}

		if (!gameOver)
		{
			spawner.Spawn();
			RowCheck();
		}		
	}

	private void RowCheck()
	{
		List<int> fullRows = new List<int>();
		for (int row = 0; row < BOARD_HEIGHT; row++)
		{
			if(!board[row].Contains("[]"))
			{
				fullRows.Add(row + 2);
				
				for (int col = 0; col < BOARD_WIDTH; col++)
				{
					board[row][col] = "[]";
				}
			}
		}

		//Tells the shapes that have dropped to delete any blocks within the full rows.
		if (fullRows.Count > 0)
		{
			Godot.Collections.Array droppedShapes = GetTree().GetNodesInGroup("DroppedShapes");
			foreach(Shape dShape in droppedShapes)
				dShape.DeleteRow(fullRows);
		}

		//Lowers unfilled rows that are higher than the deleted rows.
		for (int row = 24; row >= 0; row--)
		{
			
			if (row < IntArrayMaxValue(fullRows) - 1 && !fullRows.Contains(row + 1))
			{
				for (int col = 0; col < BOARD_WIDTH; col++)
				{
					if (board[row][col] == "[X]")
					{
						board[row][col] = "[]";

						///Lowers unfilled rows that are between 
						///the highest filled and lowest filled row.
						if (row == (IntArrayMaxValue(fullRows) - 2) || row == (IntArrayMaxValue(fullRows) - 3)
							&& !fullRows.Contains(row + 2))
							board[row + 1][col] = "[X]";
						else if (row == (IntArrayMaxValue(fullRows) - 3) && fullRows.Count > 1)
							board[row + 2][col] = "[X]";
						else
							board[row + fullRows.Count][col] = "[X]";
					}
				}
			}
		}
		//userInterface.SetScore(fullRows.Count);

	}


	private int IntArrayMaxValue(List<int> array)
	{
		if(array.Count!=0)
		{
			int max = (int)array[0];
			for (int i = 1; i < array.Count; i++)
			{
				if (max <= (int)array[i])
					max = (int)array[i];
			}
			return max;
		}
		return 0;
	}

	public bool GameOver { get => gameOver; set => gameOver = value; }
	public List<List<string>> Board { get { return board; }}
}
