using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[TestFixture]
public class ActionMasterTest
{
    private ActionMaster actionMaster;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;

    [SetUp]
    public void Setup( )
    {
        ActionMaster actionMaster = new ActionMaster( );
    }

    [Test]
    public void PassingTest( )
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn( )
    {
        ActionMaster actionMaster = new ActionMaster( );
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T02Bowl8ReturnsTidy( )
    {
        ActionMaster actionMaster = new ActionMaster( );
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T03Bowl28SpareReturnsEndTurn( )
    {
        ActionMaster actionMaster = new ActionMaster( );
        actionMaster.Bowl(2);
        Assert.AreEqual(endTurn, actionMaster.Bowl(8));
    }

    [Test]
    public void T04CheckResetAtSpareInLastFrame( )
    {
        ActionMaster actionMaster = new ActionMaster( );
        int[] rolls = {1,1,1,1,1,1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1}; // 19 bowls
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(9)); // 20th bowl makes a spare
    }

    [Test]
    public void T05CheckResetAtStrikeInLastFrame( ) {
        ActionMaster actionMaster = new ActionMaster( );
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }; // 18
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T06YouTubeRollsEndInEndGame( ) // end score was 170 @Scoring Bowling by Stephen Anderson
    {
        ActionMaster actionMaster = new ActionMaster( );
        int[] rolls = { 8,2,7,3,3,4,10,2,8,10,10,8,0,10,8,2}; 
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(endGame, actionMaster.Bowl(9));
    }

    [Test]
    public void T07NoBowl21AwardedReturnsEndGame( ) // if bowls 19 + 20 does not come to "10" then game is over
    {
        ActionMaster actionMaster = new ActionMaster( );
        int[] rolls = { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8}; 
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(endGame, actionMaster.Bowl(1));
    }

    [Test]
    public void T08TidyAfter20thBowlIf19thisStrike( ) 
    {
        ActionMaster actionMaster = new ActionMaster( );
        int[] rolls = { 1,1, 1,1, 1,1, 1, 1, 1, 1, 1, 1, 1,1, 1,1, 1,1, 10};
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(1));
    }

    [Test]
    public void T09SymptomTestOddTurns0ThenTenIncrementBowl2( )
    {
        ActionMaster actionMaster = new ActionMaster( );
        actionMaster.Bowl(0);
        actionMaster.Bowl(10);
        Assert.AreEqual(tidy, actionMaster.Bowl(7));
    }

    [Test]
    public void T10Dondi10thFrameTurkey( )
    {
        ActionMaster actionMaster = new ActionMaster( );
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    [Test]
    public void T11ZeroOneGivesEndTurn( )
    {
        ActionMaster actionMaster = new ActionMaster( );
        actionMaster.Bowl(0);
        Assert.AreEqual(endTurn, actionMaster.Bowl(1));
    }
}


