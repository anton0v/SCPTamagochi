using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] private AnomalyTest[] anomalies;
    [SerializeField] private AnomalyTest anomaly;
    [SerializeField] private Text Info;
    [SerializeField] private GameObject[] Rooms;
    [SerializeField] private GameObject CurrentRoom;


    public int Day { get; private set; } = 1;

    private KPoint EldrichKnowledge;
    private KPoint FleshKnowledge;
    private KPoint MechKnowledge;
    private List<KPoint> KPList;
    private Test contactTest;

    private int _actions = 3;

    private void Awake()
    {
        contactTest = new Test("Контакт", new List<int>(new int[] { 1, 2, 3, 4 }));
        EldrichKnowledge = new KPoint("Древние", AnomalyInfo.INFO.ELDRICH);
        FleshKnowledge = new KPoint("Плоть", AnomalyInfo.INFO.FLESH);
        MechKnowledge = new KPoint("Механ", AnomalyInfo.INFO.MECH);
        KPList = new List<KPoint>();
        KPList.Add(EldrichKnowledge);
        KPList.Add(FleshKnowledge);
        KPList.Add(MechKnowledge);
    }

    private void Start()
    {
        InfoUpdate();
        anomaly = anomalies[0];
        anomaly.InfoUpdate();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            anomaly.CurrentFood = (AnomalyFood.FOOD)((int)(anomaly.CurrentFood + 1) % AnomalyFood.FOOD.GetNames(typeof(AnomalyFood.FOOD)).Length);
            anomaly.InfoUpdate();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            anomaly.Test(contactTest);
            anomaly.InfoUpdate();
            anomaly.CalculateContainment();
            TakeAction();
            InfoUpdate();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anomaly.Room = AnomalyContain.CONTAIN_ROOM.OCCULT;
            if (CurrentRoom != Rooms[1])
            {
                if (CurrentRoom) CurrentRoom.gameObject.SetActive(false);
                CurrentRoom = Rooms[1];
                CurrentRoom.gameObject.SetActive(true);
            }
            InfoUpdate();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            anomaly.Room = AnomalyContain.CONTAIN_ROOM.METAL;
            if (CurrentRoom != Rooms[2])
            {
                if (CurrentRoom) CurrentRoom.gameObject.SetActive(false);
                CurrentRoom = Rooms[2];
                CurrentRoom.gameObject.SetActive(true);
            }
            InfoUpdate();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            anomaly.Room = AnomalyContain.CONTAIN_ROOM.HOUSE;
            if (CurrentRoom != Rooms[3])
            {
                if (CurrentRoom) CurrentRoom.gameObject.SetActive(false);
                CurrentRoom = Rooms[3];
                CurrentRoom.gameObject.SetActive(true);
            }
            InfoUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Space))
            anomaly.CalculateContainment();

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SwitchAnomaly(0);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SwitchAnomaly(1);
        }
    }

    private void TakeAction()
    {
        if(--_actions == 0)
        {
            _actions = 3;
            Day++;
            Debug.Log("День" + Day.ToString());
        }
    }

    public void InfoUpdate()
    {
        Info.text = "Информация: ";
        for (int i = 0; i < KPList.Count; i++)
        {
            Info.text += "\n" + KPList[i].Name + ": " + KPList[i].Count;
        }
        RoomUpdate(anomaly.Room);
    }

    public void AddKPoint(AnomalyInfo.INFO info)
    {
        for (int i = 0; i < KPList.Count; i++)
        {
            if (info == KPList[i].InfoType)
                KPList[i].Count++;
        }
    }

    public int GetKPointsOfType(AnomalyInfo.INFO infoType)
    {
        for(int i = 0; i < KPList.Count; i++)
        {
            if (infoType == KPList[i].InfoType)
                return KPList[i].Count;
        }
        return 0;
    }

    public void RoomUpdate(AnomalyContain.CONTAIN_ROOM room)
    {
        if(CurrentRoom) CurrentRoom.gameObject.SetActive(false);
        switch(room)
        {
            case AnomalyContain.CONTAIN_ROOM.NONE:
                CurrentRoom = Rooms[0];
                break;
            case AnomalyContain.CONTAIN_ROOM.OCCULT:
                CurrentRoom = Rooms[1];
                break;
            case AnomalyContain.CONTAIN_ROOM.METAL:
                CurrentRoom = Rooms[2];
                break;
            case AnomalyContain.CONTAIN_ROOM.HOUSE:
                CurrentRoom = Rooms[3];
                break;
        }
        CurrentRoom.gameObject.SetActive(true);
    }

    public void SwitchAnomaly(int num)
    {
        if(num < anomalies.Length)
        {
            anomaly.HideShowSprite();
            anomaly = anomalies[num];
            anomaly.HideShowSprite();
            anomaly.InfoUpdate();
            InfoUpdate();
        }
    }

    private class KPoint
    {
        public string Name { get; private set; }
        public AnomalyInfo.INFO InfoType { get; }
        public int Count { get; set; }
        public KPoint(string name, AnomalyInfo.INFO info)
        {
            Name = name;
            InfoType = info;
            Count = 0;
        }
    }
}
