using Godot;
using Database;
using System;
using System.Linq;
using System.Collections.Generic;

public class RankingScene : Node2D
{
	VBoxContainer rankFrames;
	List<PlayerDTO> sortedPlayerList;
	AudioStream rankingScene = (AudioStream)ResourceLoader.Load(@"res://Audio/Music/Home - Toby Fox.ogg");

	public override void _Ready()
	{
		rankFrames = (VBoxContainer)GetNode("RankFrames");
		sortedPlayerList = AutoLoad.PlayerBUS.PlayersList.OrderByDescending(o => o.HighScore).ToList();

		if (sortedPlayerList.Count > 0)
		{
			Add1stRankFrame();
			if (sortedPlayerList.Count > 1)
			{
				Add2ndRankFrame();
				AddOthersRankFrames();
			}
			AddCurrentPlayerRankFrame();
		}

		AutoLoad.PlayMusic(this, rankingScene);
	}

	private void Add1stRankFrame()
	{
		PackedScene frame = (PackedScene)ResourceLoader.Load("res://Scenes/1stRankFrame.tscn");
		
		RankFrames newFrame = (RankFrames)frame.Instance();
		rankFrames.AddChild(newFrame);
		newFrame.DisplayPlayerData("#1", sortedPlayerList[0].UserName, sortedPlayerList[0].HighScore.ToString());
	}
	private void Add2ndRankFrame()
	{
		PackedScene frame = (PackedScene)ResourceLoader.Load("res://Scenes/2ndRankFrame.tscn");
		RankFrames newFrame = (RankFrames)frame.Instance();
		rankFrames.AddChild(newFrame);
		newFrame.DisplayPlayerData("#2", sortedPlayerList[1].UserName, sortedPlayerList[1].HighScore.ToString());
	}
	private void AddOthersRankFrames()
	{
		PackedScene frame = (PackedScene)ResourceLoader.Load("res://Scenes/RankFrame.tscn");
	   
		for (int i = 2; i < 6 || i < sortedPlayerList.Count; i++)
		{
			RankFrames newFrame = (RankFrames)frame.Instance();
			rankFrames.AddChild(newFrame);
			newFrame.DisplayPlayerData($"#{i + 1}", sortedPlayerList[i].UserName,
				sortedPlayerList[i].HighScore.ToString());
		}

	}
	private void AddCurrentPlayerRankFrame()
	{
		PlayerDTO currentPlayer = AutoLoad.PlayerBUS.GetCurrentPlayer();
		PackedScene frame = (PackedScene)ResourceLoader.Load("res://Scenes/CurrentPlayerFrame.tscn");
		RankFrames newFrame = (RankFrames)frame.Instance();
		this.AddChild(newFrame);
		newFrame.RectGlobalPosition = new Vector2(64,340);
		newFrame.DisplayPlayerData($"#{sortedPlayerList.IndexOf(currentPlayer) + 1}", currentPlayer.UserName, currentPlayer.HighScore.ToString());
	}

	private void _on_Back_pressed()
	{
		AutoLoad.Global.GotoScene("res://Scenes/StartMenu.tscn");
	}


}


