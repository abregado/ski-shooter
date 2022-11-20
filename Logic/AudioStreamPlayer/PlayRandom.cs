
using System;
using Godot;

public class PlayRandom: AudioStreamPlayer {
    private string _prefix = "res://Sound/";
    [Export] private string _middle = "sci-fi_weapon_plasma_pistol_";
    private string _suffix = ".wav";
    [Export] private AudioStream[] _audioResources;
    
    public void PlaySound(bool restart = false) {
        GD.Randomize();
        if (!Playing || restart) {
            Stream = _audioResources[GD.Randi() % _audioResources.Length];
            Play();
        }
    }
}
