﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    private Vector2Int gridPosition;
    
    private Vector2Int gridMoveDirection;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    public LevelGrid levelGrid;

    public void Setup(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }


    private void Awake()
    {
        gridPosition=new Vector2Int(10,10);
        gridMoveTimerMax=0.5f;
        gridMoveTimer=gridMoveTimerMax;
        gridMoveDirection=new Vector2Int(1,0);
    }
    private void Update()
    {
         HandleInput();
         HandleGridMovement(); 
    }
    private void HandleInput()
    {
        //control movement through keyboard's arrow buttons.
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gridMoveDirection.y != -1) //to restric the snake from going back to the same direction it came from.
            {
            gridMoveDirection.x = 0;   
            gridMoveDirection.y = +1;
            }
        }
         if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gridMoveDirection.y != +1)
            {
            gridMoveDirection.x = 0;   
            gridMoveDirection.y = -1;
            }
        }
         if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gridMoveDirection.x != +1) 
            {
            gridMoveDirection.x = -1;
            gridMoveDirection.y = 0;
            }   
        }
         if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gridMoveDirection.x != -1)
            {
            gridMoveDirection.x = +1;
            gridMoveDirection.y = 0;
            }    
        }
    }

    private void HandleGridMovement()
    {
        gridMoveTimer+=Time.deltaTime;
        if (gridMoveTimer>=gridMoveTimerMax)
        {
            gridPosition+=gridMoveDirection;
            gridMoveTimer-=gridMoveTimerMax;

            transform.position=new Vector3(gridPosition.x,gridPosition.y);
            transform.eulerAngles=new Vector3(0 ,0 ,GetAngleFromVector(gridMoveDirection) -90);

            levelGrid.SnakeMoved(gridPosition);
        }
        
    }

    private float GetAngleFromVector(Vector2Int dir) //for rotating snake's head according to change in movement direction.
    {
        float n=Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        if (n<0) n += 360;
        return n; 
    }

    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }
}
