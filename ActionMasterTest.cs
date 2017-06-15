using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Linq;

[TestFixture]
public class ActionMasterTest
{
    //private ActionMaster actionMaster;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;

    [Test]
    public void PassingTest( )
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn( )
    {
        List<int> T01 = new List<int>( );
        T01.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(T01));
    }

    [Test]
    public void T02Bowl8ReturnsTidy( )
    {
        List<int> T02 = new List<int>( );
        T02.Add(8);
        Assert.AreEqual(tidy, ActionMaster.NextAction(T02));
    }

    [Test]
    public void T03Bowl28SpareReturnsEndTurn( )
    {
        List<int> T03 = new List<int>( );
        T03.Add(2);
        ActionMaster.NextAction(T03);
        T03.Add(8);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(T03));
    }

    [Test]
    public void T04CheckResetAtSpareInLastFrame( )
    {
        int[] rolls = {1,1,1,1,1,1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1,9}; // 19 bowls
        Assert.AreEqual(reset, ActionMaster.NextAction(rolls.ToList())); // 20th bowl makes a spare
    }

    [Test]
    public void T05CheckResetAtStrikeInLastFrame( ) {
        List<int> T05 = new List<int>( );
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, }; // 18
        foreach (int roll in rolls)
        {
            T05.Add(roll);
            ActionMaster.NextAction(T05);
        }
        T05.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(T05));
    }

    [Test]
    public void T06YouTubeRollsEndInEndGame( ) // end score was 170 @Scoring Bowling by Stephen Anderson
    {
        List<int> T06 = new List<int>( );
        int[] rolls = { 8,2,7,3,3,4,10,2,8,10,10,8,0,10,8,2,9}; 
        foreach (int roll in rolls)
        {
            T06.Add(roll);
            ActionMaster.NextAction(T06);
        }
        Assert.AreEqual(endGame, ActionMaster.NextAction(T06));
    }

    [Test]
    public void T07NoBowl21AwardedReturnsEndGame( ) // if bowls 19 + 20 does not come to "10" then game is over
    {
        List<int> T07 = new List<int>( );
        int[] rolls = { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8}; 
        foreach (int roll in rolls)
        {
            T07.Add(roll);
            ActionMaster.NextAction(T07);
        }
        T07.Add(1);
        Assert.AreEqual(endGame, ActionMaster.NextAction(T07));
    }

    [Test]
    public void T08TidyAfter20thBowlIf19thisStrike( ) 
    {
        List<int> T08 = new List<int>( );
        int[] rolls = { 1,1, 1,1, 1,1, 1, 1, 1, 1, 1, 1, 1,1, 1,1, 1,1, 10, 1};
        foreach (int roll in rolls)
        {
            T08.Add(roll);
        }
        Assert.AreEqual(tidy, ActionMaster.NextAction(T08));
    }

    [Test]
    public void T09SymptomTestOddTurns0ThenTenIncrementBowl2( )
    {
        List<int> T09 = new List<int>( );
        T09.Add(0);
        T09.Add(10);
        T09.Add(7);
        Assert.AreEqual(tidy, ActionMaster.NextAction(T09));
    }

    [Test]
    public void T10Dondi10thFrameTurkey( )
    {
        List<int> T10 = new List<int>( );
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
        foreach (int roll in rolls)
        {
            T10.Add(roll);
            ActionMaster.NextAction(T10);
        }
        T10.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(T10));
        T10.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(T10));
        T10.Add(10);
        Assert.AreEqual(endGame, ActionMaster.NextAction(T10));
    }

    [Test]
    public void T11ZeroOneGivesEndTurn( )
    {
        List<int> T11 = new List<int>( );
        T11.Add(0);
        ActionMaster.NextAction(T11);
        T11.Add(1);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(T11));
    }
}


