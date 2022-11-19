using Godot;
using System;

public class CameraFollowPlayer : Camera2D {
    [Export]
    private NodePath _playerNodePath;
    [Export]
    public float xPercentOffset;

    private Node2D _playerNode;
    
    public override void _Ready() {
        _playerNode = GetNode<Node2D>(_playerNodePath);
    }
    
    public override void _Process(float delta) {
        Vector2 pos = Position;
        Vector2 screenSize = GetViewportRect().Size; 
        pos.x = _playerNode.Position.x + (screenSize.x * xPercentOffset);
        pos.y = screenSize.y / 2f;
        Position = pos;
    }
}