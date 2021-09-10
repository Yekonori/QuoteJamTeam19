using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonObstacle : MonoBehaviour
{
    [Range(1, 3)]
    public float slowDuration = 1f;
    [Range(0, 1)]
    public float slowEffect = 0.5f;
    [Range(0,5)]
    public float dignityDamage;

}
