using UnityEngine;
using System.Collections;

public class TetrisGrid : MonoBehaviour {

    //Grid 높이와 너비
    public int gridHeight;
    public int gridLength;

    //게임 전체 판 (글로벌)
    //{[0,0],[0,1],[0,2],[0,3],
    //[1,0],[1,1],[1,2],[1,3],
    public int[,] baseGrid;

    //플레이어가 컨트롤중인 블록 위치 값
    //문제가 회전을 하기 위해서는 위치 값뿐만 아니라 중심과 종류 값도 필요하다. Class개념을 배워야하지 않을까?
    private int[,] playerGrid;

    // Use this for initialization
    void Start() {
        baseGrid = new int[gridHeight, gridLength];
        playerGrid = new int[gridHeight, gridLength];
        //Debug용_Start
        //baseGrid = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        //playerGrid = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 1, 1, 1 } };
        //Debug용_End
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(baseGrid[2, 0] + baseGrid[2, 1] + baseGrid[2, 2]);
        Debug.Log("ValidCheck = " + ValidCheck(playerGrid));
        TimerNext(playerGrid);
        Debug.Log(baseGrid[2,0] + baseGrid[2, 1] + baseGrid[2, 2]);
	}

    /// <summary>
    /// 해당 배치를 게임 판에 적용해도 되는지 여부조사
    /// <para/>True : 겹치지 않는다
    /// <para>False : 겹친다</para>
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
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
                    //Debug.Log("(" + y + "," + x + ")");
                    //게임 판이 칸을 이미 차지하고 있음면 거짓
                    if (baseGrid[y, x] != 0)
                        return false;
                    //게임 판의 칸이 비어있으면 참
                    else
                        return true;                    
                }
            }
        }

        return false;
    }

    /// <summary>
    /// 타이머 지나고 난후 그리드 처리
    /// </summary>
    /// <param name="grid"></param>
    /// <param name=""></param>
    void TimerNext(int[,] grid)
    {
        //플레이어 블록이 더 내려갈 수 있는 여부 확인
        //플레이어 블록이 맨 아랫줄에 있는가?
        for(int x = 0; x < gridLength; x += 1)
        {
            //있는 경우 플레이어 블록을 게임 블록에 적용하고 새로운 블록을 생성한다.
            if (grid[(gridHeight-1),x] != 0)
            {
                for(int yy = 0; yy < gridHeight; yy += 1)
                {
                    for(int xx = 0; xx < gridLength; xx += 1)
                    {
                        baseGrid[yy, xx] = baseGrid[yy, xx] + grid[yy, xx];
                    }
                }
                //새로운 블록 생성 함수 (playerGrid 초기화, 새로운 블록 적용)
                return;
            }

        }
        //맨 아랫줄에 없는 경우, 블록을 한칸 내릴 수 있는지 확인한다.
        return;
    }
}
