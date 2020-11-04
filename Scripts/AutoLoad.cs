using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using Database;

public class AutoLoad : Node
{
	/*Autoload script containing variables that are used across multiple different scripts.
	Responsible for saving and loading save files and user preferences.
	Also adds and controls the music audio player.*/

	static PlayerBUS playerBUS;
	static Global global;
	static FloatingTextSpawner floatingTextSpawner;

	string saveFolderPath;
	static string savePathForCSharp = @"Saves\players.txt";
	static string savePath;
	static string configPath;
	

	public const int WINDOW_HEIGHT = 416, WINDOW_WIDTH = 384;



	const int CELL_SIZE = 16;
	const float DEFAULT_SHAPE_DROP_SPEED = 0.4f;

   


	static float shapeDropSpeed = DEFAULT_SHAPE_DROP_SPEED;

	//Bien dat trong file config
	static int musicVolume = 0;
	static bool fullScreen = true;

	public override void _Ready()
	{       
		global = (Global)GetNode("/root/Global");
		floatingTextSpawner = (FloatingTextSpawner)GetNode("/root/FloatingTextSpawner");

		InitSaveFolderAndFile();

		InitPlayerBUS();

		InitConfig();
		LoadConfig();            
	}
	public static void PlayMusic(Node currentNode,AudioStream music)
	{
		AudioStreamPlayer musicPlayer = new AudioStreamPlayer();
		currentNode.AddChild(musicPlayer);

		musicPlayer.Stream = music;
		musicPlayer.VolumeDb = musicVolume;
		musicPlayer.Play();

	}
	

	void ChangeMusicVolume(int value)
	{
		musicVolume = value;
		if (musicVolume == -40)
			musicVolume = -1000;
	}



	/// <summary>
	/// Create Saves folder and initiate a text file to store players' data
	/// </summary>
	void InitSaveFolderAndFile()
	{
		var directory = new Directory();
		saveFolderPath = OS.GetExecutablePath().GetBaseDir().PlusFile("Saves");
		//saveFolderPath = directory.GetCurrentDir() + "Saves";
		directory.MakeDir(saveFolderPath);

		savePath = saveFolderPath + "/players.txt";
		
		var file = new File();
		if(!file.FileExists(savePath))
		{
			file.Open(savePath, File.ModeFlags.WriteRead);
			file.Close();
		}
		
	}
	private void InitConfig()
	{
		var file = new File();
		configPath = saveFolderPath + "/config.ini";
		if (!file.FileExists(configPath))
		{
			file.Open(configPath, File.ModeFlags.WriteRead);  
			file.Close();
			SaveConfig();
		}
	}
	public static void SaveConfig()
	{
		var config = new ConfigFile();
		var file = new File();
		config.SetValue("audio", "music_volume", musicVolume);
		config.SetValue("display", "fullscreen", fullScreen);
		if (file.FileExists("res://Saves/config.ini"))
		{
			GD.Print("exist");
			var err = config.Save(configPath);
			if (err != Error.Ok)
				GD.Print("Loi trong qua trinh save!");
		}
		else GD.Print("config file not exist");

		
	}
	public static void LoadConfig()
	{
		var config = new ConfigFile();
		var err = config.Load(configPath);

		if (err != Error.Ok)
		{
			musicVolume = -10;
			fullScreen = false;
			GD.Print("Loi trong qua trinh load cai dat!");
			return;
		}

		musicVolume = (int)config.GetValue("audio", "music_volume");
		fullScreen = (bool)config.GetValue("display", "fullscreen");
		OS.WindowFullscreen = fullScreen;
	}

	public static void InitPlayerBUS()
	{
		playerBUS = new PlayerBUS();
	}

	#region Property

	public static int CellSize
	{
		get { return CELL_SIZE; }
	}

	public static float ShapeDropSpeed
	{
		get { return shapeDropSpeed; }
		set { shapeDropSpeed = value; }
	}

	public static float DEFAULT_SHAPE_DROP_SPEED_PROP
	{
		get { return DEFAULT_SHAPE_DROP_SPEED; }
	}

	public static Global Global { get => global; set => global = value; }
	public static FloatingTextSpawner FloatingTextSpawner { get => floatingTextSpawner; set => floatingTextSpawner = value; }
	public static string SavePath { get => savePath;}
	public static string SavePathForCSharp { get => savePathForCSharp;}
	internal static PlayerBUS PlayerBUS { get => playerBUS;}
	public static int MusicVolume { get => musicVolume; set => musicVolume = value; }
	public static bool FullScreen { get => fullScreen; set => fullScreen = value; }



	#endregion


}
