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
    [Export] float uphillSpeedChangeMultiplier = 2f;
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


    public override void _PhysicsProcess(float delta)
    {
        if (feetRaycast.IsColliding())
        {
            float angle = feetRaycast.GetCollisionNormal().Rotated(-90f.ToRadians()).Angle().ToDegrees();
            angle = Mathf.PosMod(angle, 360f);

            // angle is now the normal of the surface, clockwise, relative to Down:
            //    180            .
            // 90     270          .    = 135
            //     0                 .

            float magnitude = angle <= 180f ? -(angle / 180f) : (angle-108f) / 180f;

            if (magnitude < 0)
                magnitude *= uphillSpeedChangeMultiplier;

            if (!jetpacking() || magnitude < 0)
                horizontalVelocity += magnitude * slidingSpeedGainMultiplier;
        }

        horizontalVelocity = Mathf.Max(horizontalVelocity, minimumHorizontalVelocity);

        if (jetpacking())
            verticalVelocity = Math.Min(verticalVelocity - jetpackUpForce, -jetpackMaxUpForce);

        verticalVelocity += jetpackDecay;


        Vector2 old = GlobalPosition;

        int iterations = 0;
        while (GlobalPosition.x - old.x < horizontalVelocity && iterations < 20)
        {
            Vector2 move = new Vector2();
            if (feetRaycast.GetCollider() != null)
                move = feetRaycast.GetCollisionNormal().Rotated(90f.ToRadians()).Normalized();
            else
                move = new Vector2(1, 0);

            move *= 0.5f;
            MoveAndCollide(move);
            iterations++;
        }

        MoveAndCollide(new Vector2(0, (feetRaycast.IsColliding() && !jetpacking()) ? hugFloorStrength : verticalVelocity + gravityStrength));
    }

    public float GetVelocity() {
        return horizontalVelocity;
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
