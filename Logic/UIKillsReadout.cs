using Godot;
using System;

public class UIKillsReadout : Label {
    [Export] private NodePath _playerNodePath;
    private SkiPlayer _target;
    
    public override void _Ready() {
        _target = GetNode<SkiPlayer>(_playerNodePath);
    }

    public override void _Process(float delta) {
        Text = "Kills: " + _target.GetKills().ToString();
    }
}
