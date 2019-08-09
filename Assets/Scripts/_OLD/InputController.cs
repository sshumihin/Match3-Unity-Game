using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    public event Action<Vector2, eSwipeDirection> MouseUpAction = delegate { };

    public event Action<Vector2> MouseDownAction = delegate { };

    public bool IsLocked { get; set; }

    private bool m_isDragging;

    private GameManager m_gameManager;

    private Vector2 m_startPosition;

    private float m_swipeThreshold = 1f;

    private Camera m_cam;

    public InputController(GameManager gameManager, float cellSize)
    {
        this.m_gameManager = gameManager;

        m_gameManager.StateChangedAction += OnGameStateChange;

        m_swipeThreshold = cellSize * .5f;
    }

    private void OnGameStateChange(GameManager.eStateGame state)
    {
        switch (state)
        {
            case GameManager.eStateGame.SETUP:
                break;
            case GameManager.eStateGame.MAIN_MENU:
                break;
            case GameManager.eStateGame.GAME_STARTED:
                StartGame();
                break;
            case GameManager.eStateGame.PAUSE:
                StopGame();
                break;
            case GameManager.eStateGame.GAME_OVER:
                StopGame();
                break;
        }
    }

    private void StartGame()
    {
        m_isDragging = false;
        IsLocked = false;

        m_cam = Camera.main;
    }

    private void StopGame()
    {
        m_isDragging = false;
        IsLocked = true;
    }

    internal void Update()
    {
        if (IsLocked || m_gameManager.State != GameManager.eStateGame.GAME_STARTED) return;

        if (Input.GetMouseButtonDown(0))
        {
            m_isDragging = true;
            m_startPosition = Input.mousePosition;

            MouseDownAction(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) && m_isDragging)
        {
            m_isDragging = false;

            Vector2 mp = Input.mousePosition;
            if (IsItSwipe(mp))
            {
                eSwipeDirection dir = GetSwipeDirection(mp);
                MouseUpAction(mp, dir);
            }
        }
    }

    private eSwipeDirection GetSwipeDirection(Vector2 mp)
    {
        Vector2 dir = mp - m_startPosition;

        float dotX = Vector2.Dot(dir, Vector2.right);
        float dotY = Vector2.Dot(dir, Vector2.up);

        if (dotX > Mathf.Abs(dotY))
        {
            if (dotX > 0f)
            {
                return eSwipeDirection.RIGHT;
            }
            else return eSwipeDirection.LEFT;
        }
        else
        {
            if (dotY > 0f)
            {
                return eSwipeDirection.UP;
            }
            else return eSwipeDirection.DOWN;
        }
    }

    private bool IsItSwipe(Vector2 mousePos)
    {
        Vector2 startPoint = m_cam.ScreenToWorldPoint(m_startPosition);
        Vector2 endPoint = m_cam.ScreenToWorldPoint(mousePos);

        return Vector2.Distance(startPoint, endPoint) > m_swipeThreshold;
    }
}
