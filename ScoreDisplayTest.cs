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
}