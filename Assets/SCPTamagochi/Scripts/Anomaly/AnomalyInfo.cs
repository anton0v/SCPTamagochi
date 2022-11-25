using System.Collections;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Rendering;

public class AnomalyInfo : AnomalyBase
{
    public enum INFO { FLESH, MECH, ELDRICH};
    protected INFO _infoType;

    static protected TagInfo TagFlesh;
    static protected TagInfo TagMech;
    static protected TagInfo TagEldrich;

    protected void Awake()
    {
        if (TagFlesh == null) TagFlesh = new TagInfo("Плоть", INFO.FLESH);
        if (TagMech == null) TagMech = new TagInfo("Механ", INFO.MECH);
        if (TagEldrich == null) TagEldrich = new TagInfo("Древние", INFO.ELDRICH);
    }

    public void Test(Test test)
    {
        bool flag = false;
        for (int i = 0; i < tags.Count && !flag; i++)
        {
            flag = tags[i].TestCheck(test);
            if (flag)
                _controller.AddKPoint(_infoType);
        }
    }

    protected class TagInfo : Tag
    {
        private INFO _infoType;
        public TagInfo(string name, INFO infoType) : base(name)
        {
            TagId = 1;
            Hidden = false;
            Name = name;
            _infoType = infoType;
            SetTag = SetInfo;
        }

        private void SetInfo(AnomalyBase anomaly)
        {
            ((AnomalyInfo)anomaly)._infoType = _infoType;
        }
    }
        
}