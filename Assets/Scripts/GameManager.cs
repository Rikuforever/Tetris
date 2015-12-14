using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int gridHeight;
    public int gridLength;

    private BoardManager boardScript;
    private TetrisGrid mainGrid;

    public TetrisGrid levelGrid;


    // Use this for initialization
    void Start() {
        boardScript = GetComponent<BoardManager>();
        //levelGrid = null;
        mainGrid = new TetrisGrid(gridHeight, gridLength);
        mainGrid.grid = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        levelGrid = new TetrisGrid(gridHeight, gridLength);
        levelGrid.grid = new int[3, 3] { { 0, 0, 1 },{ 0,0,1},{ 0,0,1} };
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
        boardScript.BoardSetup();
        boardScript.BoardUpdate(mainGrid, levelGrid);
        levelGrid.MoveLeft(mainGrid);
    }
}
