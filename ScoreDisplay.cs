using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Linq;

public class ScoreDisplay : MonoBehaviour {
    public Text runningTotal;
    public Text[] bowlsText,frameText;

	// Use this for initialization;
	void Start () {
        runningTotal.text = "0";
        bowlsText[0].text = "X";
	}

    public void FillRolls(List<int> rolls)
    {
        string scoreString = FormatRolls(rolls);
        for (int i = 0; i < scoreString.Length; i++)
        {
            bowlsText[i].text = scoreString[i].ToString( );
        }
    }

    public void TotalScore(List<int> scores)
    {
        runningTotal.text = scores.ElementAt(scores.Count - 1).ToString( );
    }

    public void FillFrames(List<int> frames)
    {
        for (int i = 0; i < frames.Count; i++)
        {
            frameText[i].text = frames[i].ToString( );
        }
    }

    public static string FormatRolls(List<int> rolls){
        string output = "";

        for (int i = 0; i < rolls.Count; i++) {
            int miniFrame = output.Length + 1;

            if (rolls[i] == 0)
            {  //display "-" in place of a "0"
                output+="-";
            }
            else if (miniFrame % 2 == 0 && (rolls[i - 1] + rolls[i] == 10)) // Spare 
            {
                output += "/";
            }
            else if (miniFrame >= 19 && rolls[i] == 10)
            {
                output += "X";
            }
            else if (rolls[i] == 10)
            {
                output += "X ";
            }
            else
            {
                output += rolls[i].ToString( );
            }
        }
        return output;
    }
}
