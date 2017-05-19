using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreDisplay : MonoBehaviour {
    public Text stframe;
	// Use this for initialization;
	void Start () {
        stframe = GetComponentInChildren<Text>( );
        stframe.text = "0";
	}
	
	// Update is called once per frame;
	void Update () {
		
	}

    public void Scores(List<int> scores)
    {
        stframe.text = scores.ElementAt(scores.Count-1).ToString();
    }

    public void Score(int score)
    {
        stframe.text = score.ToString( );
    }
}
