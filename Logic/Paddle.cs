using Godot;
using System;

public class Paddle : Area2D, VelocityHolder
{
	private const int MoveSpeed = 100;
	[Export] private float _acceleration = 100;
	private float _velocity;

	// All three of these change for each paddle.
	private int _ballDir;
	private string _up;
	private string _down;

	public override void _Ready()
	{
		string name = Name.ToLower();
		_up = name + "_move_up";
		_down = name + "_move_down";
		_ballDir = name == "left" ? 1 : -1;
		_velocity = 0;
	}

	public override void _Process(float delta)
	{
		// Move up and down based on input.
		float input = Input.GetActionStrength(_down) - Input.GetActionStrength(_up);
		Vector2 position = Position; // Required so that we can modify position.y.
		position += new Vector2(0, input * MoveSpeed * delta);
		position.y = Mathf.Clamp(position.y, 16, GetViewportRect().Size.y - 16);
		
		// Go Right faster and faster
		_velocity += _acceleration / 1000 * delta;
		position.x += _velocity;
		
		Position = position;


		if (Input.IsActionJustPressed("debug_hit")) {
			DamageVelocity(0.1f);
		}
	}
	
	public void DamageVelocity(float percentage) {
		_velocity *= percentage;
	}

	public float GetVelocity() {
		return _velocity;
	}
}
