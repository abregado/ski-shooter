
using System;
using Godot;

public class Enemy: Area2D, DamageableByPlayer {
    public int _health { get; private set; }

    [Export] private PackedScene _explosionScene;
    [Export] private PackedScene _hitScene;
    
    public override void _Ready() {
        _health = 7;
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
        SkiPlayer player = GetNode<SkiPlayer>("../../player");
        player.kills++;
        QueueFree();
    }

    private void SpawnExplosion() {
        Node root = GetNode<Node>("..");
        var explo = _explosionScene.Instance();
        root.AddChild(explo);
    }

    private void SpawnHit() {
        var hit = _hitScene.Instance();
        AddChild(hit);
    }

    public void OnBodyEntered(PhysicsBody2D body) {
        Console.WriteLine("Hit");
    }
}
