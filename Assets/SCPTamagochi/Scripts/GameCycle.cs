using System.Collections;
using UnityEngine;

public class GameCycle : MonoBehaviour
{
    [SerializeField] private Controller _controller;
    [SerializeField] private AnomalyTest Anomaly;

    private void Start()
    {
        AddAnomaly();
        _controller.Init();
        _controller.OnEndDay += AddAnomaly;
    }

    public void AddAnomaly()
    {
        AnomalyTest newAnomaly = Instantiate(Anomaly, new Vector2(0.07f, 2.042333f), Quaternion.identity);
        _controller.AddAnomaly(newAnomaly);
    }
}