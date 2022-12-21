using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] private AnomalyTest[] anomalies;
    [SerializeField] private Text Info;
    [SerializeField] private GameObject[] Rooms;
    [SerializeField] private GameObject CurrentRoom;
    [SerializeField] public AnomalyTest Anomaly { get; private set; }


    public int Day { get; private set; } = 1;
    public int Actions { get; private set; } = 3;

    private KPoint EldrichKnowledge;
    private KPoint FleshKnowledge;
    private KPoint MechKnowledge;
    public List<KPoint> KPList { get; private set; }
    private Test contactTest;
    

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
        Anomaly = anomalies[0];
        Anomaly.InfoUpdate();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeDiet();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ContactTest();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Anomaly.Room = AnomalyContain.CONTAIN_ROOM.OCCULT;
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
            Anomaly.Room = AnomalyContain.CONTAIN_ROOM.METAL;
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
            Anomaly.Room = AnomalyContain.CONTAIN_ROOM.HOUSE;
            if (CurrentRoom != Rooms[3])
            {
                if (CurrentRoom) CurrentRoom.gameObject.SetActive(false);
                CurrentRoom = Rooms[3];
                CurrentRoom.gameObject.SetActive(true);
            }
            InfoUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Space))
            Anomaly.CalculateContainment();

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SwitchAnomaly(0);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SwitchAnomaly(1);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SwitchAnomaly(2);
        }

    }
    public void ChangeDiet()
    {
        Anomaly.CurrentFood = (AnomalyFood.FOOD)((int)(Anomaly.CurrentFood + 1) % AnomalyFood.FOOD.GetNames(typeof(AnomalyFood.FOOD)).Length);
        Anomaly.InfoUpdate();
    }

    public void ContactTest()
    {
        Anomaly.Test(contactTest);
        Anomaly.InfoUpdate();
        Anomaly.CalculateContainment();
        TakeAction();
        Debug.Log(Anomaly);
        InfoUpdate();
    }

    private void TakeAction()
    {
        if(--Actions == 0)
        {
            Actions = 3;
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
        RoomUpdate(Anomaly.Room);
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
            Anomaly.HideShowSprite();
            Anomaly = anomalies[num];
            Anomaly.HideShowSprite();
            Anomaly.InfoUpdate();
            InfoUpdate();
        }
    }

    public class KPoint
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
