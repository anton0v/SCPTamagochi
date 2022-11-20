using System.Collections;
using UnityEditorInternal;
using UnityEngine;

public class AnomalyInfo : AnomalyBase
{
    protected enum INFO { FLESH, MECH, ELDRICH};
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

    protected class TagInfo : Tag
    {
        private AnomalyInfo.INFO _infoType;
        public TagInfo(string name, AnomalyInfo.INFO infoType) : base(name)
        {
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