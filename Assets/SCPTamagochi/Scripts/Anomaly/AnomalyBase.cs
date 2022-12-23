using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class AnomalyBase : MonoBehaviour
{
    [SerializeField] protected Sprite[] SpriteSamples;
    protected Text Info;
    [SerializeField] protected SpriteRenderer sr;
    public List<Tag> Tags { get; private set; }
    protected delegate void GetAngry(AnomalyBase anomaly);
    protected GetAngry _getAngry;
    protected Controller _controller;

    private void Awake()
    {
        
    }
    protected void Start()
    {
        Info = GameObject.FindGameObjectWithTag("Info").GetComponent<Text>();
        Debug.Log(Info.text);
        //sr = gameObject.GetComponent<SpriteRenderer>();
        Tags = new List<Tag>();
        _controller = GameObject.FindGameObjectWithTag("Controller").GetComponent<Controller>();
    }
    public virtual void CalculateContainment()
    {
        InfoUpdate();
    }

    public virtual void InfoUpdate()
    {
        Debug.Log(Info.text);
        Info.text = "Теги: ";
        for (int i = 0; i < Tags.Count; i++)
        {
            if(!Tags[i].Hidden) Info.text += "\n" + Tags[i].GetDescription();
        }
    }

    protected virtual int ResearchChance()
    {
        return 100;
    }

    public void HideShowSprite()
    {
        sr.enabled = !sr.enabled;
    }

    public class Tag
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
        for(int i = 0; i < Tags.Count; i++)
        {
            Tags[i].SetTag(this);
        }
    }
}