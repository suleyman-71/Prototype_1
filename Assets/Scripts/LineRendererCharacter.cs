using UnityEngine;
using System.Collections.Generic;

#region noktalar arasý hareket
public class LineRendererCharacter : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float moveSpeed;
    public float rotationSpeed;
    public Transform[] pathPoints;
    public List<AnimationClip> animationClips;
    public string startAnimationName;
    public List<string> touchAnimationNames;
    public List<string> defaultAnimationNames;

    private int currentPathIndex;
    private float currentDistance;
    private Vector3 direction;
    private Animator animator;
    private bool isMoving = false;
    private bool isEndPoint = false;

    private void Start()
    {
        // pathPoints dizisindeki pozisyonlar, empty nesnelerin konumlarýna atanýr.
        Vector3[] positions = new Vector3[pathPoints.Length];
        for (int i = 0; i < pathPoints.Length; i++)
        {
            positions[i] = pathPoints[i].position;
        }

        animator = GetComponent<Animator>();
        animator.Play(startAnimationName);

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);

        lineRenderer.enabled = false;

        currentPathIndex = 0;
        currentDistance = 0f;
        direction = (pathPoints[currentPathIndex + 1].position - pathPoints[currentPathIndex].position).normalized;
    }

    private void Update()
    {
        if (!isEndPoint) // isMoving true olduðu sürece karakter hareket eder
        {
            HandleTouchInput();
            CheckEndPoint();
        }
        Debug.Log("Current path index:" + currentPathIndex);
        Debug.Log("Current Animation:" + touchAnimationNames[currentPathIndex]);
    }

    private void CheckEndPoint()
    {
        if (currentPathIndex == pathPoints.Length - 1 && Vector3.Distance(transform.position, pathPoints[currentPathIndex].position) < 0.1f)
        {
            isEndPoint = true;
            animator.Play(touchAnimationNames[currentPathIndex]);
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isMoving = true;
                    break;

                case TouchPhase.Ended:
                    isMoving = false;
                    animator.Play(defaultAnimationNames[currentPathIndex]);
                    break;

                default:
                    break;
            }

            if (isMoving)
            {
                animator.Play(touchAnimationNames[currentPathIndex]);

                //Karakterin ilerleme ve dönmesini saðlamakta
                currentDistance += moveSpeed * Time.deltaTime;
                transform.position = pathPoints[currentPathIndex].position + direction * currentDistance;
                Quaternion targetRotation = Quaternion.LookRotation(pathPoints[currentPathIndex + 1].position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                if (currentPathIndex >= pathPoints.Length)
                {
                    return;
                }

                if (Vector3.Distance(transform.position, pathPoints[currentPathIndex].position) < 0.1f)
                {
                    animator.Play(touchAnimationNames[currentPathIndex]);
                }


                if (currentDistance >= Vector3.Distance(pathPoints[currentPathIndex].position, pathPoints[currentPathIndex + 1].position))
                {
                    currentDistance = 0f;
                    currentPathIndex++;

                    if (currentPathIndex == pathPoints.Length - 1)
                    {
                        //currentPathIndex = 0;
                        return;
                    }

                    direction = (pathPoints[currentPathIndex + 1].position - pathPoints[currentPathIndex].position).normalized;
                }
            }
        }
    }
}
#endregion


