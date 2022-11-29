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
        tags.Add(TagAngry);
        tags.Add(FoodTagList[Random.Range(0, FoodTagList.Count)]);
        tags.Add(RoomTagList[Random.Range(0, RoomTagList.Count)]);
        SetAllTags();
    }
}