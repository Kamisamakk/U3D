using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird
{
    public override void Skill()
    {
        base.Skill();
        Vector3 speed = rg.velocity;
        speed.x *= -1f;
        rg.velocity = speed;
    }
}
