using Godot;

public class UIEnemyHealth: Label {
    [Export] private NodePath _playerNodePath;
    private Enemy _target;
    
    public override void _Ready() {
        _target = GetNode<Enemy>(_playerNodePath);
    }

    public override void _Process(float delta) {
        Text = _target._health.ToString();
    }
    
}
