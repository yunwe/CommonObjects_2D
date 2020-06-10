using System.Collections;
using System;
using UnityEngine;

public class Handle : MonoBehaviour
{
    public static event Action HandlePulled = delegate { };
    public GameController gameController;

    private void OnMouseDown()
    {
        Debug.Log("OMD");

        if (!gameController.IsSpinning)
            StartCoroutine("IPullHandle");
    }

    IEnumerator IPullHandle()
    {
        for (int i = 0; i < 15; i += 5)
        {
            transform.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.1f);
        }

        HandlePulled();

        for (int i = 0; i < 15; i += 5)
        {
            transform.Rotate(0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }

    }
}
