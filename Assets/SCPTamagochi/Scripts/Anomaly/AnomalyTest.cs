using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;

public class AnomalyTest : AnomalyFood
{
    public enum DIFFICULTY { EASY, NORMAL, HARD}
    [SerializeField] DIFFICULTY Difficulty;
    private new void Start()
    {
        base.Start();
        Tags.Add(new TagInfo(InfoTagList[Random.Range(0, InfoTagList.Count)]));
        Tags.Add(new TagFood(FoodTagList[Random.Range(0, FoodTagList.Count)]));
        Tags.Add(new TagRoom(RoomTagList[Random.Range(0, RoomTagList.Count)]));
        switch (Difficulty)
        {
            case DIFFICULTY.EASY:
                _infoRequiredScore = 1;
                Tags.Add(new TagBehavior(TagCalm));
                break;
            case DIFFICULTY.NORMAL:
                _infoRequiredScore = 3;
                Tags.Add(new TagBehavior(BehaviorTagList[Random.Range(0, BehaviorTagList.Count)]));
                break;
            case DIFFICULTY.HARD:
                Tags.Add(new TagBehavior(TagAngry));
                _infoRequiredScore = 5;
                break;
        }
        SetAllTags();

        sr.sprite = SpriteSamples[Random.Range(0, SpriteSamples.Length)];
        sr.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f) );
    }
}