
using Godot;

public class Enemy: Area2D, DamageableByPlayer {
    public int _health { get; private set; }

    public override void _Ready() {
        _health = 100;
    }

    public void Damage(int amount) {
        _health -= amount;
    }
}
