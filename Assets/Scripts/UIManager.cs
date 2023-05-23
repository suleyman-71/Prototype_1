using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject uiElementForEnd;
    public GameObject uiElementForStart;
    public GameObject gamePlayController;

    private LineRendererCharacter lineRendererCharacter;


    private void Start()
    {
        lineRendererCharacter = gamePlayController.GetComponent<LineRendererCharacter>();
        lineRendererCharacter.OnEndPointChanged.AddListener(HandleEndPointChanged);

        lineRendererCharacter.enabled = false;
        uiElementForEnd.SetActive(false);
        uiElementForStart.SetActive(true);
    }

    private void Update()
    {

    }

    private void HandleEndPointChanged()
    {
        uiElementForEnd.SetActive(true);
    }
    public void StartGame()
    {
        gamePlayController.GetComponent<LineRendererCharacter>().enabled = true;
        uiElementForStart.SetActive(false);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("PlayingScene");
    }
}

