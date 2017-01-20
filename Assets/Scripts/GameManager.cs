using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum CurrentMode { Auto, Manual};

public class GameManager : MonoBehaviour
{

    public int gridHeight;
    public int gridLength;
    public int score;
    public int perLineScore;
    public int level;

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

    // Test
    private InputManager inputManager;


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

        // Intialize InputManager
        inputManager = new InputManager();

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
            mainGrid.MergeGrid(playerGrid);
            // Update Score UI
            score += mainGrid.ShiftLine() * perLineScore;
            textUI.text = "Score : " + score.ToString();
            boardScript.BoardUpdate(mainGrid, null);
            _isPhaseEnd = false;
            _isPhaseStart = true;
            return;
        }

        // Update Input
        inputManager.UpdateInput();

        if (inputManager.inputSoftDrop) {
            playerGrid.MoveDown(mainGrid, out _validCheck);

            //못 내려갈 경우
            if (_validCheck == false) {
                //다음 페이즈로 넘어간다.
                _isPhaseEnd = true;

                //다른 input무시
                return;
            }
        }

        if (inputManager.inputRight)
            playerGrid.MoveRight(mainGrid, out _validCheck);

        if (inputManager.inputLeft)
            playerGrid.MoveLeft(mainGrid, out _validCheck);

        if (inputManager.inputTurn)
            playerGrid.Turn(mainGrid);

        if (inputManager.inputHardDrop) {
            playerGrid.MoveButtom(mainGrid);
            _isPhaseEnd = true;
        }

        //변경사항 그래픽 적용
        boardScript.BoardUpdate(mainGrid, playerGrid);
    }

    public void StartGame()
    {
        boardScript.BoardSetup();
        boardScript.BoardUpdate(mainGrid, playerGrid);
    }

}
