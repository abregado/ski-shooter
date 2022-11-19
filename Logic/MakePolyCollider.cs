using Godot;

public class MakePolyCollider : Polygon2D
{
    public override void _Ready()
    {
        CollisionPolygon2D collisionPoly = new CollisionPolygon2D();
        collisionPoly.Polygon = this.Polygon;
        GetNode("StaticBody2D").AddChild(collisionPoly);
    }
}
