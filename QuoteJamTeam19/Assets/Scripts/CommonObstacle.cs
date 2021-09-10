using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType {
    car,
    common,
    skateboard
}
public class CommonObstacle : MonoBehaviour
{
    public ObstacleType obstacleType;
    [Range(1, 3)]
    public float slowDuration = 1f;
    [Range(0, 1)]
    public float slowEffect = 0.5f;
    [Range(0,5)]
    public float dignityDamage;

}
