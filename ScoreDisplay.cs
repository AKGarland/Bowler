using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreDisplay : MonoBehaviour {
    public Text runningTotal;
    public Text[] bowlsText,frameText;
	// Use this for initialization;
	void Start () {
        runningTotal.text = "0";
        bowlsText[0].text = "X";
	}
	
	// Update is called once per frame;
	void Update () {
		
	}
    public void FillRollCard(List<int> rolls)
    {
        rolls[1] = -1;
    }

    public void Scores(List<int> scores) // Add framescores to the array
    {
        
    }

    public void TotalScore(List<int> scores)
    {
        runningTotal.text = scores.ElementAt(scores.Count - 1).ToString( );
    }
}
