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
        SetLightRadius(playerData.lightEnergyLimation);
    }

    private void Update()
    {
        lightEnergyLimation = playerData.lightEnergyLimation;
        UpdateLightFalloff();
    }

    private void UpdateLightFalloff()
    {
        float ammos = playerData.ammo;
        float maxAmmo = playerData.lightEnergyLimation;
        Debug.Log(ammos / maxAmmo);
        playerLight.falloffIntensity =  0.5f+(1-ammos / maxAmmo)*0.5f;
    }
    //光能上限同步外部半径
    public void SetLightRadius(float radius)
    {
        playerLight.pointLightOuterRadius = radius;
    }
}       
