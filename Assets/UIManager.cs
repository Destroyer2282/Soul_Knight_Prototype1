using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            return _instance;

        }

    }
    #endregion
    private void Awake()
    {
        _instance = this;
    }
    [SerializeField] private GameObject spawnGroupContainer;
    public TMP_InputField nameInputField;

    public void SpawnGroupToogle()
    {
        spawnGroupContainer.SetActive(!spawnGroupContainer.activeSelf);
    }
    public bool CheckName()
    {
        string playerName = nameInputField.text.Trim(); //delete tabs and spaces in text

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.Log("Придумайте другое имя!");
            return false;
        }
        return true;
    }
}
