using Godot;
using System;

public class MusicPlayer : AudioStreamPlayer2D
{
	AudioStream[] musicFiles = new AudioStream[] {(AudioStream)ResourceLoader.Load(@"res://Audio/Music/ASGORE - Toby Fox.ogg"),
	                                            (AudioStream)ResourceLoader.Load(@"res://Audio/Music/Battle Against A True Hero - Toby Fox.ogg"),
	                                            (AudioStream)ResourceLoader.Load(@"res://Audio/Music/Death By Glamour - Toby Fox.ogg"),
	                                            (AudioStream)ResourceLoader.Load(@"res://Audio/Music/Hopes And Dreams - Toby Fox.ogg"),
	                                            (AudioStream)ResourceLoader.Load(@"res://Audio/Music/Spider Dance - Toby Fox.ogg")};

	public override void _Ready()
	{
		int n = (int)GD.RandRange(0, musicFiles.Length);
		Stream = musicFiles[n];
		VolumeDb = AutoLoad.MusicVolume;
		Play();
	}


	private void _on_MusicPlayer_finished()
	{
		int n = (int)GD.RandRange(0, musicFiles.Length);
		Stream = musicFiles[n];
		VolumeDb = AutoLoad.MusicVolume;
		Play();
	}
}




