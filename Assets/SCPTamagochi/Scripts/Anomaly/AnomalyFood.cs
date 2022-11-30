using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyFood : AnomalyContain
{
    public enum FOOD { MEAT, FRUIT };
    private FOOD _preferFood;
    public FOOD CurrentFood { get; set; }

    static protected TagFood TagFoodFruit;
    static protected TagFood TagFoodMeat;
    static protected List<TagFood> FoodTagList;

    protected new void Awake()
    {
        base.Awake();
        if (TagFoodFruit == null) TagFoodFruit = new TagFood("Фруктоед", FOOD.FRUIT);
        if (TagFoodMeat == null) TagFoodMeat = new TagFood("Мясоед", FOOD.MEAT);
        if (FoodTagList == null)
        {
            FoodTagList = new List<TagFood>();
            FoodTagList.Add(TagFoodMeat);
            FoodTagList.Add(TagFoodFruit);
        }

    }

    public override void CalculateContainment()
    {
        base.CalculateContainment();
        if (CurrentFood != _preferFood) _getAngry();
    }

    public override void InfoUpdate()
    {
        base.InfoUpdate();
        Info.text += "\nРацион: ";
        switch(CurrentFood)
        {
            case FOOD.MEAT:
                Info.text += "Мясо";
                break;
            case FOOD.FRUIT:
                Info.text += "Фрукты";
                break;
            default:
                break;
        }
    }

    public void HideShowSprite()
    {
        sr.enabled = !sr.enabled;
    }

    protected override int ResearchChance()
    {
        return (_preferFood == CurrentFood) ? base.ResearchChance() : base.ResearchChance() - 10;
    }
    protected class TagFood : Tag
    {
        private FOOD _preferFood;
        public TagFood(string name, FOOD preferFood) : base(name)
        {
            TagId = 3;
            Name = name;
            _preferFood = preferFood;
            SetTag = SetFood;
        }

        public TagFood(TagFood tag) : base(tag.Name)
        {
            TagId = 3;
            Name = tag.Name;
            _preferFood = tag._preferFood;
            SetTag = tag.SetTag;
        }

        public void SetFood(AnomalyBase anomaly)
        {
            ((AnomalyFood)anomaly)._preferFood = _preferFood;
        }
    }
}