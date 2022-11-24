using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyFood : AnomalyBehavior
{
    public enum FOOD { MEAT, FRUIT };
    private FOOD _preferFood;
    public FOOD CurrentFood { get; set; }

    static protected FoodTag TagFoodFruit;
    static protected FoodTag TagFoodMeat;
    static protected List<FoodTag> FoodTagList;

    protected new void Awake()
    {
        base.Awake();
        if (TagFoodFruit == null) TagFoodFruit = new FoodTag("Фруктоед", FOOD.FRUIT);
        if (TagFoodMeat == null) TagFoodMeat = new FoodTag("Мясоед", FOOD.MEAT);
        if (FoodTagList == null)
        {
            FoodTagList = new List<FoodTag>();
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
    protected class FoodTag : Tag
    {
        private FOOD _preferFood;
        public FoodTag(string name, FOOD preferFood) : base(name)
        {
            TagId = 3;
            Name = name;
            _preferFood = preferFood;
            SetTag = SetFood;
        }


        public void SetFood(AnomalyBase anomaly)
        {
            ((AnomalyFood)anomaly)._preferFood = _preferFood;
        }
    }
}