using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class AnomalyBase : MonoBehaviour
{
    [SerializeField] protected Text Info;
    [SerializeField] protected Sprite[] SpriteSamples;
    protected SpriteRenderer sr;
    protected List<Tag> tags;
    protected delegate void GetAngry(AnomalyBase anomaly);
    protected GetAngry _getAngry;
    protected Controller _controller;

    protected void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        tags = new List<Tag>();
        _controller = GameObject.FindGameObjectWithTag("Controller").GetComponent<Controller>();
    }
    public virtual void CalculateContainment()
    {
        InfoUpdate();
    }

    public virtual void InfoUpdate()
    {
        Info.text = "Теги: ";
        for (int i = 0; i < tags.Count; i++)
        {
            if(!tags[i].Hidden) Info.text += "\n" + tags[i].GetDescription();
        }
    }

    protected virtual int ResearchChance()
    {
        return 100;
    }

    protected class Tag
    {
        protected int TagId;
        public bool Hidden { get; protected set; }
        public string Name { get; protected set; }
        public delegate void TagDelegate(AnomalyBase anomaly);
        public TagDelegate SetTag;
        public Tag(string name)
        {
            TagId = 0;
            Name = name;
            Hidden = true;
        }
        public string GetDescription()
        {
            return Name;
        }
        public bool TestCheck(Test test)
        {
            if (!Hidden) return false;
            for(int i = 0; i < test.TagIdList.Count; i++)
            {
                if (test.TagIdList[i] == TagId)
                {
                    Hidden = false;
                    return true;
                }
            }
            return false;
        }
        
    }

    protected void SetAllTags()
    {
        for(int i = 0; i < tags.Count; i++)
        {
            tags[i].SetTag(this);
        }
    }
}