using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Rendering;

public class AnomalyInfo : AnomalyBase
{
    public enum INFO { FLESH, MECH, ELDRICH};
    public INFO InfoType { get; private set; }
    protected int _infoRequiredScore = 0;

    static protected TagInfo TagFlesh;
    static protected TagInfo TagMech;
    static protected TagInfo TagEldrich;
    static protected List<TagInfo> InfoTagList;

    protected void Awake()
    {
        if (TagFlesh == null) TagFlesh = new TagInfo("Плоть", INFO.FLESH);
        if (TagMech == null) TagMech = new TagInfo("Механ", INFO.MECH);
        if (TagEldrich == null) TagEldrich = new TagInfo("Древние", INFO.ELDRICH);
        if (InfoTagList == null)
        {
            InfoTagList = new List<TagInfo>();
            InfoTagList.Add(TagFlesh);
            InfoTagList.Add(TagMech);
            InfoTagList.Add(TagEldrich);
        }
    }

    public bool Test(Test test)
    {
        bool flag = false;
        Debug.Log("Шанс исследования: " + ResearchChance().ToString());
        if(Random.Range(0, 101) > ResearchChance())
        {
            Debug.Log("Тестирование провалено");
            return false;
        }

        Debug.Log("Тестирование успешно");
        for (int i = 0; i < Tags.Count && !flag; i++)
        {
            flag = Tags[i].TestCheck(test);
            if (flag)
                _controller.AddKPoint(InfoType);
        }
        return true;
    }

    protected override int ResearchChance()
    {
        int current = _controller.GetKPointsOfType(InfoType);
        return (current < _infoRequiredScore) ? base.ResearchChance() - 10 * (_infoRequiredScore - current) : base.ResearchChance();
    }

    protected class TagInfo : Tag
    {
        private INFO InfoType;
        public TagInfo(string name, INFO infoType) : base(name)
        {
            TagId = 1;
            Hidden = false;
            Name = name;
            InfoType = infoType;
            SetTag = SetInfo;
        }

        public TagInfo(TagInfo tag) : base(tag.Name)
        {
            TagId = 1;
            Hidden = false;
            InfoType = tag.InfoType;
            SetTag = tag.SetTag;
        }

        private void SetInfo(AnomalyBase anomaly)
        {
            ((AnomalyInfo)anomaly).InfoType = InfoType;
        }
    }
        
}