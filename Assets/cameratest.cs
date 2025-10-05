using Mirror.Examples.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class cameratest : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private AudioListener audioListener;
/*
    public override void OnStartLocalPlayer()
    {
        // 🔥 Включаем компоненты только для локального игрока
        if (playerCamera != null)
        {
            playerCamera.enabled = true;
            playerCamera.tag = "MainCamera"; // Делаем основной камерой
        }

        if (audioListener != null)
        {
            audioListener.enabled = true;
        }

        // Отключаем у других игроков
        PlayerCamera[] allCameras = FindObjectsOfType<PlayerCamera>();
        foreach (PlayerCamera cam in allCameras)
        {
            if (cam != this && cam.isLocalPlayer)
            {
                if (cam.playerCamera != null)
                    cam.playerCamera.enabled = false;
                if (cam.playerCamera != null)
                    cam.audioListener.enabled = false;
            }
        }
    }*/
}
