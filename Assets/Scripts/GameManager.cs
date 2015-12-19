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

    private enum holdKey { Null, Left, Right }
    private holdKey _isHolding;


    // Use this for initialization
    void Awake() {
        boardScript = GetComponent<BoardManager>();
	}

    void Start()
    {
        //[임시]levelGrid 아직 미구현
        levelGrid = new TetrisGrid(gridHeight, gridLength);
        levelGrid.grid = new int[20, 10] {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};

        //levelGrid에서 받은 정보 적용, gridHeight 와 gridLength 설정
        mainGrid = levelGrid;
        this.gridHeight = mainGrid.gridHeight;
        this.gridLength = mainGrid.gridLength;
        
        //[임시]블록 생성 미구현이므로 수동 설정
        playerGrid = new TetrisGrid(gridHeight, gridLength);
        playerGrid.grid = new int[20, 10] {
            { 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};

        boardScript.BoardSetup();
    }
	
	// Update is called once per frame
	void Update () {

        //일정 시간 마다
        if (_timeCounter >= gameSpeed)
        {
            //카운터 초기화
            _timeCounter = 0f;

            //블록이 내려가고
            playerGrid.MoveDown(mainGrid);
            
            //못 내려갈 경우
            if (playerGrid.canMoveDown == false)
            {
                //다음 페이즈로 넘어간다.
                EndPhase();
                
                //다른 input무시
                return;
            }
        }

        ////컨트롤 관련

        UpdateHoldingKey();
        //Debug.Log(_isHolding);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //블록 회전
        }
        
        //[임시]아래키 홀딩 구현 필요
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerGrid.MoveDown(mainGrid);
            //내려갈시 카운터 초기화
            if (playerGrid.canMoveDown == true)
                _timeCounter = 0f;
        }

        if (Input.GetKey(KeyCode.RightArrow) && (_isHolding == holdKey.Right))
        {
            if(holdTime <= _holdCounter)
            {
                playerGrid.MoveRight(mainGrid);
                _holdCounter = 0f;
            }

            _holdCounter += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && (_isHolding == holdKey.Left))
        {
            if (holdTime <= _holdCounter)
            {
                playerGrid.MoveLeft(mainGrid);
                _holdCounter = 0f;
            }

            _holdCounter += Time.deltaTime;
        }


        ////컨트롤 관련 End

        //변경사항 그래픽 적용
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

    private void UpdateHoldingKey()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //동시 인풋시 무시
            if (Input.GetKeyDown(KeyCode.RightArrow))
                return;

            //홀딩시간 초기화
            _holdCounter = holdTime;

            _isHolding = holdKey.Left;
            return;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //동시 인풋시 무시
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                return;

            //홀딩시간 초기화
            _holdCounter = holdTime;

            _isHolding = holdKey.Right;
            return;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            //홀딩시간 초기화
            _holdCounter = holdTime;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                _isHolding = holdKey.Right;
                return;
            }

            _isHolding = holdKey.Null;
            return;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            //홀딩시간 초기화
            _holdCounter = holdTime;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _isHolding = holdKey.Left;
                return;
            }

            _isHolding = holdKey.Null;
            return;
        }
    }
}
