using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform[] targets;
    public Transform[] cameraPositions;
    public Transform cameraStartPosition;
    public Transform characterToFollow;
    public float transitionDuration = 1.0f;
    public Camera mainCamera;

    private int currentTargetIndex = 0;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (mainCamera == null)
        {
            Debug.LogError("CameraController requires a main camera!");
            return;
        }

        mainCamera.transform.position = cameraStartPosition.position;
        mainCamera.transform.rotation = cameraStartPosition.rotation;
        //if (targets.Length > 0)
        //{
        //    mainCamera.transform.position = targets[0].position;
        //    mainCamera.transform.rotation = targets[0].rotation;
        //}
    }

    private void Update()
    {
        if (characterToFollow == null)
            return;
        if (currentTargetIndex < targets.Length)
        {
            Vector3 targetPosition = targets[currentTargetIndex].position;
            float distanceToTarget = Vector3.Distance(characterToFollow.position, targetPosition);

            if (distanceToTarget < 0.1f)
            {
                ChangeCameraTarget();
            }

        }
    }

    public void ChangeCameraTarget()
    {

        StartCoroutine(SmoothCameraTransition());
        currentTargetIndex++;

        //if (currentTargetIndex >= targets.Length)
        //{
        //    currentTargetIndex = 0;
        //}

        
    }

    private IEnumerator SmoothCameraTransition()
    {
        Transform currentTarget = targets[currentTargetIndex];
        Transform currentCameraPosition = cameraPositions[currentTargetIndex];
        Vector3 startPosition = mainCamera.transform.position;
        Quaternion startRotation = mainCamera.transform.rotation;
        Vector3 targetPosition = currentCameraPosition.position;
        Quaternion targetRotation = currentCameraPosition.rotation;
        float startTime = Time.time;

        while (Time.time - startTime <= transitionDuration)
        {
            float t = (Time.time - startTime) / transitionDuration;
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            mainCamera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
        mainCamera.transform.rotation = targetRotation;
    }
}
