
using Godot;

public class Enemy: Area2D, DamageableByPlayer {
    public int _health { get; private set; }

    [Export] private PackedScene _explosionScene;
    [Export] private PackedScene _hitScene;
    
    private PlayRandom _audioDirector;

    public override void _Ready() {
        _health = 10;
        _audioDirector = GetNode<PlayRandom>("AudioStreamPlayer");
    }

    public void Damage(int amount) {
        _health -= amount;
        SpawnHit();
        if (_health <= 0) {
            Kill();    
        }
    }

    private void Kill() {
        SpawnExplosion();
        QueueFree();
    }

    private void SpawnExplosion() {
        Node root = GetNode<Node>("./Node2D");
        var explo = _explosionScene.Instance();
        root.AddChild(explo);
    }

    private void SpawnHit() {
        Node root = GetNode<Node>("./");
        var hit = _hitScene.Instance();
        root.AddChild(hit);
    }
}
