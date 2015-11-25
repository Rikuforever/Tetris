using UnityEngine;
using System.Collections;

public class TetrisGrid : MonoBehaviour {

    //게임 판 높이와 너비
    public int gridHeight;
    public int gridLength;

    //게임 전체 판 (글로벌)
    public int[,] baseGrid;

    //플레이어가 컨트롤중인 블록 위치 값
    private int[,] playerGrid;

	// Use this for initialization
	void Start () {
        baseGrid = new int[gridHeight, gridLength];
        playerGrid = new int[gridHeight, gridLength];
    }
	
	// Update is called once per frame
	void Update () {
        ValidCheck(playerGrid);
        Debug.Log(ValidCheck(playerGrid));
	}

    //인풋 좌표와 게임 판 하고 겹치는지 확인
    bool ValidCheck(int[,] grid)
    {
        //GetLength(X)로 X차원의 길이를 젤 수 있다.
        //Debug.Log("Length = " + grid.GetLength(1));
        //Debug.Log("Height = " + grid.GetLength(0));

        for (int x = 0 ; x < grid.GetLength(1); x += 1)
        {
            for (int y =0; y < grid.GetLength(0); y += 1)
            {
                //인풋 좌표에서 1값이 있는 칸에
                if (grid[y,x] != 0)
                {
                    Debug.Log("(" + y + "," + x + ")");
                    //게임 판이 차지하고 있음면 거짓
                    if (baseGrid[y, x] != 0)
                        return false;
                    //게임 판이 비어있으면 참
                    else
                        return true;
                    
                }
            }
        }

        return false;
    }
}
