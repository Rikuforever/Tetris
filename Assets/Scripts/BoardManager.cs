using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

    /*
    GameObject
    */
    public GameObject blockTile;

    /*
    GameManager
    */
    private GameManager gameScirpt;

    /*
    GameObject
    */
    private GameObject _blockInstance;
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
    }


    public void BoardSetup(int[,] levelGrid)
    {
        
        //기존에 있는 Board 묶음 지우기
        Destroy(GameObject.Find("Board"));
        //Instantiate Board and set boardHolder to its transform.
        _boardHolder = new GameObject("Board").transform;

        for (int y = 0; y < gameScirpt.gridHeight; y++)
        {
            for(int x = 0; x< gameScirpt.gridLength; x++)
            {
                GameObject newObject = Instantiate(blockTile, new Vector3(x,-y,0f), Quaternion.identity) as GameObject;
                BlockInfo _blockInstance = newObject.GetComponent<BlockInfo>();
                _blockInstance.x = x;
                _blockInstance.y = y;

                //디자인된 레벨 대로 구현, NULL인 경우 빈공간 생성
                if (levelGrid != null && levelGrid[y, x] != 0)
                {
                    newObject.SetActive(true);
                }
                else
                {
                    newObject.SetActive(false);
                }

                _blockInstance.transform.SetParent(_boardHolder);
            }
        }       

    }

    public void BoardUpdate(int[,] mainGrid, int[,] playerGrid)
    {
        
        //_boardGrid와 비교하여 변경사항 있으면 적용
        for (int y = 0; y < gameScirpt.gridHeight; y++)
        {
            for(int x = 0; x < gameScirpt.gridLength; x++)
            {
                int temp = mainGrid[y, x] + playerGrid[y, x];

                if(_boardGrid[y,x] != temp)
                {
                    _boardGrid[y, x] = temp;
                }
            }
        }
    }

}
