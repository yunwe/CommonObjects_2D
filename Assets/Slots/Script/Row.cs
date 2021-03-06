﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    static readonly int[][] MAP = new int[5][];
    static Sprite[] SYMBOLS;

    public int id;

    [SerializeField]
    private float m_slotHeight;
    [SerializeField]
    private int m_spriteSortingOrder;

    [HideInInspector]
    public bool rowStopped;
    [HideInInspector]
    public int endResult;

    
    private int m_MapId;
    private int m_MoveInterval;
    private float m_MoveLength;
    private Vector3 m_StartPos;

    void Awake()
    {
        if(SYMBOLS == null)
        {
            MAP[0] = new int[] { 0, 1, 6, 9, 12, 3, 4, 21, 24, 0 };
            MAP[1] = new int[] { 2, 5, 8, 11, 13, 16, 17, 18, 10, 2 };
            MAP[2] = new int[] { 7, 10, 14, 15, 19, 20, 22, 23, 6, 7 };
            MAP[3] = new int[] { 3, 10, 14, 1, 6, 9, 17, 22, 23, 3 };
            MAP[4] = new int[] { 10, 2, 5, 8, 19, 13, 16, 11, 13, 10 };

            SYMBOLS = Resources.LoadAll<Sprite>("Symbols");
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rowStopped = true;
        Handle.HandlePulled += StartRotating;

        m_MapId = Random.Range(0, MAP.GetLength(0));
        SetUp(MAP[m_MapId]);
        m_StartPos = transform.position;
        m_MoveInterval = 3;
        m_MoveLength = m_slotHeight / m_MoveInterval;
    }

    private void StartRotating()
    {
        endResult = -1;
        StartCoroutine("Rotate");
    }

    public void StopRotating(int stopIndex)
    {
        endResult = stopIndex;
    }

    private void SetUp(int[] symbols)
    {
        for(int i=0; i < symbols.Length; i++)
        {
            GameObject symbol = new GameObject("slot_" + i);
            symbol.transform.SetParent(transform);
            symbol.transform.localScale = Vector3.one;
            symbol.transform.localRotation = Quaternion.identity;
            symbol.transform.localPosition = new Vector3(0, i * m_slotHeight, 0);

            SpriteRenderer renderer = symbol.AddComponent<SpriteRenderer>();
            renderer.sprite = SYMBOLS[symbols[i]];
            renderer.sortingOrder = m_spriteSortingOrder;
            renderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
    }

    private void Move()
    {
        if (transform.position.y <= -m_slotHeight * (MAP[m_MapId].Length - 1))
            transform.position = m_StartPos;

        transform.position = new Vector3(transform.position.x, transform.position.y - m_MoveLength);
    }

    private int GetDisplayIndex(Vector3 current)
    {
        float y = Mathf.Abs(current.y - m_StartPos.y);
        return Mathf.FloorToInt(y / m_slotHeight);
    }

    private IEnumerator Rotate()
    {
        Debug.Log("Rotating");

        rowStopped = false;
        float timeInterval = 0.025f;
        int moveCount = 0;
        
        while(endResult < 0 || moveCount%m_MoveInterval != 0)
        {
            Move();
            moveCount++;
            yield return new WaitForSeconds(timeInterval);
        }

        int itemCount = MAP[m_MapId].Length - 1;
        int random = Random.Range(1, 3) * m_MoveInterval * itemCount;

        int currentIndex = GetDisplayIndex(transform.position);
        int dist = endResult - currentIndex;
        dist = dist < 0 ? itemCount + dist : dist;

        int steps = (dist * m_MoveInterval) + random;

        ////reduce speed
        for (int i = 0; i < steps; i++)
        {
            Move();

            if (i > Mathf.RoundToInt(steps * 0.95f))
                timeInterval = 0.2f;
            else if (i > Mathf.RoundToInt(steps * 0.75f))
                timeInterval = 0.15f;
            else if (i > Mathf.RoundToInt(steps * 0.5f))
                timeInterval = 0.1f;
            else if (i > Mathf.RoundToInt(steps * 0.25f))
                timeInterval = 0.05f;

            yield return new WaitForSeconds(timeInterval);

        }


        rowStopped = true;
        endResult = GetDisplayIndex(transform.position);
        Debug.Log("Row " + id + " end At - " + endResult);
    }

     
}
