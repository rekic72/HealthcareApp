using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UI;

public class HeartModelSwitch : MonoBehaviour
{
    public GameObject heartModel;
    public Button toggleButton;
    public Text buttonText;

    public void ToggleHeartVisibility()
    {
        Debug.Log("ToggleHeartVisibility aufgerufen");
        bool isActive = heartModel.activeSelf;
        heartModel.SetActive(!heartModel.activeSelf);

        if (buttonText != null)
        {
            buttonText.text = isActive ? "Einblenden" : "Ausblenden";
        }
    }
}
