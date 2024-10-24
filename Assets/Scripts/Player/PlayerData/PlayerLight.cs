using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLight : MonoBehaviour
{
    public Light2D playerLight;
    public PlayerData_Temp playerData;
    public float lightEnergyLimation; // ����ӵ�����

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
    //��������ͬ���ⲿ�뾶
    public void SetLightRadius(float radius)
    {
        playerLight.pointLightOuterRadius = radius;
    }
}       
