using Godot;
using System;

public class SkiPlayer : RigidBody2D
{
    public override void _Ready()
    {

    }

    public override void _IntegrateForces(Physics2DDirectBodyState bodyState)
    {

        bool jetpack = Input.IsActionPressed("jetpack");
        bool ski = !jetpack;

        this.PhysicsMaterialOverride.Friction = ski ? 0 : 1;

        if (jetpack)
        {
            Vector2 currentVelocity = bodyState.LinearVelocity;

            float jetpackSpeed = 100;
            jetpackSpeed = Mathf.Max(jetpackSpeed, currentVelocity.Length());
            Vector2 jetpackDirection = new Vector2(1,-1).Normalized() * jetpackSpeed;

            float alpha = 0.05f;
            Vector2 newVelocity = jetpackDirection * alpha + currentVelocity * (1.0f-alpha);
            bodyState.LinearVelocity = newVelocity;
        }
    }
}
