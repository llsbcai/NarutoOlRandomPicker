using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Database database = new();
    public Button btnClearAllData;
    public Button btnParse;
    public InputField inputFieldContent;
    public Button btnReadFileAndParse;
    public InputField inputFieldDirPath;

    public void Awake()
    {

    }

    public void OnEnable()
    {
        btnParse.onClick.AddListener(ParseCallback);
        btnClearAllData.onClick.AddListener(() => { database.ClearAllItem(); });
        btnReadFileAndParse.onClick.AddListener(ReadFileAndParse);
    }

    public void ParseCallback()
    {
        string str = inputFieldContent.text;
        ParseQqOcr(str);
    }

    public void ParseWetChatOcr(string input)
    {
        string[] strArray = input.Split("本周贡献");
        List<PlayerData> result = new(9);
        foreach (string s in strArray)
        {
            if(!s.Contains("参战次数"))continue;
            string[] detailArray = s.Split("参战次数");
            //获取名字
            string playerName = detailArray[0].Trim();
            int lastIndexForZhan = playerName.LastIndexOf("战");
            playerName = playerName.Substring(0, lastIndexForZhan).Trim();
            if (playerName.Contains("\n"))
            {
                playerName = playerName.Split("\n")[1];
            }
            else
            {
                playerName = Regex.Replace(playerName, @"\d+", "");
            }
            //获取参战次数
            int timesThisWeek = int.Parse(detailArray[1]);
            PlayerData playerData = new PlayerData(playerName, timesThisWeek);
            result.Add(playerData);
        }
        string str = "";
        foreach (PlayerData playerData in result)
        {
            database.AddNewItem(playerData);
            str += JsonConvert.SerializeObject(playerData);
            str += "\r\n";
        }
        Debug.Log($"解析成功:\r\n{str}");
    }

    public void ParseQqOcr(string input)
    {
        string[] strings = input.Trim().Split("\n");
        List<PlayerData> result = new(9);
        for (int i = 0; i < strings.Length; i++)
        {
            int tag = i % 12;
            if (tag == 0 || tag == 1 || tag == 2)
            {
                string fightingCapacityStr = strings[i + 3];
                string timesThisWeekStr = strings[i + 6];
                string contributionStr = strings[i + 9];
                int strength = int.Parse(fightingCapacityStr.Replace("战", ""));
                int timesThisWeek = int.Parse(timesThisWeekStr.Replace("参战次数", ""));
                int contribution = int.Parse(contributionStr.Replace("本周贡献", ""));
                PlayerData data = new PlayerData(strings[i], strength, contribution, timesThisWeek);
                result.Add(data);
            }
        }
        string str = "";
        foreach (PlayerData playerData in result)
        {
            database.AddNewItem(playerData);
            str += JsonConvert.SerializeObject(playerData);
            str += "\r\n";
        }
        Debug.Log($"解析成功:\r\n{str}");
    }

    public void ReadFileAndParse()
    {
        database.ClearAllItem();
        string dirPath = inputFieldDirPath.text;
        if (!Directory.Exists(dirPath)) return;
        DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
        FileInfo[] fileInfos = dirInfo.GetFiles("*.txt", SearchOption.AllDirectories);
        foreach (FileInfo fileInfo in fileInfos)
        {
            string text = File.ReadAllText(fileInfo.FullName, Encoding.UTF8);
            ParseQqOcr(text);
        }
    }
}
