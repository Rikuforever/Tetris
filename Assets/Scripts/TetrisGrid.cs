using UnityEngine;
using System.Collections;
using System;

/*//
Enum [전역]
*///
public enum blockType { Main, I, J, L, S, Z, T, O, NUM_TYPES }
public enum blockColor { Null, Sky, Blue, Orange, Green, Red, Purple, Yellow, Grey, NUM_COLORS }

//MonoBehavior에서 제공하는 함수 등이 필요 없기 때문에 제거
[Serializable]
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

    ////[임시]생성자 (블록타입 추가 필요)
    public TetrisGrid (int gridHeight, int gridLength, blockType blockType, int pivotX, int pivotY, int turnState)
    {
        this.gridHeight = gridHeight;
        this.gridLength = gridLength;
        this.grid = new int[gridHeight, gridLength];
        this._blockType = blockType;
        this._pivotX = pivotX;
        this._pivotY = pivotY;
        this._blockTurnState = turnState;
    }
    
    ////복사생성자
    public TetrisGrid (TetrisGrid T)
    {
        this.gridHeight = T.gridHeight;
        this.gridLength = T.gridLength;
        this.grid = (int[,])T.grid.Clone();
        this._blockType = T._blockType;
        this._pivotX = T._pivotX;
        this._pivotY = T._pivotY;
        this._blockTurnState = T._blockTurnState;
        this._color = T._color;
        this._validCheck = T._validCheck;
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
        TetrisGrid temp = new TetrisGrid(this);

        //왼쪽 이동
        this._pivotX -= 1;

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
        if (mainGrid != null && ValidCheck(mainGrid) == false)
        {
            Debug.Log("GoBack!");
            this.grid = temp.grid;
            Debug.Log(this.grid[2, 0]);
            Debug.Log(temp.grid[2, 0]);
            this._pivotX = temp._pivotX;
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
        TetrisGrid temp = new TetrisGrid(this);

        //오른쪽 이동
        this._pivotX += 1;

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
        if (mainGrid != null && ValidCheck(mainGrid) == false)
        {
            this.grid = temp.grid;
            this._pivotX = temp._pivotX;
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
        TetrisGrid temp = new TetrisGrid(this);

        //아래쪽 이동 및 valid 참
        this._pivotY += 1;

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
        if (mainGrid != null && ValidCheck(mainGrid) == false)
        {
            this.grid = temp.grid;
            this._pivotY = temp._pivotY;
            valid = false;
        }
    }

    ////위이동
    public void MoveUp(TetrisGrid mainGrid, out bool valid)
    {
        valid = true;

        //천장에 있는 경우 변경 없음 및 valid 거짓
        for (int x = 0; x < gridLength; x++)
        {
            if (this.grid[0, x] != 0)
            {
                valid = false;
                return;
            }
        }

        //grid 임시 저장
        TetrisGrid temp = new TetrisGrid(this);

        //위 이동 및 valid 참
        this._pivotY -= 1;

        for (int y = 1; y < gridHeight; y++)
        {
            for (int x = 0; x < gridLength; x++)
            {
                this.grid[y - 1, x] = this.grid[y, x];
            }
        }
        for (int x = 0; x < gridLength; x++)
        {
            this.grid[gridHeight - 1, x] = 0;
        }

        //mainGrid와 겹치면 복귀 및 valid 거짓
        if (mainGrid != null && ValidCheck(mainGrid) == false)
        {
            this.grid = temp.grid;
            this._pivotY = temp._pivotY;
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
            TetrisGrid temp = new TetrisGrid(this);

            //TurnState == 0
            if (this._blockTurnState == 0)
            {
                //배열 크기 확인, 공간 부족할시 임시 이동
                if (this._pivotX == 0 || this.grid[_pivotY, _pivotX - 1] != 0)
                {
                    MoveRight(null, out _validCheck);
                }
                else if (this._pivotX == (this.gridLength - 1) || this.grid[_pivotY, _pivotX + 1] != 0)
                {
                    MoveLeft(null, out _validCheck);
                    MoveLeft(null, out _validCheck);
                }
                else if (this._pivotX == (this.gridLength - 2) || this.grid[_pivotY, _pivotX + 2] != 0)
                {
                    MoveLeft(null, out _validCheck);
                }

                //임시 회전
                Debug.Log("TurnStart");
                MoveBlock(_pivotX, _pivotY - 1, _pivotX + 2, _pivotY);
                MoveBlock(_pivotX, _pivotY, _pivotX + 1, _pivotY);
                MoveBlock(_pivotX, _pivotY + 1, _pivotX, _pivotY);
                MoveBlock(_pivotX, _pivotY + 2, _pivotX - 1, _pivotY);
                Debug.Log("TurnEnd");
                //정보 갱신
                this._pivotX += 1;
                this._blockTurnState = 1;
                Debug.Log("NewPivotX: " + this._pivotX + " NewPivotY: " + this._pivotY +" NewTurnState" + this._blockTurnState);

                //유효성 체크, false시 위로 한칸 올린다.
                if (ValidCheck(mainGrid) == false)
                {
                    MoveUp(mainGrid, out _validCheck);
                    //그래도 겹치면 원위치
                    if(_validCheck == false)
                    {
                        this._pivotX = temp._pivotX;
                        this._pivotY = temp._pivotY;
                        this.grid = temp.grid;
                        this._blockTurnState = temp._blockTurnState;
                    }
                }

            }
        }
    }

    //블록 이동
    public void MoveBlock(int beforeX, int beforeY, int afterX, int afterY)
    {
        this.grid[afterY, afterX] = this.grid[beforeY, beforeX];
        this.grid[beforeY, beforeX] = 0;
    }
}
