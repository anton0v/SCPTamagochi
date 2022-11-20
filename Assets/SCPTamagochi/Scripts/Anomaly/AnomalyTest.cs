using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class AnomalyTest : AnomalyBehavior
{
    private new void Start()
    {
        base.Start();
        tags.Add(TagEldrich);
        tags.Add(TagAngry);
    }
}