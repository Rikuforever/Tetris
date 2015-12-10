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
    private Transform _boradHolder;

    /*
    Int Array
    */
    private int[,] _boardGrid;

    /*
    Functions
    */

    void Awake()
    {
        //레퍼런스 받아오기
        gameScirpt = GetComponent<GameManager>();
    }


    public void BoardSetup(int[,] levelGrid)
    {
        //레벨그리드가 비어 있으면 빈 그리드 생성
        if (levelGrid == null)
        {
            for(int y = 0; y < gameScirpt.gridHeight; y++)
            {
                for(int x = 0; x< gameScirpt.gridLength; x++)
                {
                    GameObject newObject = Instantiate(blockTile, new Vector3(x,-y,0f), Quaternion.identity) as GameObject;
                    BlockInfo _blockInstance = newObject.GetComponent<BlockInfo>();
                    _blockInstance.x = x;
                    _blockInstance.y = y;
                    newObject.SetActive(false);
                }
            }
        }

        //레밸그리드가 채워져 있으면 그 블록 Active 상태로 생성
    }



}
