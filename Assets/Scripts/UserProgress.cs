using System;
using System.Collections.Generic;
using UnityEngine;

public class UserProgress : MonoBehaviour
{
    public GameObject startPrefab;
    public GameObject endPrefab;
    public int sizeOfIndexList;
    public List<GameObject> prefabs = new List<GameObject>();
    public int currentLevel;

    void Start()
    {
        currentLevel = GetCurrentLevel();
        GenerateRandomIndexList();
        //LoadPlayerProgress();
        LoadLevelProgress(currentLevel);
    }

    public int GetCurrentLevel()
    {
        if (PlayerPrefs.HasKey("currentLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("currentLevel");
        }
        else
        {
            currentLevel = 1;
            SavePlayerProgress(currentLevel); // Ba�lang�� seviyesini kaydet
        }
        return currentLevel;
    }

    //void LoadPlayerProgress()
    //{
    //    if (PlayerPrefs.HasKey("currentLevel"))
    //    {
    //        currentLevel = PlayerPrefs.GetInt("currentLevel");
    //    }
    //    else
    //    {
    //        currentLevel = 1;
    //        SavePlayerProgress(); // Ba�lang�� seviyesini kaydet
    //    }

    //    LoadLevelProgress(currentLevel);
    //}

    public void  SavePlayerProgress(int currentLevel)
    {
        PlayerPrefs.SetInt("currentLevel", currentLevel);
        PlayerPrefs.Save();
    }

    void GenerateRandomIndexList()
    {
        string levelKey = "level_" + currentLevel.ToString();

        if (!PlayerPrefs.HasKey(levelKey))
        {
            List<int[]> allCombinations = GetAllCombinations();

            // Kar���k bir s�ralama yap
            ShuffleList(allCombinations);

            int[] selectedCombination = GetUnusedCombination(allCombinations);

            if (selectedCombination != null)
            {
                SaveIndexList(selectedCombination);
            }
        }
        //else
        //{
        //    LoadLevelProgress(currentLevel);
        //}
    }

    int[] GetUnusedCombination(List<int[]> combinations)
    {
        foreach (int[] combination in combinations)
        {
            string combinationKey = SerializeIndexList(combination);

            if (!PlayerPrefs.HasKey(combinationKey))
            {
                PlayerPrefs.SetInt(combinationKey, 1);
                PlayerPrefs.Save();
                return combination;
            }
        }

        return null; // Kullan�lmam�� kombinasyon bulunamad�
    }


    List<int[]> GetAllCombinations()
    {
        List<int[]> allCombinations = new List<int[]>();

        int[] indexList = new int[sizeOfIndexList]; // indexList boyutunu belirleyin (�rne�in 5)

        GenerateCombination(allCombinations, indexList, 0);

        return allCombinations;
    }

    void GenerateCombination(List<int[]> allCombinations, int[] indexList, int currentIndex)
    {
        if (currentIndex == indexList.Length)
        {
            int[] combination = new int[indexList.Length];
            Array.Copy(indexList, combination, indexList.Length);
            allCombinations.Add(combination);
        }
        else
        {
            for (int i = 0; i < prefabs.Count; i++)
            {
                indexList[currentIndex] = i;
                GenerateCombination(allCombinations, indexList, currentIndex + 1);
            }
        }
    }

    void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    void SaveIndexList(int[] indexList)
    {
        string serializedList = SerializeIndexList(indexList);
        string levelKey = "level_" + currentLevel.ToString();

        PlayerPrefs.SetString(levelKey, serializedList);
        PlayerPrefs.Save();
    }

    int[] LoadIndexList(int level)
    {
        string levelKey = "level_" + level.ToString();

        if (PlayerPrefs.HasKey(levelKey))
        {
            string serializedList = PlayerPrefs.GetString(levelKey);
            return DeserializeIndexList(serializedList);
        }

        return null;
    }

    string SerializeIndexList(int[] indexList)
    {
        return JsonUtility.ToJson(indexList);
    }

    int[] DeserializeIndexList(string serializedList)
    {
        return JsonUtility.FromJson<int[]>(serializedList);
    }

    void LoadLevelProgress(int level)
    {
        int[] indexList = LoadIndexList(level);

        if (indexList != null)
        {
            if (prefabs.Count > 0)
            {
                // Ba�lang�� prefab�n� yerle�tir
                GameObject currentPrefab = Instantiate(startPrefab, transform.position, Quaternion.identity);

                // sonraki prefablarla ba�lant�lar�n� kur
                for (int i = 0; i < indexList.Length; i++)
                {
                    // currentPrefab'�n "dot_1" noktas� ile bir sonraki prefab�n "dot_0" noktas�n� e�le�tir
                    Transform currentDot1 = currentPrefab.transform.Find("dot_1");
                    Transform nextDot0 = prefabs[indexList[i]].transform.Find("dot_0");
                    Vector3 offset = nextDot0.localPosition - currentDot1.localPosition;

                    // yeni prefab� ba�la
                    GameObject nextPrefab = Instantiate(prefabs[indexList[i]], currentPrefab.transform.position + offset, Quaternion.identity);

                    // ba�lant�y� olu�tur
                    ConnectPrefabs(currentPrefab, nextPrefab);

                    // sonraki prefab i�in currentPrefab� g�ncelle
                    currentPrefab = nextPrefab;
                }

                // Son prefab�n sonuna biti� prefab�n� ekle
                if (endPrefab != null)
                {
                    Transform currentDot1 = currentPrefab.transform.Find("dot_1");
                    Transform endDot0 = endPrefab.transform.Find("dot_0");
                    Vector3 offset = endDot0.localPosition - currentDot1.localPosition;

                    GameObject endPrefabInstance = Instantiate(endPrefab, currentPrefab.transform.position + offset, Quaternion.identity);
                    ConnectPrefabs(currentPrefab, endPrefabInstance);
                }
            }
        }
        else
        {
            // Dizi y�klenemedi, hata i�leme kodu buraya gelebilir
        }
    }


    void ConnectPrefabs(GameObject currentPrefab, GameObject nextPrefab)
    {
        // currentPrefab'�n "dot_1" noktas� ile bir sonraki prefab�n "dot_0" noktas�n� e�le�tir
        Transform currentDot1 = currentPrefab.transform.Find("dot_1");
        Transform nextDot0 = nextPrefab.transform.Find("dot_0");

        // �ki prefab�n aras�ndaki fark� hesapla
        Vector3 offset = nextDot0.position - currentDot1.position;

        // nextPrefab'� offset ile ta��
        nextPrefab.transform.position = currentPrefab.transform.position + offset;

        // currentPrefab'�n "dot_1" noktas�na empty obje yerle�tir
        GameObject connection = new GameObject();
        connection.transform.position = currentDot1.position;
        connection.transform.rotation = currentDot1.rotation;
        connection.transform.parent = currentDot1;

        // nextPrefab'�n "dot_0" noktas�n� empty objeye ba�la
        nextDot0.parent = connection.transform;
        nextDot0.localPosition = Vector3.zero;
        nextDot0.localRotation = Quaternion.identity;
    }



}