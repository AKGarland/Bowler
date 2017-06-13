using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Linq;

public class ScoreDisplay : MonoBehaviour {
    public Text runningTotal;
    public Text[] bowlsText,frameText;

    public void FillRolls(List<int> rolls)
    {
        string scoreString = FormatRolls(rolls);
        for (int i = 0; i < scoreString.Length; i++)  
        {
            bowlsText[i].text = scoreString[i].ToString( );  // Iterating through each Text box on the bowls part of the scoring frames, this fills in the first box with the first score and so on.
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
            frameText[i].text = frames[i].ToString( ); // Iterating through frame score boxes at the bottom, this assigns the ith saved frame int to the ith frame as per Unity Inspector
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
            else if ((miniFrame % 2 == 0 || miniFrame == 21)&& (rolls[i - 1] + rolls[i] == 10)) // Spare displays in the second bowl of a frame AND ALSO if a spare is detected in frame 21 (third, not caught by the even number checker)
            {
                output += "/";
            }
            else if ((miniFrame >= 19 && rolls[i] == 10)) // Strike displays in the final frame as just X
            {
                output += "X";
            }
            else if (miniFrame % 2 == 1 && rolls[i] == 10) // Strike displays elsewhere with a space afterwards to ensure the correct character is assigned to the correct score box. Strikes cannot be scored in a second frame, they are classed as Spares
            {
                output += "X ";
            }
            else
            {
                output += rolls[i].ToString( ); // Handles normal bowl 
            }
        }
        return output;
    }
}
