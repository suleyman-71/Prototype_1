#region çalýþan versiyonu
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Playables;

//public class TrackManager : MonoBehaviour
//{
//    public List<GameObject> prefabs = new List<GameObject>();
//    public int[] indexList = new int[] { 0, 0, 1, 0, 0 };

//    void Start()
//    {

//        if (prefabs.Count > 0)
//        {
//            // ilk prefabý yerleþtir
//            GameObject currentPrefab = Instantiate(prefabs[indexList[0]], transform.position, Quaternion.identity);

//            // sonraki prefablarla baðlantýlarýný kur
//            for (int i = 1; i < indexList.Length; i++)
//            {
//                // currentPrefab'ýn "dot_1" noktasý ile bir sonraki prefabýn "dot_0" noktasýný eþleþtir
//                Transform currentDot1 = currentPrefab.transform.Find("dot_1");
//                Transform nextDot0 = prefabs[indexList[i]].transform.Find("dot_0");
//                Vector3 offset = nextDot0.localPosition - currentDot1.localPosition;

//                // yeni prefabý baðla
//                GameObject nextPrefab = Instantiate(prefabs[indexList[i]], currentPrefab.transform.position + offset, Quaternion.identity);

//                // baðlantýyý oluþtur
//                ConnectPrefabs(currentPrefab, nextPrefab);

//                // sonraki prefab için currentPrefabý güncelle
//                currentPrefab = nextPrefab;
//            }
//        }
//    }

//    void ConnectPrefabs(GameObject currentPrefab, GameObject nextPrefab)
//    {
//        // currentPrefab'ýn "dot_1" noktasý ile bir sonraki prefabýn "dot_0" noktasýný eþleþtir
//        Transform currentDot1 = currentPrefab.transform.Find("dot_1");
//        Transform nextDot0 = nextPrefab.transform.Find("dot_0");

//        // currentPrefab'ýn "dot_1" noktasýnda bir empty object oluþtur
//        GameObject connection = new GameObject();
//        connection.transform.parent = currentDot1;
//        connection.transform.localPosition = Vector3.zero;

//        // nextPrefab'ýn "dot_0" noktasýna empty objecti baðla
//        nextDot0.parent = connection.transform;
//        nextDot0.localPosition = Vector3.zero;
//    }
//}
#endregion


using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>();
    public UserProgress userProgress;

    void Start()
    {
        userProgress = UserProgress.LoadProgress();

        if (prefabs.Count > 0 && userProgress.levelScores.Length > 0)
        {
            // Ýlk prefabý yerleþtir
            GameObject currentPrefab = Instantiate(prefabs[userProgress.levelScores[0]], transform.position, Quaternion.identity);

            // Sonraki prefablarla baðlantýlarýný kur
            for (int i = 1; i < userProgress.levelScores.Length; i++)
            {
                // currentPrefab'ýn "dot_1" noktasý ile bir sonraki prefabýn "dot_0" noktasýný eþleþtir
                Transform currentDot1 = currentPrefab.transform.Find("dot_1");
                Transform nextDot0 = prefabs[userProgress.levelScores[i]].transform.Find("dot_0");
                Vector3 offset = nextDot0.localPosition - currentDot1.localPosition;

                // Yeni prefabý baðla
                GameObject nextPrefab = Instantiate(prefabs[userProgress.levelScores[i]], currentPrefab.transform.position + offset, Quaternion.identity);

                // Baðlantýyý oluþtur
                ConnectPrefabs(currentPrefab, nextPrefab);

                // Sonraki prefab için currentPrefabý güncelle
                currentPrefab = nextPrefab;
            }
        }
    }

    void ConnectPrefabs(GameObject currentPrefab, GameObject nextPrefab)
    {
        // currentPrefab'ýn "dot_1" noktasý ile bir sonraki prefabýn "dot_0" noktasýný eþleþtir
        Transform currentDot1 = currentPrefab.transform.Find("dot_1");
        Transform nextDot0 = nextPrefab.transform.Find("dot_0");

        // currentPrefab'ýn "dot_1" noktasýnda bir empty object oluþtur
        GameObject connection = new GameObject();
        connection.transform.parent = currentDot1;
        connection.transform.localPosition = Vector3.zero;

        // nextPrefab'ýn "dot_0" noktasýna empty objecti baðla
        nextDot0.parent = connection.transform;
        nextDot0.localPosition = Vector3.zero;
    }
}
