using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class AnomalyTest : AnomalyFood
{
    private new void Start()
    {
        base.Start();
        tags.Add(new TagInfo(TagEldrich));
        tags.Add(new TagBehavior(TagAngry));
        tags.Add(new TagFood(FoodTagList[Random.Range(0, FoodTagList.Count)]));
        tags.Add(new TagRoom(RoomTagList[Random.Range(0, RoomTagList.Count)]));
        SetAllTags();
    }
}