using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int gridHeight;
    public int gridLength;

    public float gameSpeed;
    public float holdTime;
    private float _timeCounter;
    private float _holdCounter;

    private BoardManager boardScript;

    public TetrisGrid levelGrid;
    private TetrisGrid mainGrid;
    private TetrisGrid playerGrid;




    // Use this for initialization
    void Awake() {
        boardScript = GetComponent<BoardManager>();
	}

    void Start()
    {
        //levelGrid = null;
        playerGrid = new TetrisGrid(gridHeight, gridLength);
        playerGrid.grid = new int[3, 3] { { 0, 0, 1 }, { 0, 0, 1 }, { 0, 0, 1 } };
        mainGrid = new TetrisGrid(playerGrid.gridHeight, playerGrid.gridLength);
        mainGrid.grid = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        boardScript.BoardSetup();
    }
	
	// Update is called once per frame
	void Update () {

        //일정 시간 마다
        if (_timeCounter >= gameSpeed)
        {
            //블록이 내려가고
            playerGrid.MoveDown(mainGrid);
            Debug.Log(playerGrid.canMoveDown);
            //못 내려갈 경우
            if (playerGrid.canMoveDown == false)
            {
                //다음 페이즈로 넘어간다.
                Debug.Log("EndPhase");
                EndPhase();
                _timeCounter = 0f;
                return;
            }

            _timeCounter = 0f;
        }

        //왼쪽 키 누를 경우
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Pressed left");
            playerGrid.MoveLeft(mainGrid);    
        }
        //오른쪽 키 누를 경우
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Pressed Right");
            playerGrid.MoveRight(mainGrid);
        }

        boardScript.BoardUpdate(mainGrid, playerGrid);
        _timeCounter += Time.deltaTime;
	}

    public void StartGame()
    {
        boardScript.BoardSetup();
        boardScript.BoardUpdate(mainGrid, playerGrid);
        playerGrid.MoveLeft(mainGrid);
    }

    private void EndPhase()
    {

    }
}
