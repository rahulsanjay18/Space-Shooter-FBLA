using System.Collections;
using System.Collections.Generic;

public class Scorer : System.IComparable<Scorer>
{

    public string name { get; set; }    // name of player
    public int score { get; set; }      // their score

    // default constructor
    public Scorer()
    {
        this.name = null;
        this.score = 0;
    }

    // parameterized constructor
    public Scorer(string n, int s)
    {
        this.name = n;
        this.score = s;
    }

    // allows for sorting to occur
    public int CompareTo(Scorer other)
    {
        if(other == null)
        {
            return -1;
        }else
        {
            return other.score.CompareTo(this.score);
        }
    }
}
