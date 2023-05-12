using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] uiElementsToHide; // Kaybolacak UI elementlerini tutan dizi

    private bool isUIVisible = true; // UI elementlerinin görünürlüðünü takip eden bir flag

    private void Update()
    {
        // Ekrana dokunulduðunda veya fare týklandýðýnda UI'nýn görünürlüðünü deðiþtir
        if (Input.GetMouseButtonDown(0))
        {
            ToggleUIVisibility();
        }
    }

    private void ToggleUIVisibility()
    {
        isUIVisible = !isUIVisible; // Flag'i tersine çevir

        // UI elementlerini gizle veya göster
        foreach (GameObject element in uiElementsToHide)
        {
            element.SetActive(isUIVisible);
        }
    }
}

