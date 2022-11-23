﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class AnomalyBase : MonoBehaviour
{

    [SerializeField] protected Text Info;
    protected SpriteRenderer sr;
    protected List<Tag> tags;
    protected delegate void GetAngry();
    protected GetAngry _getAngry;

    protected void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        tags = new List<Tag>();
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
    protected class Tag
    {
        public bool Hidden { get; set; }
        public Tag(string name)
        {
            Name = name;
            Hidden = true;
        }
        public string GetDescription()
        {
            return Name;
        }
        public string Name { get; protected set; }
        public delegate void TagDelegate(AnomalyBase anomaly);
        public TagDelegate SetTag;
    }

    protected void SetAllTags()
    {
        for(int i = 0; i < tags.Count; i++)
        {
            tags[i].SetTag(this);
        }
    }
}