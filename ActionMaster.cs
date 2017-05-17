using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster { // Removed Monobehaviour as this is our own plain class
    public enum Action {Tidy, Reset, EndTurn, EndGame};

    private int[] bowls = new int[21];
    private int bowl = 1;

    public Action Bowl (int pins)
    {
        if (pins < 0 || pins > 10) {throw new UnityException("Invalid pins.");}

        bowls[bowl - 1] = pins;

       if (bowl == 21) // if bowls 19 + 20 does not come to "10" then game is over.
        {
            return Action.EndGame;
        }

       // Last frame special cases
        if ((bowl == 19 && Bowl21Awarded( )) || (bowl == 20 && Bowl21Awarded( ) && bowls[19]==10))
        {
            bowl += 1;
            return Action.Reset;
        }
        else if (bowl >= 19 && Bowl21Awarded( ) && bowls[18] < 10)
        {
            bowl += 1;
            return Action.Reset;
        }
        else if (bowl == 20 && (bowls[18] == 10 && bowls[19] != 10) && Bowl21Awarded( ))
        {
            return Action.Tidy;
        }
        else if (bowl == 20)  // Game ends at bowl 20
        {
            return Action.EndGame;
        }

        if (bowl % 2 != 0)  // If bowl is an odd number it must be the first bowl of the frame 
        {
            if (pins == 10 && bowl < 19)
            {
                bowl += 2;
                return Action.EndTurn;
            }
            else
            {
                bowl += 1;
                return Action.Tidy;
            }
        } else if (bowl %2 == 0) {  // Second bowl of frame
            bowl += 1;
            return Action.EndTurn;
        }

        throw new UnityException("Not sure which action to return.");
    }

    private bool Bowl21Awarded( )
    {
        return(bowls[19-1] + bowls[20-1] >=10 );
    }
}
