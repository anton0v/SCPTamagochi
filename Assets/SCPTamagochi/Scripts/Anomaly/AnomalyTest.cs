using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class AnomalyTest : AnomalyFood
{
    private new void Start()
    {
        base.Start();
        tags.Add(TagEldrich);
        tags[0].Hidden = false;
        tags.Add(TagAngry);
        tags.Add(FoodTagList[Random.Range(0, FoodTagList.Count)]);
        SetAllTags();
    }
}