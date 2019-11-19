using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird
{
    public override void Skill()
    {
        base.Skill();
        rg.velocity *= 2f;
    }
}
