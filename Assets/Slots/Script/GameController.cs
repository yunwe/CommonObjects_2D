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




    public bool IsSpinning
    {
        get
        {
            return !(rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped);
        }
    }


    
}
