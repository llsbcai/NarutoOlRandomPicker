using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataMono : MonoBehaviour
{
    public Text txtPlayerName;
    public Text txtPlayerStrength;
    public Text txtTimesThisWeek;
    public Text txtContributionThisWeek;

    public void SetInfo(string playerName, int strength, int timesThisWek, int contributionThisWeek)
    {
        txtPlayerName.text = playerName;
        txtPlayerStrength.text = "战" + strength;
        txtTimesThisWeek.text = timesThisWek.ToString();
        txtContributionThisWeek.text = contributionThisWeek.ToString();
    }
}
