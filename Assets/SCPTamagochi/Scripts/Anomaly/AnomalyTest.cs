using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;

public class AnomalyTest : AnomalyFood
{
    private new void Start()
    {
        base.Start();
        Tags.Add(new TagInfo(TagEldrich));
        Tags.Add(new TagBehavior(TagAngry));
        Tags.Add(new TagFood(FoodTagList[Random.Range(0, FoodTagList.Count)]));
        Tags.Add(new TagRoom(RoomTagList[Random.Range(0, RoomTagList.Count)]));
        SetAllTags();
        _infoRequiredScore = 3;
        sr.sprite = SpriteSamples[Random.Range(0, SpriteSamples.Length)];
        //sr.color = new Color((float)Random.Range(0, 255), (float)Random.Range(0, 255), (float)Random.Range(0, 255) );
    }
}