using Godot;
using System;

public class SkiPlayer : RigidBody2D, VelocityHolder {
    private float _velocity;
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

        _velocity = bodyState.LinearVelocity.Length();
    }

    public float GetVelocity() {
        return _velocity;
    }

    public void DamageVelocity(float percentage) {
        //does nothing for now
    }
    
    public void OnAreaEntered(Area2D area)
    {
        if (area is Enemy enemy)
        {
            enemy.Damage(100);
            DamageVelocity(0.1f);
        }
    }
    
    
}
