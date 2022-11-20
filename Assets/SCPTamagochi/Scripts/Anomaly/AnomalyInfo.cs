using System.Collections;
using UnityEditorInternal;
using UnityEngine;

public class AnomalyInfo : AnomalyBase
{
    protected enum INFO { FLESH, MECH, ELDRICH};
    protected INFO _infoType;

    static protected TagInfo TagFlesh = new TagInfo("Плоть", INFO.FLESH);
    static protected TagInfo TagMech = new TagInfo("Механ", INFO.MECH);
    static protected TagInfo TagEldrich = new TagInfo("Древние", INFO.ELDRICH);

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