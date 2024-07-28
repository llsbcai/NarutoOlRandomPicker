using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Database
{
    public List<PlayerData> playerDataList = new(65);

    public void AddNewItem(PlayerData data)
    {
        playerDataList.Add(data);
    }

    public void ClearAllItem()
    {
        playerDataList.Clear();
    }

    public List<List<PlayerData>> DrawPrize(int orangeCount, int purpleCount)
    {
        if (orangeCount + purpleCount > playerDataList.Count) return null;
        int seed = GetTotalTimes();
        Random ran = new(seed);
        PlayerData[] datas = playerDataList.OrderByDescending(p => p.timesThisWeek + ran.Next(0, 100)/ 100f).ToArray();
        List<List<PlayerData>> list2D = new (2)
        {
            new List<PlayerData>(),
            new List<PlayerData>()
        };
        int index = 0;
        for (int i = 0; i < orangeCount; i++)
        {
            list2D[0].Add(datas[i]);
            index++;
        }

        for (int i = index; i < index + purpleCount; i++)
        {
            list2D[1].Add(datas[i]);
        }
        return list2D;
    }

    private int GetTotalTimes()
    {
        int ret = 0;
        foreach (PlayerData data in playerDataList)
        {
            ret += data.timesThisWeek;
        }
        return ret;
    }

    public string GetDetailThisWeek()
    {
        string ret = "";
        ret += "本周参战人数:" + playerDataList.Count(p => p.timesThisWeek > 0);
        return ret;
    }
}
