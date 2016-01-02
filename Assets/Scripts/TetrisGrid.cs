﻿using UnityEngine;
using System.Collections;
using System;

/*//
Enum [전역]
*///
public enum blockType { Main, I, J, L, S, Z, T, O, NUM_TYPES }
public enum blockColor { Null, Sky, Blue, Orange, Green, Red, Purple, Yellow, Grey, NUM_COLORS }

//MonoBehavior에서 제공하는 함수 등이 필요 없기 때문에 제거
public class TetrisGrid {

    private blockType _blockType;
    

    /*//
    Int
    *///
    public int gridHeight;
    public int gridLength;
    private int _blockTurnState;
    private int _color;
    private int _pivotX;
    private int _pivotY;

    /*//
    Array int
    *///
    public int[,] grid;

    /*//
    Bool
    *///
    private bool _validCheck;

    /*//
    합수
    *///

    ////생성자 (블록타입 추가 필요)
    public TetrisGrid (int gridHeight, int gridLength)
    {
        this.gridHeight = gridHeight;
        this.gridLength = gridLength;
        this.grid = new int[gridHeight, gridLength];
    }
    
    ////유효성검사
    public bool ValidCheck(TetrisGrid mainGrid)
    {
        //행렬 사이즈가 다른 경우 FALSE 반환
        if (mainGrid.gridHeight != this.gridHeight || mainGrid.gridLength != this.gridLength)
        {
            Debug.Log("The grid size doesn't match");
            return false;
        }

        for(int y = 0; y < gridHeight; y++)
        {
            for(int x = 0; x < gridLength; x++)
            {
                //행렬에서 블록이 겹치는 경우(곱할경우 0이 아님)
                if (mainGrid.grid[y,x] * this.grid[y,x] != 0)
                {
                    return false;
                }
            }
        }

        //겹치는 블록이 없다면 TRUE 반환
        return true;
    }
    
    ////왼쪽이동
    public void MoveLeft(TetrisGrid mainGrid, out bool valid)
    {
        valid = true;
        
        //왼쪽 가장자리에 블록이 있다면 변화 없음
        for(int y = 0; y < gridHeight; y++)
        {
                if(this.grid[y,0] != 0)
                {
                    valid = false;
                    return;
                }
        }

        //grid 임시 저장
        int[,] tempGrid = this.grid;

        //왼쪽 이동
        for (int y = 0; y < gridHeight; y++)
        {
            for(int x =0; x < (gridLength-1); x++)
            {
                this.grid[y, x] = this.grid[y, x + 1];
            }
        }
        for(int y =0; y < gridHeight; y++)
        {
            this.grid[y, gridLength - 1] = 0;
        }

        //mainGrid와 겹치면 복귀 또는 mainGrid가 비어있으면 그대로 반환 
        if (ValidCheck(mainGrid) == false && mainGrid != null)
        {
            this.grid = tempGrid;
            valid = false;
        }
    }


    ////오른쪽이동
    public void MoveRight(TetrisGrid mainGrid, out bool valid)
    {
        valid = true;
        
        //오른쪽 가장자리에 블록이 있다면 변화 없음
        for (int y = 0; y < gridHeight; y++)
        {
                if (this.grid[y, gridLength - 1 ] != 0)
                {
                    valid = false;
                    return;
                }
        }

        //grid 임시 저장
        int[,] tempGrid = this.grid;

        //오른쪽 이동
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = (gridLength - 1); x > 0 ; x--)
            {
                this.grid[y, x] = this.grid[y, x - 1];
            }
        }
        for (int y = 0; y < gridHeight; y++)
        {
            this.grid[y, 0] = 0;
        }
        
        //mainGrid와 겹치면 복귀
        if (ValidCheck(mainGrid) == false && mainGrid != null)
        {
            this.grid = tempGrid;
            valid = false;
        }
    }

    ////아래이동
    public void MoveDown(TetrisGrid mainGrid, out bool valid)
    {
        valid = true;
        
        //바닥에 블록이 있는 경우 변경 없음 및 valid 거짓
        for(int x = 0; x < gridLength; x++)
        {
            if(this.grid[ gridHeight - 1 ,x] != 0)
            {
                valid = false;
                return;
            }
        }

        //grid 임시 저장
        int[,] tempGrid = this.grid;

        //아래쪽 이동 및 valid 참
        for (int y = gridHeight - 1; y > 0 ; y--)
        {
            for(int x = 0; x < gridLength; x++)
            {
                this.grid[y, x] = this.grid[y - 1, x];
            }
        }
        for(int x = 0; x < gridLength; x++)
        {
            this.grid[0, x] = 0;
        }

        //mainGrid와 겹치면 복귀 및 valid 거짓
        if (ValidCheck(mainGrid) == false && mainGrid != null)
        {
            this.grid = tempGrid;
            valid = false;
        }
    }

    ////합치기(적용)
    public void MergeGrid(TetrisGrid mainGrid)
    {
        //겹치는지 확인(매번 검사하기 때문에 당연히 겹치면 안된다. 디버깅용)
        if (ValidCheck(mainGrid))
            Debug.Log("합치기 오류!!");

        //행렬 더하기
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x= 0; x < gridLength; x++)
            {
                mainGrid.grid[y, x] += this.grid[y, x];
            }
        }
    }

    ////회전
    public void Turn(TetrisGrid mainGrid)
    {
        //I 타입
        if (this._blockType == blockType.I)
        {
            TetrisGrid temp = this;

            //TurnState == 0
            if (this._blockTurnState == 0)
            {
                //배열 크기 체크(회전할 공간 없을 경우 Pivot 이동)
                if(this._pivotX > 0)
                {
                    
                }

            }
        }
    }
}
