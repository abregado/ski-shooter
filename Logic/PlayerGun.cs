using Godot;
using System;

public class PlayerGun : RayCast2D {

    [Export] private float _reloadTime = 30f;
    private Line2D _line;

    private float _nextShot;
    private float _nextLineClear;
    private SkiPlayer _player;

    private int[] _damageValues;
    
    [Export] private PackedScene _shotScene;
    
    public override void _Ready() {
        _line = GetNode<Line2D>("Line2D");
        _line.ClearPoints();
        _line.AddPoint(Vector2.Zero);
        _line.AddPoint(Vector2.Zero);
        _player = GetNode<SkiPlayer>("..");

        _damageValues = new[] {0, 1, 1, 2, 3, 3, 7, 7, 7, 9, 9};
    }

    public override void _PhysicsProcess(float delta) {
        Vector2 direction = GetGlobalMousePosition() - GlobalPosition;
        CastTo = direction.Normalized() * 5000f;
    }

    public override void _Process(float delta) {
        Vector2 linePoint = GetCollisionPoint();

        if (Time.GetTicksMsec() > _nextLineClear) {
            _line.SetPointPosition(0,Position);
            _line.SetPointPosition(1,Position);
            _line.Visible = false;
        }
        
        
        if (Input.IsActionPressed("player_shoot") && !_player.jetpacking()) {
            if (!IsColliding()) {
                return;
            }
            
            if (Time.GetTicksMsec() > _nextShot) {
                
                _line.SetPointPosition(0,Position);
                _line.SetPointPosition(1,linePoint - GlobalPosition);
                _nextLineClear = Time.GetTicksMsec() + 100f;
                _line.Visible = true;
            
                
                AttemptFire();
            }
        }
        //Enabled = Input.IsActionPressed("player_shoot");
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
        int speed = (int) Math.Ceiling(_player.GetVelocity());
        speed = Math.Max(speed, 0);
        speed = Math.Min(speed, _damageValues.Length - 1);
        target.Damage(_damageValues[speed]);
    }
    
    private void SpawnShot() {
        var shot = _shotScene.Instance();
        AddChild(shot);
    }
}
