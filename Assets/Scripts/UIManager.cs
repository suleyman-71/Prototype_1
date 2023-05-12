using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] uiElementsToHide; // Kaybolacak UI elementlerini tutan dizi

    private bool isUIVisible = true; // UI elementlerinin g�r�n�rl���n� takip eden bir flag

    private void Update()
    {
        // Ekrana dokunuldu�unda veya fare t�kland���nda UI'n�n g�r�n�rl���n� de�i�tir
        if (Input.GetMouseButtonDown(0))
        {
            ToggleUIVisibility();
        }
    }

    private void ToggleUIVisibility()
    {
        isUIVisible = !isUIVisible; // Flag'i tersine �evir

        // UI elementlerini gizle veya g�ster
        foreach (GameObject element in uiElementsToHide)
        {
            element.SetActive(isUIVisible);
        }
    }
}

