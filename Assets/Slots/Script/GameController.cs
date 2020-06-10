using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public InputField row_0;
    public InputField row_1;
    public InputField row_2;

    public Button stopButton;

    [SerializeField]
    private Row[] rows;

    public void Awake()
    {
        stopButton.onClick.AddListener(StopSpinning);
        row_0.text = "0";
        row_1.text = "0";
        row_2.text = "0";
    }


    public bool IsSpinning
    {
        get
        {
            return !(rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped);
        }
    }

    void StopSpinning()
    {
        rows[0].StopRotating(int.Parse(row_0.text));
        rows[1].StopRotating(int.Parse(row_1.text));
        rows[2].StopRotating(int.Parse(row_2.text));
    }
    
}
