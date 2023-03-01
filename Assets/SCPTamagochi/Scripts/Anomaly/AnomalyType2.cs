using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyType2 : AnomalyBehavior
{
    private new void Start()
    {
        base.Start();
        Tags.Add(new TagBehavior(TagCalm));
        SetAllTags();
    }
}
