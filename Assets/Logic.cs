using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logic : MonoBehaviour {

    public Text displayText;
    public Text reflectionDisplayText;

    public void InitializeAndSave()
    {
        PlayerData.Reset();
        PlayerData.Local.Initialize();
        PlayerData.Local.DisplayName = "5argon" + Random.Range(0, 100).ToString("000");

        Inner inner = new Inner();
        Innermost innermost = new Innermost();
        innermost.Integer = Random.Range(0, 100);
        innermost.String = Random.Range(0, 100).ToString();
        inner.MapInnermost.Add("key", innermost);

        PlayerData.Local.RepeatedInner.Clear();
        PlayerData.Local.RepeatedInner.Add(inner);

        PlayerData.Local.Save();
        DisplayPlayerInformation();
        Debug.Log("Saved");

    }

    public void ReflectionTest()
    {
        AbsolutePosition ap = new AbsolutePosition();
        ap.ValuePt = 500;
        reflectionDisplayText.text = ap.ToString();
    }

    public void DisplayPlayerInformation()
    {
        if (PlayerData.Local == null)
        {
            displayText.text = "No data...";
        }
        else
        {
            //displayText.text = $"{PlayerData.Local.PlayerId} {PlayerData.Local.FormattedShortPlayerId} {PlayerData.Local.DisplayName} > {PlayerData.Local.RepeatedInner[0].MapInnermost["key"].Integer} > {PlayerData.Local.RepeatedInner[0].MapInnermost["key"].String}";
            displayText.text = PlayerData.Local.PlayerId + " " + PlayerData.Local.FormattedShortPlayerId + " " + PlayerData.Local.DisplayName + " > " + PlayerData.Local.RepeatedInner[0].MapInnermost["key"].Integer + " > " + PlayerData.Local.RepeatedInner[0].MapInnermost["key"].String;
        }
	}

	public void LoadFromProto()
	{
		PlayerData.LocalReload();
		DisplayPlayerInformation();
		Debug.Log("Loaded");
	}

}
