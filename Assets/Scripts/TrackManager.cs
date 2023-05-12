#region �al��an versiyonu
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
//            // ilk prefab� yerle�tir
//            GameObject currentPrefab = Instantiate(prefabs[indexList[0]], transform.position, Quaternion.identity);

//            // sonraki prefablarla ba�lant�lar�n� kur
//            for (int i = 1; i < indexList.Length; i++)
//            {
//                // currentPrefab'�n "dot_1" noktas� ile bir sonraki prefab�n "dot_0" noktas�n� e�le�tir
//                Transform currentDot1 = currentPrefab.transform.Find("dot_1");
//                Transform nextDot0 = prefabs[indexList[i]].transform.Find("dot_0");
//                Vector3 offset = nextDot0.localPosition - currentDot1.localPosition;

//                // yeni prefab� ba�la
//                GameObject nextPrefab = Instantiate(prefabs[indexList[i]], currentPrefab.transform.position + offset, Quaternion.identity);

//                // ba�lant�y� olu�tur
//                ConnectPrefabs(currentPrefab, nextPrefab);

//                // sonraki prefab i�in currentPrefab� g�ncelle
//                currentPrefab = nextPrefab;
//            }
//        }
//    }

//    void ConnectPrefabs(GameObject currentPrefab, GameObject nextPrefab)
//    {
//        // currentPrefab'�n "dot_1" noktas� ile bir sonraki prefab�n "dot_0" noktas�n� e�le�tir
//        Transform currentDot1 = currentPrefab.transform.Find("dot_1");
//        Transform nextDot0 = nextPrefab.transform.Find("dot_0");

//        // currentPrefab'�n "dot_1" noktas�nda bir empty object olu�tur
//        GameObject connection = new GameObject();
//        connection.transform.parent = currentDot1;
//        connection.transform.localPosition = Vector3.zero;

//        // nextPrefab'�n "dot_0" noktas�na empty objecti ba�la
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
            // �lk prefab� yerle�tir
            GameObject currentPrefab = Instantiate(prefabs[userProgress.levelScores[0]], transform.position, Quaternion.identity);

            // Sonraki prefablarla ba�lant�lar�n� kur
            for (int i = 1; i < userProgress.levelScores.Length; i++)
            {
                // currentPrefab'�n "dot_1" noktas� ile bir sonraki prefab�n "dot_0" noktas�n� e�le�tir
                Transform currentDot1 = currentPrefab.transform.Find("dot_1");
                Transform nextDot0 = prefabs[userProgress.levelScores[i]].transform.Find("dot_0");
                Vector3 offset = nextDot0.localPosition - currentDot1.localPosition;

                // Yeni prefab� ba�la
                GameObject nextPrefab = Instantiate(prefabs[userProgress.levelScores[i]], currentPrefab.transform.position + offset, Quaternion.identity);

                // Ba�lant�y� olu�tur
                ConnectPrefabs(currentPrefab, nextPrefab);

                // Sonraki prefab i�in currentPrefab� g�ncelle
                currentPrefab = nextPrefab;
            }
        }
    }

    void ConnectPrefabs(GameObject currentPrefab, GameObject nextPrefab)
    {
        // currentPrefab'�n "dot_1" noktas� ile bir sonraki prefab�n "dot_0" noktas�n� e�le�tir
        Transform currentDot1 = currentPrefab.transform.Find("dot_1");
        Transform nextDot0 = nextPrefab.transform.Find("dot_0");

        // currentPrefab'�n "dot_1" noktas�nda bir empty object olu�tur
        GameObject connection = new GameObject();
        connection.transform.parent = currentDot1;
        connection.transform.localPosition = Vector3.zero;

        // nextPrefab'�n "dot_0" noktas�na empty objecti ba�la
        nextDot0.parent = connection.transform;
        nextDot0.localPosition = Vector3.zero;
    }
}
