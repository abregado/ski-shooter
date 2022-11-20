
using System;
using Godot;

public class EnemyController: Node2D {
    [Export] private PackedScene _enemyScene;
    
    [Export] private float _levelLength = 300000;
    [Export] private float _enemyGapMin = 150;
    [Export] private float _enemyGapMax = 1000;
    
    [Export] private float _heightMax = 150;
    [Export] private float _heightMin = 10;

    public override void _Ready() {
        float currentX = 100;
        float rangeX = _enemyGapMax - _enemyGapMin;
        float rangeY = _heightMax - _heightMin;

        while (currentX < _levelLength) {
            float step = _enemyGapMin + GD.Randi() % rangeX;
            currentX += step;

            float y = _heightMin + GD.Randi() % rangeY;

            Node2D newEnemy = _enemyScene.Instance() as Node2D;
            newEnemy.GlobalPosition = new Vector2(currentX, y);
            Console.WriteLine("Spawned enemy at " + currentX + "," + y);
            AddChild(newEnemy);
        }
    }
}
