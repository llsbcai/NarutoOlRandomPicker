using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberAreaMono : MonoBehaviour
{
    public Transform container;
    public GameObject playerDataMonoPrefab;
    public Manager manager;
    public int count;
    private void Refresh()
    {
        for (int i = container.childCount - 1; i >= 0; i--)
        {
            Destroy(container.GetChild(i).gameObject);
        }

        List<PlayerData> list = manager.database.playerDataList;
        for (int i = 0; i < list.Count; i++)
        {
            PlayerData data = list[i];
            GameObject newGo = Instantiate(playerDataMonoPrefab, container, false);
            newGo.GetComponent<PlayerDataMono>().SetInfo(data.name, data.strength, data.timesThisWeek, data.contribution);
        }
        count = manager.database.playerDataList.Count;
    }

    private void Awake()
    {
        Refresh();
    }
    private void Update()
    {
        if (count != manager.database.playerDataList.Count)
        {
            Refresh();
        }
    }
}
