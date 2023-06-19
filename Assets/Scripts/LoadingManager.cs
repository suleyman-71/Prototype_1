using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public Image backgroundBar; // Beyaz arka plan Image referans�
    public Image fillBar; // Ye�il dolgu Image referans�
    public string nextSceneName; // Ge�ilecek di�er sahnenin ad�

    private bool isLoadingComplete = false;

    private void Start()
    {
        // �� par�ac��� ba�latma
        Thread loadingThread = new Thread(LoadingThread);
        loadingThread.Start();
    }

    private void Update()
    {
        // �� par�ac���ndan gelen y�klenme ilerlemesini UI'� g�ncelleme
        if (!isLoadingComplete)
        {
            float progress = Mathf.Clamp01(LoadingProgress.GetProgress()); // �� par�ac���ndan ilerlemeyi al
            fillBar.fillAmount = progress; // Ye�il dolgu Image'�n�n dolgu miktar�n� g�ncelle
        }
        else
        {
            // Y�kleme tamamland�, di�er sahneye ge�i�
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void LoadingThread()
    {
        // Y�kleme i�lemini sim�le et
        for (int i = 0; i <= 100; i++)
        {
            LoadingProgress.SetProgress(i / 100f); // �� par�ac���ndan ilerlemeyi g�ncelle
            Thread.Sleep(50); // ��lemi bekletme (sim�lasyon ama�l�)
        }

        // Y�kleme tamamland�
        isLoadingComplete = true;
    }

    public static class LoadingProgress
    {
        private static float progress = 0f;

        public static float GetProgress()
        {
            return progress;
        }

        public static void SetProgress(float value)
        {
            progress = value;
        }
    }

}
