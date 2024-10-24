using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLight : MonoBehaviour
{
    public Light2D playerLight;
    public PlayerData_Temp playerData;
    public float lightEnergyLimation; // 最大子弹数量

    private void Start()
    {
        lightEnergyLimation = playerData.lightEnergyLimation;
        UpdateLightFalloff();
    }

    private void Update()
    {
        lightEnergyLimation = playerData.lightEnergyLimation;
        UpdateLightFalloff();
    }

    private void UpdateLightFalloff()
    {
        int ammos = playerData.ammo;
        int maxAmmo = playerData.lightEnergyLimation;

        playerLight.falloffIntensity = 1- ammos / maxAmmo *0.5f;
    }
}       
