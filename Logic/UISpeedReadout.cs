using Godot;
using System;

public class UISpeedReadout : Label {
    [Export] private NodePath _playerNodePath;
    private VelocityHolder _target;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        _target = GetNode<VelocityHolder>(_playerNodePath);
    }

    public override void _Process(float delta) {
        Text = _target.GetVelocity().ToString();
    }
}
