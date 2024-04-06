using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MySceneManager
{
    /// <summary>
    /// ��� ����� ��� �������� - ����� ������, ��� ��� ����� ���� ������ 
    /// ���� ���� ������� �������� �� ����� ������������ ������
    /// </summary>
    public static void LoadScene(int id)
    {
        LoadingScene.SceneId = id;
        SceneManager.LoadScene("LoadingScene");
    }

    /// <summary>
    /// ��� ����� ��� �������� - ����� ������, ��� ��� ����� ���� ������ 
    /// ���� ���� ������� �������� �� ����� ������������ ������
    /// </summary>
    public static void LoadScene(string name)
    {
        LoadingScene.SceneName = name;
        SceneManager.LoadScene("LoadingScene");
    }
}
