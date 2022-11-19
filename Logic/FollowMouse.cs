using Godot;
using System;

public class FollowMouse : Sprite {
    [Export] private bool _local;
    
    public override void _Process(float delta) {
        if (_local) {
            Position = GetLocalMousePosition();
        }
        else {
            Position = GetGlobalMousePosition();    
        }
        
    }
}
