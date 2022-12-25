using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameCycle : MonoBehaviour
{
    [SerializeField] private Controller _controller;
    [SerializeField] private AnomalyTest SimpleAnomaly;
    [SerializeField] private AnomalyTest NormalAnomaly;
    [SerializeField] private AnomalyTest HardAnomaly;
    private int _finance = 200;
    private void Start()
    {
        AddAnomaly();
        _controller.Init();
        _controller.OnEndDay += AddAnomaly;
        _controller.OnEndDay += ProvideFunding;
        _controller.OnGameEnd += Victory;
        _controller.OnFailure += Failure;
    }

    public void AddAnomaly()
    {
        AnomalyTest newAnomaly;
        if (_controller.Day < 4)
            newAnomaly = CreateAnomaly(SimpleAnomaly);
        else if (_controller.Day < 8)
            newAnomaly = CreateAnomaly(NormalAnomaly);
        else
            newAnomaly = CreateAnomaly(HardAnomaly);

        _controller.AddAnomaly(newAnomaly);
    }

    private AnomalyTest CreateAnomaly(AnomalyTest anomaly)
    {
        return Instantiate(anomaly, new Vector2(0.07f, 2.042333f), Quaternion.identity);
    }

    public void ProvideFunding()
    {
        _controller.Capital += _finance;
        _finance += 50 * (_controller.Day - 1);
    }

    public void Victory()
    {
        Debug.Log("ВСЕ АНОМАЛИИ ИЗУЧЕНЫ");
    }

    public void Failure()
    {
        Debug.Log("ДЕНЬГИ КОНЧАЛИСЬ, КОНЕЦ ИГРЫ");
    }
}