using System;

[Serializable]
public class PlayerData
{
    public int id;
    public string name;
    public int strength;
    public int contribution;
    public int timesThisWeek;

    public PlayerData(string name, int timeThisWeek)
    {
        this.name = name;
        this.timesThisWeek = timeThisWeek;
    }

    public PlayerData(string name, int strength, int contribution, int timesThisWeek)
    {
        this.name = name;
        this.strength = strength;
        this.contribution = contribution;
        this.timesThisWeek = timesThisWeek;
    }
}
