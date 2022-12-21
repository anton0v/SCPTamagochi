using System.Collections;
using UnityEngine;

public class AnomalyBehavior : AnomalyInfo
{
    private int _angerMax;
    private int _angerCount;
    private int _angerDecrease;

    static protected TagBehavior TagCalm;
    static protected TagBehavior TagAngry;

    protected new void Awake()
    {
        base.Awake();
        if (TagCalm == null) TagCalm = new TagBehavior("Спокойный", 1, 0, DecreaseAngerCnt);
        if (TagAngry == null) TagAngry = new TagBehavior("Агрессивный", 3, 1, DecreaseAngerCnt);
    }
    protected class TagBehavior : Tag
    {
        private int _angerMax;
        private int _angerCount;
        private int _angerDecrease;
        GetAngry _getAngry;
        public TagBehavior(string name, int angerCount, int angerDecrease, GetAngry getAngry) : base(name)
        {
            TagId = 2;
            _angerMax = angerCount;
            _angerCount = angerCount;
            _angerDecrease = angerDecrease;
            _getAngry = getAngry;
            SetTag = SetBehavior;
        }

        public TagBehavior(TagBehavior tag) : base(tag.Name)
        {
            TagId = 2;
            _angerMax = tag._angerCount;
            _angerCount = tag._angerCount;
            _angerDecrease = tag._angerDecrease;
            _getAngry = tag._getAngry;
            SetTag = tag.SetTag;
        }

        public void SetBehavior(AnomalyBase anomaly)
        {
            ((AnomalyBehavior)anomaly)._angerMax = _angerMax;
            ((AnomalyBehavior)anomaly)._angerCount = _angerCount;
            ((AnomalyBehavior)anomaly)._angerDecrease = _angerDecrease;
            ((AnomalyBehavior)anomaly)._getAngry = _getAngry;
        }
    }

    public static void DecreaseAngerCnt(AnomalyBase anomaly)
    {
        if(((AnomalyBehavior)anomaly)._angerCount > 0)
        {
            ((AnomalyBehavior)anomaly)._angerCount -= ((AnomalyBehavior)anomaly)._angerDecrease;
            if (((AnomalyBehavior)anomaly)._angerCount <= 0) ((AnomalyBehavior)anomaly).sr.color = Color.red;
        }
    }
}