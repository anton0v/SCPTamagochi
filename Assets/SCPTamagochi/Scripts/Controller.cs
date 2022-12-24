using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    
    [SerializeField] private Text Info;
    [SerializeField] private GameObject[] Rooms;
    [SerializeField] private GameObject CurrentRoom;
    private List<AnomalyBase> _anomalies;
    private KeyCode[] _keypad;
    private KeyCode[] _keyForRooms;
    public AnomalyTest Anomaly { get; private set; }


    public int Day { get; private set; } = 1;
    public int Actions { get; private set; } = 3;
    public int Capital { get; set; } = 100;

    private KPoint EldrichKnowledge;
    private KPoint FleshKnowledge;
    private KPoint MechKnowledge;
    public List<KPoint> KPList { get; private set; }
    private Test contactTest;
    public UnityAction OnEndDay;
    

    private void Awake()
    {
        contactTest = new Test("�������", new List<int>(new int[] { 1, 2, 3, 4 }));
        EldrichKnowledge = new KPoint("�������", AnomalyInfo.INFO.ELDRICH);
        FleshKnowledge = new KPoint("�����", AnomalyInfo.INFO.FLESH);
        MechKnowledge = new KPoint("�����", AnomalyInfo.INFO.MECH);
        KPList = new List<KPoint>();
        KPList.Add(EldrichKnowledge);
        KPList.Add(FleshKnowledge);
        KPList.Add(MechKnowledge);
        _keypad = new KeyCode[] { KeyCode.Keypad0,
                                  KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3,
                                  KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6,
                                  KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9,
        };
        _keyForRooms = new KeyCode[] { 0, KeyCode.A , KeyCode.S , KeyCode.D };
        _anomalies = new List<AnomalyBase>();
    }

    private void Start()
    {
        
    }
    public void Init()
    {
        Anomaly = (AnomalyTest)_anomalies[0];
        InfoUpdate();
        Anomaly.HideShowSprite();
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

        

        if (Input.GetKeyDown(KeyCode.Space))
            CheckContainment();

        CheckKeyPadPressed();
        CheckKeyForRooms();

    }

    private void CheckKeyPadPressed()
    {
        for(int i = 0; i < _keypad.Length; i++)
            if (Input.GetKeyDown(_keypad[i]))
            {
                SwitchAnomaly(i);
            }
    }
    private void CheckKeyForRooms()
    {
        for (int i = 0; i < _keyForRooms.Length; i++)
            if (Input.GetKeyDown(_keyForRooms[i]))
            {
                ChangeRoom((AnomalyContain.CONTAIN_ROOM)i);
            }
    }
    public void ChangeDiet()
    {
        Anomaly.CurrentFood = (AnomalyFood.FOOD)((int)(Anomaly.CurrentFood + 1) % AnomalyFood.FOOD.GetNames(typeof(AnomalyFood.FOOD)).Length);
        Anomaly.InfoUpdate();
    }

    public void ChangeRoom(AnomalyContain.CONTAIN_ROOM room)
    {
        Anomaly.Room = room;
        Capital -= Anomaly.GetRoomCost();
        if (CurrentRoom != Rooms[(int)room])
        {
            if (CurrentRoom) CurrentRoom.gameObject.SetActive(false);
            CurrentRoom = Rooms[(int)room];
            CurrentRoom.gameObject.SetActive(true);
        }
        InfoUpdate();
    }

    public bool ContactTest()
    {
        bool result = Anomaly.Test(contactTest);
        Anomaly.InfoUpdate();
        CheckContainment();
        TakeAction();
        InfoUpdate();
        return result;
    }

    private void TakeAction()
    {
        if(--Actions == 0)
        {
            Actions = 3;
            Day++;
            Debug.Log("����" + Day.ToString());
            if (Day > 10)
                return;
            OnEndDay();
        }
    }

    private void CheckContainment()
    {
        for (int i = 0; i < _anomalies.Count; i++)
        {
            _anomalies[i].CalculateContainment();
        }
    }
    public void InfoUpdate()
    {
        Info.text = "����������: ";
        Info.text += "\n�������: " + Capital;
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

    public int GetAnomalyCount()
    {
        return _anomalies.Count;
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

    public void AddAnomaly(AnomalyBase anomaly)
    {
        _anomalies.Add(anomaly);
    }

    public void SwitchAnomaly(int num)
    {
        if(num < _anomalies.Count)
        {
            Anomaly.HideShowSprite();
            Anomaly = (AnomalyTest)_anomalies[num];
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
