//using UnityEngine;

//[System.Serializable]
//public class PrefabData
//{
//    public GameObject prefab;
//    public ConnectionPoints connectionPoints;
//    public PointData[] pointData;
//}

//[System.Serializable]
//public class ConnectionPoints
//{
//    public Transform rightPoint;
//    public Transform leftPoint;
//}

//[System.Serializable]
//public class PointData
//{
//    public Transform pointPosition;
//    public bool isNewCamera;
//    public Transform cameraPosition;
//    public AnimationClip playAnimationClip;
//    public AnimationClip idleAnimationClip;
//}

//public class PrefabSpawner : MonoBehaviour
//{
//    public PrefabData[] prefabs;
//    public int[] prefabIndexes;
//    public Transform spawnPoint;
//    public float spacing = 0.1f;

//    private Vector3 currentSpawnPosition;

//    private void Start()
//    {
//        //currentSpawnPosition = spawnPoint.position;
//        //SpawnPrefabs();
//    }

//    private void SpawnPrefabs()
//    {
//        for (int i = 0; i < prefabIndexes.Length; i++)
//        {
//            int prefabIndex = prefabIndexes[i];

//            if (prefabIndex >= 0 && prefabIndex < prefabs.Length)
//            {
//                PrefabData prefabData = prefabs[prefabIndex];

//                GameObject prefabInstance = Instantiate(prefabData.prefab, currentSpawnPosition, Quaternion.identity);

//                // �lk prefab ise do�rudan spawn pozisyonunu kullan
//                if (i == 0)
//                {
//                    prefabInstance.transform.position = currentSpawnPosition;
//                }
//                else
//                {
//                    // �nceki prefab�n sa� noktas�n� al
//                    Transform previousRightPoint = prefabs[prefabIndexes[i - 1]].connectionPoints.rightPoint;

//                    // Mevcut prefab�n sol noktas�n� al
//                    Transform currentLeftPoint = prefabData.connectionPoints.leftPoint;

//                    // Noktalar�n d�nya konumlar�n� al
//                    Vector3 previousRightPointWorldPosition = prefabInstance.transform.TransformPoint(previousRightPoint.localPosition);
//                    Vector3 currentLeftPointWorldPosition = prefabInstance.transform.TransformPoint(currentLeftPoint.localPosition);

//                    // �nceki prefab�n sa� noktas� ile mevcut prefab�n sol noktas� aras�ndaki mesafeyi hesapla
//                    Vector3 spawnOffset = previousRightPointWorldPosition - currentLeftPointWorldPosition;
//                    prefabInstance.transform.position += spawnOffset - new Vector3(spacing, 0f, 0f); // Spacing de�erini uygula
//                }

//                currentSpawnPosition = GetNextSpawnPosition(prefabInstance.transform, prefabData);
//            }
//            else
//            {
//                Debug.LogError("Invalid prefab index!");
//            }
//        }
//    }

//    private Vector3 GetNextSpawnPosition(Transform prefabInstance, PrefabData prefabData)
//    {
//        // Sonraki spawn pozisyonunu, prefab�n sa� noktas�n�n d�nya konumu olarak belirle
//        Transform rightPoint = prefabData.connectionPoints.rightPoint;
//        //Vector3 rightPointWorldPosition = prefabInstance.transform.TransformPoint(rightPoint.localPosition);
//        Vector3 rightPointWorldPosition = new Vector3(0.0f, 0.0f, prefabInstance.transform.TransformPoint(rightPoint.localPosition).z);
//        return rightPointWorldPosition;
//    }
//}


