using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuManager : MonoBehaviour {

    public Image winnerImage;
    public Sprite player1Wins;
    public Sprite player2Wins;

    private GameManager1 gm;

    // Use this for initialization
    void Start () {
        gm = FindObjectOfType<GameManager1>();
        //player1Wins = Resources.Load<Sprite>("player1wins");
        //player2Wins = Resources.Load<Sprite>("p1wins_0");
        Debug.Log("LevelMenuManager Started.");
        Debug.Log(gm.winnerId);
        if (gm.winnerId == 1)
        {
            Debug.Log("Player 1 wins.");
            winnerImage.sprite = player1Wins;
        } else if (gm.winnerId == 2)
        {
            Debug.Log("Player 2 wins.");
            winnerImage.sprite = player2Wins;
        }
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(gm.winnerId);
    }
}
