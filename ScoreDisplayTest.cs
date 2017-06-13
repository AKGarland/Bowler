using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Linq;

[TestFixture]
public class ScoreDisplayTest
{
    [Test]
    public void PassingTest( )
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01Bowl1( )
    {
        int[] rolls = { 1 };
        string rollsString = "1";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList( )));
    }

    [Test]
    public void T02Bowl1and2( )
    {
        int[] rolls = { 1, 2 };
        string rollsString = "12";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList( )));
    }

    [Test]
    public void T03Bowl10( )
    {
        int[] rolls = { 10 };
        string rollsString = "X ";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList( )));
    }

    [Test]
    public void T04Bowl3and0( )
    {
        int[] rolls = { 3,0 };
        string rollsString = "3-";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList( )));
    }

    [Test]
    public void T05Bowl10and4( )
    {
        int[] rolls = { 10 , 4};
        string rollsString = "X 4";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList( )));
    }

    [Test]
    public void T06Bowl1and9( )
    {
        int[] rolls = { 1, 9 };
        string rollsString = "1/";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList( )));
    }

    [Test]
    public void T07BowlStrikeinLastFrame( )
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 1, 1 };
        string rollsString = "111111111111111111X11";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList( )));
    }

    [Test]
    public void T08BowlStrikeOnSecondBowl( )
    {
        int[] rolls = { 0, 10, 3, 4 };
        string rollsString = "-X34";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList( )));
    }

    [Category("Verification")]
    [Test]
    public void TG01GoldenCopyB1of3( )
    {
        int[] rolls = { 10, 9, 1, 9, 1, 9, 1, 9, 1, 7, 0, 9, 0, 10, 8, 2, 8, 2, 10 };
        string rollsString = "X 9/9/9/9/7-9-X 8/8/X";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList( )));
    }
}