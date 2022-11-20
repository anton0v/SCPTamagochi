using System.Collections;
using UnityEngine;

public class AnomalyBehavior : AnomalyInfo
{
    private int _angerMax;
    private int _angerCount;
    private int _angerDecrease;
    GetAngry _decreaseAngerCnt;

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
        AnomalyBase.GetAngry _getAngry;
        public TagBehavior(string name, int angerCount, int angerDecrease, GetAngry getAngry) : base(name)
        {
            _angerMax = angerCount;
            _angerCount = angerCount;
            _angerDecrease = angerDecrease;
            _getAngry = getAngry;
            SetTag = SetBehavior;
        }

        public void SetBehavior(AnomalyBase anomaly)
        {
            ((AnomalyBehavior)anomaly)._angerMax = _angerMax;
            ((AnomalyBehavior)anomaly)._angerCount = _angerCount;
            ((AnomalyBehavior)anomaly)._angerDecrease = _angerDecrease;
            ((AnomalyBehavior)anomaly)._getAngry = _getAngry;
        }
    }

    public void DecreaseAngerCnt()
    {
        if(_angerCount > 0) _angerCount -= _angerDecrease;
    }
}