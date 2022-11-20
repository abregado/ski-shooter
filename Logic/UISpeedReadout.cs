using Godot;
using System;

public class UISpeedReadout : Label {
    [Export] private NodePath _playerNodePath;
    private VelocityHolder _target;

    [Export] private NodePath _highestSpeedUI;
    private Label _highestSpeed;

    private int _highest = 0;
    
    public override void _Ready() {
        _target = GetNode<VelocityHolder>(_playerNodePath);
        _highestSpeed = GetNode<Label>(_highestSpeedUI);
    }

    public override void _Process(float delta) {
        int speed = (int) Math.Floor(_target.GetVelocity());

        if (speed > _highest) {
            _highest = speed;
            _highestSpeed.Text = "Highest Velocity: " + _highest.ToString();
        }
        
        Text = "Velocity: " + speed.ToString();
    }
}
