using System.Collections;
using UnityEditor.Build.Content;
using UnityEngine;

public class AnomalyTest : AnomalyBehavior
{
    private void Start()
    {
        tags.Add(TagEldrich);
        tags.Add(TagAngry);
    }
}