using Godot;
using System;

public static class NumericExtensions
{
    public static float ToRadians(this float degrees)
    {
        return ((float)Math.PI / 180f) * degrees;
    }

    public static float ToDegrees(this float radians)
    {
        return radians * (180f / (float)Math.PI);
    }
}

public class SkiPlayer : KinematicBody2D, VelocityHolder
{
    [Export] float hugFloorStrength = 100f;
    [Export] float gravityStrength = 1f;
    [Export] float slidingSpeedGainMultiplier = 1f/5f;
    [Export] float jetpackUpForce = 0.1f;
    [Export] float jetpackMaxUpForce = 10f;
    [Export] float jetpackDecay = 0.01f;
    [Export] float minimumHorizontalVelocity = 1.0f;



    RayCast2D feetRaycast;
    float horizontalVelocity = 0;
    float verticalVelocity = 0;

    public override void _Ready()
    {
        feetRaycast = GetNode<RayCast2D>("FeetRaycast");
    }

    public bool jetpacking()
    {
        return Input.IsActionPressed("jetpack");
    }

    public bool onTheGround()
    {
        return !jetpacking() && feetRaycast.IsColliding();
    }

    public override void _PhysicsProcess(float delta)
    {
        if (onTheGround())
        {
            float angle = feetRaycast.GetCollisionNormal().Rotated(-90f.ToRadians()).Angle().ToDegrees();
            angle = Mathf.PosMod(angle, 360f);

            // angle is now the normal of the surface, clockwise, relative to Down:
            //    180            .
            // 90     270          .    = 135
            //     0                 .

            float magnitude = angle <= 180f ? -(angle / 180f) : (angle-108f) / 180f;
            // Console.WriteLine(magnitude);

            horizontalVelocity += Mathf.Sign(magnitude) * magnitude*magnitude * slidingSpeedGainMultiplier;
        }

        horizontalVelocity = Mathf.Max(horizontalVelocity, minimumHorizontalVelocity);

        if (jetpacking())
            verticalVelocity = Math.Min(verticalVelocity - jetpackUpForce, -jetpackMaxUpForce);

        verticalVelocity += jetpackDecay;

        Vector2 old = GlobalPosition;
        MoveAndCollide(new Vector2(horizontalVelocity, 0));

        MoveAndCollide(new Vector2(horizontalVelocity, onTheGround() ? hugFloorStrength : verticalVelocity + gravityStrength));

        float moved = GlobalPosition.x - old.x;
        if (horizontalVelocity - moved > 0.1f)
            horizontalVelocity = moved;
    }

    // public override void _IntegrateForces(Physics2DDirectBodyState bodyState)
    // {
    //     bool jetpack = Input.IsActionPressed("jetpack");
    //     bool ski = !jetpack;
    //
    //     this.PhysicsMaterialOverride.Friction = ski ? 0 : 1;
    //     this.GravityScale = 5;//jetpack ? 0 : 1;
    //     this.LinearDamp = 0;//jetpack ? 0 : 1;
    //
    //     if (jetpack)
    //     {
    //         Vector2 currentVelocity = bodyState.LinearVelocity;
    //
    //         float jetpackSpeed = 400;
    //         jetpackSpeed = Mathf.Max(jetpackSpeed, currentVelocity.Length());
    //         Vector2 jetpackDirection = new Vector2(1,-1).Normalized() * jetpackSpeed;
    //
    //         float alpha = 0.05f;
    //         Vector2 newVelocity = jetpackDirection * alpha + currentVelocity * (1.0f-alpha);
    //         bodyState.LinearVelocity = newVelocity;
    //     }
    //
    //     _velocity = bodyState.LinearVelocity.Length();
    // }

    public float GetVelocity() {
        return horizontalVelocity;
    }
}
