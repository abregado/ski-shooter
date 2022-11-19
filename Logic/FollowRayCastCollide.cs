using Godot;
using System;

public class FollowRayCastCollide : Sprite {
    private RayCast2D _caster;
    
    public override void _Ready() {
        _caster = GetNode<RayCast2D>("..");
    }

    public override void _Process(float delta) {
        GlobalPosition = _caster.GetCollisionPoint();
    }
}
