using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameCycle : MonoBehaviour
{
    [SerializeField] private Controller _controller;
    [SerializeField] private AnomalyTest Anomaly;
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
        AnomalyTest newAnomaly = Instantiate(Anomaly, new Vector2(0.07f, 2.042333f), Quaternion.identity);
        _controller.AddAnomaly(newAnomaly);
    }

    public void ProvideFunding()
    {
        _controller.Capital += _finance;
        _finance += 100 * (_controller.Day - 1);
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