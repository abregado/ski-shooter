
public class DeleteAfterPlay: PlayRandom {
    public override void _Ready() {
        base._Ready();
        PlaySound();
    }

    public override void _Process(float delta) {
        if (!Playing) {
            QueueFree();
        }
    }
}
