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
    public bool IsStudied { get; private set; } = false;

    private void Awake()
    {
        
    }
    protected void Start()
    {
        _controller = GameObject.FindGameObjectWithTag("Controller").GetComponent<Controller>();
        Info = GameObject.FindGameObjectWithTag("Info").GetComponent<Text>();
        Tags = new List<Tag>();
    }
    public virtual void CalculateContainment()
    {
        //InfoUpdate();
    }

    public virtual void InfoUpdate()
    {
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

    public void CheckHiddenTags()
    {
        for (int i = 0; i < Tags.Count; i++)
        {
            if (Tags[i].Hidden)
                return;
        }
        IsStudied = true;
    }

    protected void ShuffleHiddenTags()
    {
        for(int i = 0;  i < Tags.Count; i++)
        {
            Tag temp = Tags[i];
            int randIndex = Random.Range(0, Tags.Count);
            Tags[i] = Tags[randIndex];
            Tags[randIndex] = temp;
        }
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
        ShuffleHiddenTags();
    }
}