using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logic : MonoBehaviour {

	public Text displayText;

    public void InitializeAndSave()
    {
		PlayerData.Reset();
		PlayerData.Local.Initialize();
        PlayerData.Local.DisplayName = "5argon" + Random.Range(0, 100).ToString("000");
        PlayerData.Local.Save();
        DisplayPlayerInformation();
		Debug.Log("Saved");
    }

    public void DisplayPlayerInformation()
    {
        if (PlayerData.Local == null)
        {
            displayText.text = "No data...";
        }
        else
        {
            displayText.text = $"{PlayerData.Local.PlayerId} {PlayerData.Local.FormattedShortPlayerId} {PlayerData.Local.DisplayName}";
        }
	}

	public void LoadFromProto()
	{
		PlayerData.LocalReload();
		DisplayPlayerInformation();
		Debug.Log("Loaded");
	}

}
