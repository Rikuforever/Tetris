using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

    /*
    GameObject
    */
    public GameObject[] blockTiles;

    /*
    GameManager
    */
    private GameManager gameScirpt;

    /*
    GameObject
    */
    //왜 Transform?
    private Transform _boardHolder;

    /*
    Int Array
    */
    private int[,] _boardGrid;

    /*
    Functions
    */

    void Awake()
    {   
        //레퍼런스 받아오기??
        gameScirpt = GetComponent<GameManager>();

        //배열크기 제한
        _boardGrid = new int[gameScirpt.gridHeight, gameScirpt.gridLength];
    }


    public void BoardSetup()
    {
        //_boardGrid 초기화
        _boardGrid = new int[gameScirpt.gridHeight, gameScirpt.gridLength];

        //기존에 있는 Board 묶음 지우기
        Destroy(GameObject.Find("Board"));
        //Instantiate Board and set boardHolder to its transform.
        _boardHolder = new GameObject("Board").transform;

        for (int y = 0; y < gameScirpt.gridHeight; y++)
        {
            for(int x = 0; x< gameScirpt.gridLength; x++)
            {
                BlockCreate(x, y);
            }
        }

        //Board 오브젝의 inactive 한 Child들을 검색하기 위해 검색해 놓는다.
        _boardHolder.GetComponentInChildren(typeof(Transform));       

    }

    //모든 블록들의 상태을 바꾸는 함수
    public void BoardUpdate(TetrisGrid mainGrid, TetrisGrid playerGrid)
    {        
        //_boardGrid와 비교하여 변경사항 있으면 적용
        for (int y = 0; y < gameScirpt.gridHeight; y++)
        {
            for(int x = 0; x < gameScirpt.gridLength; x++)
            {
                int temp = mainGrid.grid[y, x] + playerGrid.grid[y, x];

                if(_boardGrid[y,x] != temp)
                {
                    _boardGrid[y, x] = temp;
                    BlockUpdate(x, y, temp);
                }
            }
        }
    }

    //한 블록의 상태을 바꾸는 함수
    private void BlockUpdate(int x, int y, int value)
    {
        string findName = "block" + x + y;
        Transform childXY = _boardHolder.Find(findName);

        //해당 좌표의 모든 오브젝 끄고
        for(int a = 0; a < childXY.childCount; a++)
        {
            childXY.GetChild(a).gameObject.SetActive(false);
        }

        //비어 있는 칸인 경우 여기서 끊는다.
        if (((blockColor)value) == blockColor.Null)
            return;

        //해당 좌표의 특정 색 오브젝 켜기
        childXY.Find(((blockColor)value).ToString()).gameObject.SetActive(true);
    }

    //한 블록을 생성하는 함수
    private void BlockCreate(int x, int y)
    {
        //좌표그룹화에 쓸 오브젝트 생성
        GameObject tempObject = new GameObject();
        tempObject.name = "block" + x + y;

        //색깔 별로
        for (int c = 0; c < (int)blockColor.NUM_COLORS; c++)
        {      
            GameObject newObject = Instantiate(blockTiles[c], new Vector3(x, -y, 0f), Quaternion.identity) as GameObject;
            //int에서 enum으로 변환하는 방법!!
            newObject.name = ((blockColor)c).ToString();
            newObject.SetActive(false);

            //만든 오브젝트를 좌표그룹에 집어 넣음
            newObject.transform.SetParent(tempObject.transform);
        }

        //메인 그룹에 좌표그룹 포함
        tempObject.transform.SetParent(_boardHolder);
    }

}
