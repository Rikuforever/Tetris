using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public int gridHeight;
    public int gridLength;
    public int score;
    public int perLineScore;
    public int level;

    public float gameSpeed;
    public float holdTime;
    private float _timeCounter;
    private float _RLholdCounter;
    private float _DholdCounter;

    private bool _validCheck;
    private bool _isPhaseStart;
    private bool _isPhaseEnd;
    private bool _isGameEnd;

    private BoardManager boardScript;

    public TetrisGrid levelGrid;
    private TetrisGrid mainGrid;
    private TetrisGrid playerGrid;

    public Text textUI;

    private enum holdKey { Null, Left, Right }
    private holdKey _isHolding;


    // Use this for initialization
    void Awake()
    {
        boardScript = GetComponent<BoardManager>();
    }

    void Start()
    {
        // Initialize UI
        textUI.text = "Score : 0";

        // Initialize Parameters
        _validCheck = true;
        _isPhaseStart = false;
        _isPhaseEnd = false;
        _isGameEnd = false;

        //[임시]levelGrid 아직 미구현
        levelGrid = new TetrisGrid(gridHeight, gridLength, blockType.Main, 0, 0, 0);
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
       
        playerGrid = TetrisGrid.GenerateBlock(this.gridHeight, this.gridLength);

        //보드 초기화
        boardScript.BoardSetup();
    }

    // Update is called once per frame
    void Update()
    {
        // 게임 오버 처리
        if(_isGameEnd == true)
        {
            textUI.text = "GAME OVER";
            return;
        }

        // 페이즈 시작 처리
        if (_isPhaseStart == true)
        {
            playerGrid = TetrisGrid.GenerateBlock(this.gridHeight, this.gridLength);
            //게임오버 확인
            if (!playerGrid.ValidCheck(mainGrid))
            {
                _isGameEnd = true;
                return;
            }
            boardScript.BoardUpdate(mainGrid, playerGrid);
            _isPhaseStart = false;
            return;
        }

        // 페이즈 마무리 처리
        if (_isPhaseEnd == true)
        {
            _timeCounter = 0f;
            //합체
            mainGrid.MergeGrid(playerGrid);
            score += mainGrid.ShiftLine() * perLineScore;
            textUI.text = "Score : " + score.ToString();
            boardScript.BoardUpdate(mainGrid, null);
            _isPhaseEnd = false;
            _isPhaseStart = true;
            return;
        }

        //일정 시간 마다
        if (_timeCounter >= gameSpeed)
        {
            //카운터 초기화
            _timeCounter = 0f;

            //블록이 내려가고
            playerGrid.MoveDown(mainGrid, out _validCheck);

            //못 내려갈 경우
            if (_validCheck == false)
            {
                //다음 페이즈로 넘어간다.
                _isPhaseEnd = true;

                //다른 input무시
                return;
            }
        }
        //

        ////컨트롤 관련

        UpdateHoldingKey();

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (holdTime <= _DholdCounter)
            {
                playerGrid.MoveDown(mainGrid, out _validCheck);
                _DholdCounter = 0;
            }
            else
                _DholdCounter += Time.deltaTime;
            //내려갈시 카운터 초기화
            if (_validCheck == true)
                _timeCounter = 0f;
        }

        if (Input.GetKey(KeyCode.RightArrow) && (_isHolding == holdKey.Right))
        {
            if (holdTime <= _RLholdCounter)
            {
                playerGrid.MoveRight(mainGrid, out _validCheck);
                _RLholdCounter = 0f;
            }
            else
                _RLholdCounter += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && (_isHolding == holdKey.Left))
        {
            if (holdTime <= _RLholdCounter)
            {
                playerGrid.MoveLeft(mainGrid, out _validCheck);
                _RLholdCounter = 0f;
            }
            else
                _RLholdCounter += Time.deltaTime;
        }

        // 회전
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerGrid.Turn(mainGrid);
        }

        // 박기
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerGrid.MoveButtom(mainGrid);
            _isPhaseEnd = true;

            //다른 input무시
            return;
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
    }


    private void UpdateHoldingKey()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //홀딩시간 초기화
            _DholdCounter = holdTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //동시 인풋시 무시
            if (Input.GetKeyDown(KeyCode.RightArrow))
                return;

            //홀딩시간 초기화
            _RLholdCounter = holdTime;

            _isHolding = holdKey.Left;
            return;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //동시 인풋시 무시
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                return;

            //홀딩시간 초기화
            _RLholdCounter = holdTime;

            _isHolding = holdKey.Right;
            return;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            //홀딩시간 초기화
            _RLholdCounter = holdTime;

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
            _RLholdCounter = holdTime;

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
