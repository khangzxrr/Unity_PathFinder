using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathInfo
{
    public PathInfo(float angle, bool rol)
    {
        this.angle = angle;
        this.rol = rol;
        this.distance = distance;
    }
    public float distance { get; set; }
    public float angle { get; set; } //angle to turn
    public bool rol { get; set; } //right or left true = right, false = left
}
