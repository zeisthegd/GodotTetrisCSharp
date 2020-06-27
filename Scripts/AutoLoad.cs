using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public class AutoLoad : Node
{
    /*Autoload script containing variables that are used across multiple different scripts.
    Responsible for saving and loading save files and user preferences.
    Also adds and controls the music audio player.*/

    string configPath;
    string savePath;

    public const int WINDOW_HEIGHT = 416, WINDOW_WIDTH = 384;



    const int CELL_SIZE = 16;
    const float DEFAULT_SHAPE_DROP_SPEED = 1f;

    AudioStreamPlayer musicPlayer = new AudioStreamPlayer();
    static float shapeDropSpeed = DEFAULT_SHAPE_DROP_SPEED;

    //Bien dat trong file config
    int musicVolume;
    int sfxVolume;
    bool fullScreen;
    bool fastMode;

    Dictionary<int, int> highScore;

    public override void _Ready()
    {
        
    }

    void ChangeMusicVolume(int value)
    {
        musicVolume = value;
        if (musicVolume == -40)
            musicVolume = -1000;
        musicPlayer.VolumeDb = musicVolume;
    }

    void ChangeSFXVolume(int value)
    {
        sfxVolume = value;
        if (musicVolume == -50)
            musicVolume = -1000;
    }

    void SaveConfig()
    {
        var config = new ConfigFile();

        config.SetValue("audio", "music_volume", musicVolume);
        config.SetValue("audio", "sfx_volume", sfxVolume);
        config.SetValue("display", "fullscreen", fullScreen);
        config.SetValue("game_mode", "fast_mode", fastMode);

        var err = config.Save(configPath);

        if (err != Error.Ok)
            GD.Print("Loi trong qua trinh save!");
    }

    void LoadConfig()
    {
        var config = new ConfigFile();
        var err = config.Load(configPath);

        if (err != Error.Ok)
        {
            musicVolume = -10;
            sfxVolume = -20;
            fullScreen = false;
            GD.Print("Loi trong qua trinh load cai dat!");
            return;
        }

        musicVolume = (int)config.GetValue("audio", "music_volume");
        sfxVolume = (int)config.GetValue("audio", "sfx_volume");
        fullScreen = (bool)config.GetValue("display", "fullscreen");
        fastMode = (bool)config.GetValue("game_mode", "fast_mode");
    }

    void SaveData(Dictionary<int, int> newScore)
    {
        highScore = newScore;

        var saveFile = new File();
        var err = saveFile.Open(savePath, File.ModeFlags.Write);

        if(err == Error.Ok)
        {
            GD.Print("Loi trong qua trinh mo savefile");
            return;
        }
        saveFile.StoreVar(highScore);
        saveFile.Close();
    }

    void LoadData()
    {
        var saveFile = new File();
        var err = saveFile.Open(savePath, File.ModeFlags.Read);

        if(err == Error.Ok)
        {
            highScore.Clear();
            highScore.Add(1, 0);//normal mode
            highScore.Add(2, 0);//fast mode

            GD.Print("Loi trong qua trinh mo savefile.Load du lieu mac dinh!");
            return;
        }

        highScore = (Dictionary<int,int>)saveFile.GetVar();
        saveFile.Close();
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



    #endregion


}
