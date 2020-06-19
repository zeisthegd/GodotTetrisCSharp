using Godot;
using System;

public class Block : CollisionShape2D
{
	Sprite blockSprite;
	string IBlockPath = "res://Art/Blocks/Blocks1.png";
	string LBlockPath = "res://Art/Blocks/Blocks2.png";
	string TBlockPath = "res://Art/Blocks/Blocks3.png";
	string SBlockPath = "res://Art/Blocks/Blocks4.png";
	string OBlockPath = "res://Art/Blocks/Blocks5.png";
	string JBlockPath = "res://Art/Blocks/Blocks6.png";
	string ZBlockPath = "res://Art/Blocks/Blocks7.png";

	public override void _Ready()
	{
		
		blockSprite = (Sprite)GetNode("BlockSprite");
	}

	public void ChangeSprite(uint n)
	{
		string texturePath = SetTexturePath(n);
		blockSprite.Texture = (Texture)ResourceLoader.Load(texturePath);
	}

	private string SetTexturePath(uint n)
	{
		switch(n)
		{
			case 0:
				return IBlockPath;
			case 1:
				return LBlockPath;
			case 2:
				return TBlockPath;
			case 3:
				return SBlockPath;
			case 4:
				return OBlockPath;
			case 5:
				return JBlockPath;
			case 6:
				return ZBlockPath;
			default:
				return OBlockPath;
		}
	}


}
