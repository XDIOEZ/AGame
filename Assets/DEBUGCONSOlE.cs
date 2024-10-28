using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ��Ҫ������������ռ���ʹ�ó���������

public class DEBUGCONSOlE : MonoBehaviour
{
    // �����Ҳ���Unity����
    private static DEBUGCONSOlE instance;
    private string debugInfo = ""; // ������Ϣ

    bool isDebug = false; // �Ƿ��ڵ���ģʽ
    bool setJumpCount = false; // ���ڿ����Ƿ�ÿ֡������Ծ����

    void Start()
    {
        // ȷ��ֻ��һ��DEBUGCONSOlEʵ������
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �л�����ʱ������
        }
        else
        {
            Destroy(gameObject); // ����Ѿ���ʵ�����ڣ������µ�ʵ��
        }
    }

    void Update()
    {
        // ����F12��������̨
        if (Input.GetKeyDown(KeyCode.F12))
        {
            isDebug = !isDebug; // �л�����ģʽ
        }

        if (!isDebug)
        {
            return; // ������ڵ���ģʽ��ֱ�ӷ���
        }

        // �л������߼�
        for (int i = 0; i <= 5; i++)
        {
            if (Input.GetKeyDown((KeyCode)(KeyCode.Alpha0 + i)))
            {
                SceneManager.LoadScene(i); // ���ض�Ӧ����
            }
        }

        // ����F11���debugInfo
        if (Input.GetKeyDown(KeyCode.F11))
        {
            debugInfo = ""; // ��յ�����Ϣ
        }

        // ��ȡ������ݣ�����G����
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); // ����������Ҷ���
            if (players.Length > 0)
            {
                foreach (GameObject player in players)
                {
                    PlayerData_Temp playerData = player.GetComponent<PlayerData_Temp>();
                    PlayerMove playerMove = player.GetComponent<PlayerMove>();

                    // ����������¼��Ϣ
                    if (playerData != null && playerMove != null)
                    {
                        debugInfo += $"����ҵ� - ����ֵ: {playerData.health}, ��ҩ: {playerData.ammo}, ����: {playerData.lightEnergyLimation}, ��Ծ����: {playerMove.jumpCount}\n";
                    }
                    else
                    {
                        if (playerData == null)
                        {
                            debugInfo += $"��Ҷ��� {player.name} ȱ�� PlayerData_Temp �����\n";
                        }
                        if (playerMove == null)
                        {
                            debugInfo += $"��Ҷ��� {player.name} ȱ�� PlayerMove �����\n";
                        }
                    }
                }
            }
            else
            {
                debugInfo += "������δ�ҵ���Ҷ���\n";
            }
        }

        // ����������ݣ�����F8����
        if (Input.GetKeyDown(KeyCode.F8))
        {
            setJumpCount = true; // ������Ծ�����ı�־Ϊtrue
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); // ����������Ҷ���
            if (players.Length > 0)
            {
                foreach (GameObject player in players)
                {
                    PlayerData_Temp playerData = player.GetComponent<PlayerData_Temp>();
                    PlayerMove playerMove = player.GetComponent<PlayerMove>();

                    // ���� PlayerData_Temp ����
                    if (playerData != null)
                    {
                        playerData.health = 99; // ��������ֵΪ99
                        playerData.ammo = 99;   // ���õ�ҩΪ99
                        playerData.lightEnergyLimation = 99; // ���ù���Ϊ99
                        debugInfo += $"��� {player.name} ��������Ϊ99������ֵ����ҩ�����ܣ���\n";
                    }
                    else
                    {
                        debugInfo += $"��Ҷ��� {player.name} ȱ�� PlayerData_Temp �����\n";
                    }

                    // ���� PlayerMove ����
                    if (playerMove != null)
                    {
                        playerMove.jumpCount = 99; // ������Ծ����Ϊ99
                        debugInfo += $"��� {player.name} ����Ծ��������Ϊ99��\n"; // ���µ�����Ϣ
                    }
                    else
                    {
                        debugInfo += $"��Ҷ��� {player.name} ȱ�� PlayerMove �����\n";
                    }
                }
            }
            else
            {
                debugInfo += "������δ�ҵ���Ҷ���\n";
            }
        }

        // ÿ֡������Ծ����Ϊ99
        if (setJumpCount)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); // ����������Ҷ���
            foreach (GameObject player in players)
            {
                PlayerMove playerMove = player.GetComponent<PlayerMove>();
                if (playerMove != null)
                {
                    playerMove.jumpCount = 99; // ÿ֡������Ծ����Ϊ99
                }
            }
        }
    }

    void OnGUI()
    {
        if (isDebug)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 24;
            style.normal.textColor = Color.white;

            // ��ʾ����ģʽ������Ϣ
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 50), "����ģʽ�ѿ���", style);

            // ��ʾ��������
            GUI.Label(new Rect(10, 10, 500, 50), "��F12�л�����ģʽ��", style);
            GUI.Label(new Rect(10, 40, 500, 50), "��0-5�л�������", style);
            GUI.Label(new Rect(10, 70, 500, 50), "��G��ȡ������ݡ�", style);
            GUI.Label(new Rect(10, 100, 500, 50), "��F8������ֵ����ҩ�͹�������Ϊ99����ÿ֡������Ծ����Ϊ99��", style);
            GUI.Label(new Rect(10, 130, 500, 50), "��F11��յ�����Ϣ��", style);
            GUI.Label(new Rect(10, 170, 500, 50), debugInfo, style); // ��ʾ������Ϣ
        }
    }
}
