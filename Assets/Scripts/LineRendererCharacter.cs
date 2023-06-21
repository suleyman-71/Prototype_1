using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

#region noktalar aras� hareket
public class LineRendererCharacter : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float moveSpeed;
    public float rotationSpeed;
    public Transform[] pathPoints;
    public Transform[] clipPoints;
    public List<AnimationClip> animationClips;
    public string startAnimationName;
    public List<string> touchAnimationNames;
    public List<string> defaultAnimationNames;

    private int currentPathIndex;
    private float currentDistance;
    private Vector3 direction;
    private Animator animator;
    private bool isMoving = false;
    public bool isEndPoint = false;
    private int currentLevel;
    private UserProgress userProgress = new UserProgress();

    public UnityEvent OnEndPointChanged; // UnityEvent tan�mlanmas�


    private void Start()
    {
        //OnEndPointChanged.AddListener(HandleEndPointChanged);


        currentLevel = userProgress.currentLevel;
        // pathPoints dizisindeki pozisyonlar, empty nesnelerin konumlar�na atan�r.
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
        if (!isEndPoint) // isMoving true oldu�u s�rece karakter hareket eder
        {
            HandleTouchInput();
            CheckEndPoint();
        }
        Debug.Log("Current path index:" + currentPathIndex);
        Debug.Log("Current Animation:" + touchAnimationNames[currentPathIndex]);
        Debug.Log("char endpoint" + isEndPoint);
    }

    //private void HandleEndPointChanged()
    //{
    //    // isEndPoint de�eri de�i�ti�inde yap�lacak i�lemler
    //}

    private void CheckEndPoint()
    {
        if (currentPathIndex == pathPoints.Length - 1 && Vector3.Distance(transform.position, pathPoints[currentPathIndex].position) < 0.1f)
        {
            isEndPoint = true;
            animator.Play(touchAnimationNames[currentPathIndex]);
            currentLevel++;
            userProgress.SavePlayerProgress(currentLevel);

            OnEndPointChanged.Invoke(); // OnEndPointChanged olay�n� tetikleme

        }
    }

    private void HandleTouchInput()
    {
        for(int i = 0; i < clipPoints.Length; i++){
            if(clipPoints[i] == pathPoints[currentPathIndex]){
                animator.Play(touchAnimationNames[currentPathIndex]);
            }
            else{
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

                //Karakterin ilerleme ve d�nmesini sa�lamakta
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
        // if (Input.touchCount == 1)
        // {
        //     Touch touch = Input.GetTouch(0);

        //     switch (touch.phase)
        //     {
        //         case TouchPhase.Began:
        //             isMoving = true;
        //             break;

        //         case TouchPhase.Ended:
        //             isMoving = false;
        //             animator.Play(defaultAnimationNames[currentPathIndex]);
        //             break;

        //         default:
        //             break;
        //     }

        //     if (isMoving)
        //     {
        //         animator.Play(touchAnimationNames[currentPathIndex]);

        //         //Karakterin ilerleme ve d�nmesini sa�lamakta
        //         currentDistance += moveSpeed * Time.deltaTime;
        //         transform.position = pathPoints[currentPathIndex].position + direction * currentDistance;
        //         Quaternion targetRotation = Quaternion.LookRotation(pathPoints[currentPathIndex + 1].position - transform.position);
        //         transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        //         if (currentPathIndex >= pathPoints.Length)
        //         {
        //             return;
        //         }

        //         if (Vector3.Distance(transform.position, pathPoints[currentPathIndex].position) < 0.1f)
        //         {
        //             animator.Play(touchAnimationNames[currentPathIndex]);
        //         }


        //         if (currentDistance >= Vector3.Distance(pathPoints[currentPathIndex].position, pathPoints[currentPathIndex + 1].position))
        //         {
        //             currentDistance = 0f;
        //             currentPathIndex++;

        //             if (currentPathIndex == pathPoints.Length - 1)
        //             {
        //                 //currentPathIndex = 0;
        //                 return;
        //             }

        //             direction = (pathPoints[currentPathIndex + 1].position - pathPoints[currentPathIndex].position).normalized;
        //         }
        //     }
        // }
    }
}
#endregion