using Godot;
using System;

public class UISpeedReadout : Label {
    [Export] private NodePath _playerNodePath;
    private VelocityHolder _target;
    
    public override void _Ready() {
        _target = GetNode<VelocityHolder>(_playerNodePath);
    }

    public override void _Process(float delta) {
        Text = _target.GetVelocity().ToString();
    }
}
