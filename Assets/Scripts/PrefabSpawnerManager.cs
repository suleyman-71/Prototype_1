using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PrefabData
{
    public GameObject prefab;
    public Animator prefabAnimator;
    public AnimationClip prefabAnimationClip;
    public string longClipPointName;
    public AnimationClip longAnimationClip;
    public string cameraName;
}

public class PrefabSpawnerManager : MonoBehaviour
{
    public Animator characterAnimator;
    public AnimationClip playAnimationClip;
    public AnimationClip idleAnimationClip;
    public Transform spawnPoint;
    public float spacing = 0.1f;
    public PrefabData[] prefabData;
    public int[] prefabIndexes;

    private Vector3 currentSpawnPosition;
    void Start()
    {
        currentSpawnPosition = spawnPoint.position;
        SpawnPrefabs();
    }


    void Update()
    {

    }

    private void SpawnPrefabs()
    {
        int prefabNo = 0; //for debug.log
        for (int i = 0; i < prefabIndexes.Length; i++)
        {
            int prefabIndex = prefabIndexes[i];
            GameObject prefabInstance = null;
            if (prefabIndex >= 0 && prefabIndex < prefabData.Length)
            {
                PrefabData currentPrefabData = prefabData[prefabIndex];
                prefabInstance = Instantiate(currentPrefabData.prefab, currentSpawnPosition, Quaternion.identity);

                // Ýlk prefab ise doðrudan spawn pozisyonunu kullan
                if (i == 0)
                {
                    prefabInstance.transform.position = currentSpawnPosition;

                    Debug.Log("Current prefab:" + prefabNo + "position:" + prefabInstance.transform.position);
                    prefabNo++;
                }
                else
                {
                    // Önceki prefabýn sað noktasýný al
                    GameObject previousPrefab = prefabData[prefabIndexes[i - 1]].prefab;
                    Transform previousRightPoint = GetConnectionPointTransform(previousPrefab, "right_point");

                    // Mevcut prefabýn sol noktasýný al
                    Transform currentLeftPoint = GetConnectionPointTransform(currentPrefabData.prefab, "left_point");

                    // Noktalarýn dünya konumlarýný al
                    Vector3 previousRightPointWorldPosition = prefabInstance.transform.TransformPoint(previousRightPoint.localPosition);
                    Vector3 currentLeftPointWorldPosition = prefabInstance.transform.TransformPoint(currentLeftPoint.localPosition);

                    // Önceki prefabýn sað noktasý ile mevcut prefabýn sol noktasý arasýndaki mesafeyi hesapla
                    Vector3 spawnOffset = previousRightPointWorldPosition - currentLeftPointWorldPosition;
                    //prefabInstance.transform.position += spawnOffset;
                    prefabInstance.transform.position = new Vector3(
                        prefabInstance.transform.position.x,
                        prefabInstance.transform.position.y + spawnOffset.y,
                        prefabInstance.transform.position.z + spawnOffset.z);


                    Debug.Log("Current prefab:"+ prefabNo +"position:" + prefabInstance.transform.position);
                    Debug.Log("Previous prefab:" + (prefabNo - 1) + "right point:" + previousRightPointWorldPosition);
                    Debug.Log("Current prefab:" + prefabNo + "left point:" + currentLeftPointWorldPosition);
                    prefabNo++;
                }
                currentSpawnPosition = GetNextSpawnPosition(prefabInstance.transform, currentPrefabData);
            }
            else
            {
                Debug.LogError("Invalid prefab index!");
            }
        }
    }
    private Vector3 GetNextSpawnPosition(Transform prefabInstance, PrefabData prefabData)
    {
        // Sonraki spawn pozisyonunu, prefabýn sað noktasýnýn dünya konumu olarak belirle
        Transform rightPoint = GetConnectionPointTransform(prefabData.prefab, "right_point");
        //Vector3 rightPointWorldPosition = prefabInstance.transform.TransformPoint(rightPoint.localPosition);
        Vector3 rightPointWorldPosition = new Vector3(0.0f, 0.0f, prefabInstance.transform.TransformPoint(rightPoint.localPosition).z);
        return rightPointWorldPosition;
    }
    private Transform GetConnectionPointTransform(GameObject prefab, string pointName)
    {
        Transform prefabTransform = prefab.transform;
        Transform connectionPointTransform = null;

        foreach (Transform childTransform in prefabTransform)
        {
            if (childTransform.name == pointName)
            {
                connectionPointTransform = childTransform;
                break;
            }
        }

        return connectionPointTransform;
    }
    private List<Transform> GetRoadPointTransforms(GameObject prefab)
    {
        string childNamePrefix = "road_point_";
        List<Transform> roadPointTransforms = new List<Transform>();

        Transform prefabTransform = prefab.transform;

        foreach (Transform childTransform in prefabTransform)
        {
            if (childTransform.name.StartsWith(childNamePrefix))
            {
                roadPointTransforms.Add(childTransform);
            }
        }

        roadPointTransforms.Sort((a, b) =>
        {
            int aNumber = ExtractNumberFromName(a.name);
            int bNumber = ExtractNumberFromName(b.name);
            return aNumber.CompareTo(bNumber);
        });

        int ExtractNumberFromName(string name)
        {
            string numberString = name.Substring(childNamePrefix.Length);
            int number;
            if (int.TryParse(numberString, out number))
            {
                return number;
            }
            return 0; // Varsayýlan deðer olarak 0 döndürülebilir veya baþka bir deðer atanabilir
        }

        return roadPointTransforms;
    }

}
