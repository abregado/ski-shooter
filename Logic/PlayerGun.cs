using Godot;
using System;

public class PlayerGun : RayCast2D {
    private Line2D _line;
    
    public override void _Ready() {
        _line = GetNode<Line2D>("Line2D");
        _line.ClearPoints();
        _line.AddPoint(Vector2.Zero);
        _line.AddPoint(Vector2.Zero);
    }

    public override void _PhysicsProcess(float delta) {
        Vector2 direction = GetGlobalMousePosition() - GlobalPosition;
        CastTo = direction.Normalized() * 5000f;
    }

    public override void _Process(float delta) {
        Vector2 linePoint = GetCollisionPoint();

        if (linePoint != Vector2.Zero) {
            _line.SetPointPosition(0,Position);
            _line.SetPointPosition(1,linePoint - GlobalPosition);
        }
        else {
            _line.SetPointPosition(0,Position);
            _line.SetPointPosition(1,Position);
        }
        
        if (Input.IsActionPressed("player_shoot")) {
            Enemy enemy = GetCollider() as Enemy;

            if (enemy != null) {
                Console.WriteLine("hit!");
                enemy.Damage(1);
            }
        }

        Enabled = Input.IsActionPressed("player_shoot");
        _line.Visible = Enabled;
    }
}
