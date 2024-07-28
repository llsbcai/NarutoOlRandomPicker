using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawMono : MonoBehaviour
{
    public InputField inputFieldOrangeCount;
    public InputField inputFieldPurplesCount;
    public Button btnDraw;
    public Text txtResult;
    public Manager manager;
    private void OnEnable()
    {
        btnDraw.onClick.AddListener(Draw);
    }

    private void Draw()
    {
        string result = "";
        if (int.TryParse(inputFieldOrangeCount.text, out int orangeCount) &&
            int.TryParse(inputFieldPurplesCount.text, out int purpleCount))
        {
            List<List<PlayerData>> list2D = manager.database.DrawPrize(orangeCount, purpleCount);
            if (list2D == null)
            {
                result += "要分配的包数量大于总人数，are you fucking kidding me??";
            }
            else
            {
                result += "获得橙包：";
                foreach (PlayerData playerData in list2D[0])
                {
                    result += playerData.name;
                    result += "  ";
                }
                result += "\r\n获得紫包：";
                foreach (PlayerData playerData in list2D[1])
                {
                    result += playerData.name;
                    result += "  ";
                }
            }
        }
        txtResult.text = result;
    }
}
