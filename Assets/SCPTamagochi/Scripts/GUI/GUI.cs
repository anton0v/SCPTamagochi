using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUI : MonoBehaviour
{

    private int _ObjectId;
    [SerializeField] private Controller GameController;
    public GameObject _ButtonMenu;
    // Start is called before the first frame update
    //Diet
    public void FeedButton()
    {
        GameController.ChangeDiet();

    }
    //Test(s)
    public void TestButton()
    {
        _ButtonMenu.SetActive(true);

    }
    public void CancelTestButton()
    {
        _ButtonMenu.SetActive(false);

    }
    public void ContactTestButton()
    {
        GameController.ContactTest();
    }
    //Room(s)
    public void RoomButton()
    {

    }
    public void HomeRoomButton()
    
    {
         
    }
    //switch buttons
    public void NextButton()
    {
       if (_ObjectId != GameController.GetAnomalyCount())
       {
            _ObjectId++;

       }
       else
       {
            _ObjectId = 0;
       }
        Debug.Log(_ObjectId);
        GameController.SwitchAnomaly(_ObjectId);

    }
    public void PreviousButton()
    {
        if (_ObjectId > 0)
        {
            _ObjectId--;

        }
        else
        {
            _ObjectId = GameController.GetAnomalyCount();
        }
        Debug.Log(_ObjectId); 
        GameController.SwitchAnomaly(_ObjectId);

    }
    public void SteelRoomButton()
    {
    }
    public void OcultlRoomButton()
    {
       
    }

    void Start()
    {
        _ObjectId = 0;
        _ButtonMenu = GameObject.Find("Panel");
        _ButtonMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
