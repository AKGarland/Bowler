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
        List<int> T01 = new List<int>( );
        T01.Add(10);
        ActionMaster actionMaster = new ActionMaster( );
        Assert.AreEqual(endTurn, actionMaster.Bowl(T01));
    }

    [Test]
    public void T02Bowl8ReturnsTidy( )
    {
        List<int> T02 = new List<int>( );
        T02.Add(8);
        ActionMaster actionMaster = new ActionMaster( );
        Assert.AreEqual(tidy, actionMaster.Bowl(T02));
    }

    [Test]
    public void T03Bowl28SpareReturnsEndTurn( )
    {
        ActionMaster actionMaster = new ActionMaster( );
        List<int> T03 = new List<int>( );
        T03.Add(2);
        actionMaster.Bowl(T03);
        T03.Add(8);
        Assert.AreEqual(endTurn, actionMaster.Bowl(T03));
    }

    [Test]
    public void T04CheckResetAtSpareInLastFrame( )
    {
        ActionMaster actionMaster = new ActionMaster( );
        List<int> T04 = new List<int>( );
        int[] rolls = {1,1,1,1,1,1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1}; // 19 bowls
        foreach (int roll in rolls)
        {
            T04.Add(roll);
            actionMaster.Bowl(T04);
        }
        T04.Add(9);
        Assert.AreEqual(reset, actionMaster.Bowl(T04)); // 20th bowl makes a spare
    }

    [Test]
    public void T05CheckResetAtStrikeInLastFrame( ) {
        ActionMaster actionMaster = new ActionMaster( );
        List<int> T05 = new List<int>( );
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, }; // 18
        foreach (int roll in rolls)
        {
            T05.Add(roll);
            actionMaster.Bowl(T05);
        }
        T05.Add(10);
        Assert.AreEqual(reset, actionMaster.Bowl(T05));
    }

    [Test]
    public void T06YouTubeRollsEndInEndGame( ) // end score was 170 @Scoring Bowling by Stephen Anderson
    {
        ActionMaster actionMaster = new ActionMaster( );
        List<int> T06 = new List<int>( );
        int[] rolls = { 8,2,7,3,3,4,10,2,8,10,10,8,0,10,8,2,9}; 
        foreach (int roll in rolls)
        {
            T06.Add(roll);
            actionMaster.Bowl(T06);
        }
        Assert.AreEqual(endGame, actionMaster.Bowl(T06));
    }

    [Test]
    public void T07NoBowl21AwardedReturnsEndGame( ) // if bowls 19 + 20 does not come to "10" then game is over
    {
        ActionMaster actionMaster = new ActionMaster( );
        List<int> T07 = new List<int>( );
        int[] rolls = { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8}; 
        foreach (int roll in rolls)
        {
            T07.Add(roll);
            actionMaster.Bowl(T07);
        }
        T07.Add(1);
        Assert.AreEqual(endGame, actionMaster.Bowl(T07));
    }

    [Test]
    public void T08TidyAfter20thBowlIf19thisStrike( ) 
    {
        ActionMaster actionMaster = new ActionMaster( );
        List<int> T08 = new List<int>( );
        int[] rolls = { 1,1, 1,1, 1,1, 1, 1, 1, 1, 1, 1, 1,1, 1,1, 1,1, 10, 1};
        foreach (int roll in rolls)
        {
            T08.Add(roll);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(T08));
    }

    [Test]
    public void T09SymptomTestOddTurns0ThenTenIncrementBowl2( )
    {
        ActionMaster actionMaster = new ActionMaster( );
        List<int> T09 = new List<int>( );
        T09.Add(0);
        T09.Add(10);
        T09.Add(7);
        Assert.AreEqual(tidy, actionMaster.Bowl(T09));
    }

    [Test]
    public void T10Dondi10thFrameTurkey( )
    {
        ActionMaster actionMaster = new ActionMaster( );
        List<int> T10 = new List<int>( );
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
        foreach (int roll in rolls)
        {
            T10.Add(roll);
            actionMaster.Bowl(T10);
        }
        T10.Add(10);
        Assert.AreEqual(reset, actionMaster.Bowl(T10));
        T10.Add(10);
        Assert.AreEqual(reset, actionMaster.Bowl(T10));
        T10.Add(10);
        Assert.AreEqual(endGame, actionMaster.Bowl(T10));
    }

    [Test]
    public void T11ZeroOneGivesEndTurn( )
    {
        ActionMaster actionMaster = new ActionMaster( );
        List<int> T11 = new List<int>( );
        T11.Add(0);
        actionMaster.Bowl(T11);
        T11.Add(1);
        Assert.AreEqual(endTurn, actionMaster.Bowl(T11));
    }
}


