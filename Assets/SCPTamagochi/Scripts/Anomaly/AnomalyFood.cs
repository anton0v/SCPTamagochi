using System.Collections;
using UnityEngine;

public class AnomalyFood : AnomalyBehavior
{
    public enum FOOD { MEAT, FRUIT };
    private FOOD _preferFood;
    public FOOD CurrentFood { get; set; }

    static protected FoodTag TagFoodFruit = new FoodTag("Фрукты", FOOD.FRUIT);
    static protected FoodTag TagFoodFMeat = new FoodTag("Мясо", FOOD.MEAT);

    protected new void Awake()
    {
        base.Awake();
        if (TagFoodFruit == null) TagFoodFruit = new FoodTag("Фрукты", FOOD.FRUIT);
        if (TagFoodFMeat == null) TagFoodFMeat = new FoodTag("Мясо", FOOD.MEAT);
    }

    protected class FoodTag : Tag
    {
        private FOOD _preferFood;
        public FoodTag(string name, FOOD preferFood) : base(name)
        {
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