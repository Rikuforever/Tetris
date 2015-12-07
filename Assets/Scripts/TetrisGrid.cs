using UnityEngine;
using System.Collections;

//MonoBehavior에서 제공하는 함수 등이 필요 없기 때문에 제거
public class TetrisGrid {

    //Int
    public int gridHeight;
    public int gridLength;
    private int _blockTurnState;
    private int _color;

    //Array int
    public int[,] grid;
    private int[] _pivot;

    //Enum
    private enum _blockType { Main, I, J, L, S, Z, T, O}

    //합수

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

}
