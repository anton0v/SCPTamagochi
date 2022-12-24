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
        Tags.Add(new TagInfo(InfoTagList[Random.Range(0, InfoTagList.Count)]));
        Tags.Add(new TagBehavior(TagAngry));
        Tags.Add(new TagFood(FoodTagList[Random.Range(0, FoodTagList.Count)]));
        Tags.Add(new TagRoom(RoomTagList[Random.Range(0, RoomTagList.Count)]));
        SetAllTags();
        ShuffleHiddenTags();
        _infoRequiredScore = 3;
        sr.sprite = SpriteSamples[Random.Range(0, SpriteSamples.Length)];
        sr.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f) );
    }
}