using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// LoadingScene - ����� ������ ����� � ������ ��� ������
/// (��� ��� � ��� ������������ ������ � ��� �������� ����)
/// </summary>
public class LoadingScene : MonoBehaviour
{
    private static int SCENE_ID;
    private static string SCENE_NAME;

    [SerializeField] private int _defaultSceneId = 1; // main menu

    private AsyncOperation _asyncOperation;

    /// <summary>
    /// SCENE_NAME ����� ������, ��� ��� ����� ���� ������ 
    /// ���� ���� ������� �������� �� ����� ������������ ������
    /// </summary>
    public static int SceneId
    {
        get => SCENE_ID;
        set
        {
            SCENE_NAME = "";
            SCENE_ID = value;
        }
    }

    /// <summary>
    /// SCENE_ID ����� ������, ��� ��� ����� ���� ������ 
    /// ���� ���� ������� �������� �� ����� ������������ ������
    /// </summary>
    public static string SceneName
    {
        get => SCENE_NAME;
        set
        {
            SCENE_NAME = value;
            SCENE_ID = -1;
        }
    }

    private void Start()
    {
        if (SCENE_ID == 0 || SCENE_NAME == "LoadingScene")
            SCENE_ID = _defaultSceneId;

        YandexSDK.Instance.CreateInstance();
        GameDataManager.Instance.LoadLevelsData();

        StartCoroutine(LoadSceneRoutine());
    }

    IEnumerator LoadSceneRoutine() 
    {
        yield return new WaitForSeconds(1);

        if (SCENE_ID != -1)
        {
            Debug.Log($"LoadingScene.LoadSceneRoutine(): ������� �� ������� - {SCENE_ID}");
            _asyncOperation = SceneManager.LoadSceneAsync(SCENE_ID);
        }
        else if (SCENE_NAME != "")
        {
            Debug.Log($"LoadingScene.LoadSceneRoutine(): ������� �� ������� - {SCENE_NAME}");
            _asyncOperation = SceneManager.LoadSceneAsync(SCENE_NAME);
        }
        else
        {
            Debug.Log($"LoadingScene.LoadSceneRoutine(): ������� �� ������� - {_defaultSceneId}");
            _asyncOperation = SceneManager.LoadSceneAsync(_defaultSceneId);
        }

        while (!_asyncOperation.isDone &&
            !GameDataManager.Instance.IsDataIsReady) 
        {
            float progress = _asyncOperation.progress;
            yield return 0;
        }
    }
}
