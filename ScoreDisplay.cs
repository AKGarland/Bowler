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
        var rollList = new List<string>( );

        for (int i = 0; i < rolls.Count; i++)
        {
            if (rolls[i] == 10) //if this first bowl is a 10 display X
            {
                rollList.Add("X");
                if (i % 2 == 0)
                {
                    rollList.Add(" ");
                }
            }
            else if (rolls[i] == 0){ //display "-" in place of a "0"
                rollList.Add("-");
            } else
            {
                if (i%2==1 && (rolls[i - 1] + rolls[i] == 10))
                {
                    rollList.Add("/");
                }
                else
                {
                    rollList.Add(rolls[i].ToString( ));
                }
            }
        }
        output = string.Join("",rollList.ToArray());

        return output;
    }
}
