using System.Collections;
using UnityEditorInternal;
using UnityEngine;

public class AnomalyInfo : AnomalyBase
{
    protected enum INFO { FLESH, MECH, ELDRICH};
    protected INFO _infoType;
    protected TagInfo fleshTag = new TagInfo("Плоть", INFO.FLESH);
    protected TagInfo mechTag = new TagInfo("Механ", INFO.FLESH);
    protected TagInfo eldrichTag = new TagInfo("Древние", INFO.FLESH);

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