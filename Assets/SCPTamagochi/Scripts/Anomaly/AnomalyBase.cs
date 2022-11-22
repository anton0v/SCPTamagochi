using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class AnomalyBase : MonoBehaviour
{

    [SerializeField] private Text Info;
    private SpriteRenderer sr;
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
            Info.text += "\n" + tags[i].GetDescription();
        }
    }
    protected class Tag
    {
        public Tag(string name)
        {
            Name = name;
        }
        public string GetDescription()
        {
            return Name;
        }
        public string Name { get; protected set; }
        public delegate void TagDelegate(AnomalyBase anomaly);
        public TagDelegate SetTag;
    }
}