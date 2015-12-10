using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int gridHeight;
    public int gridLength;

    private BoardManager boardScript;

    public int[,] levelGrid;


	// Use this for initialization
	void Start () {
        boardScript = GetComponent<BoardManager>();
        levelGrid = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
        boardScript.BoardSetup(levelGrid);
    }
}
