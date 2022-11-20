using Godot;
using System;

public class PlayerGun : RayCast2D {

    [Export] private float _reloadTime = 30f;
    private Line2D _line;

    private float _nextShot;
    private SkiPlayer _player;
    
    [Export] private PackedScene _shotScene;
    
    public override void _Ready() {
        _line = GetNode<Line2D>("Line2D");
        _line.ClearPoints();
        _line.AddPoint(Vector2.Zero);
        _line.AddPoint(Vector2.Zero);
        _player = GetNode<SkiPlayer>("..");
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
            if (Time.GetTicksMsec() > _nextShot) {
                AttemptFire();
            }
        }

        Enabled = Input.IsActionPressed("player_shoot");
        _line.Visible = Enabled;
    }

    private void AttemptFire() {
        Enemy enemy = GetCollider() as Enemy;

        if (enemy != null) {
            Fire(enemy);
        }

        _nextShot = Time.GetTicksMsec() + _reloadTime;
        SpawnShot();
    }

    private void Fire(Enemy target) {
        Console.WriteLine("hit!");
        target.Damage((int) Math.Ceiling(_player.GetVelocity()));
    }
    
    private void SpawnShot() {
        var shot = _shotScene.Instance();
        AddChild(shot);
    }
}
