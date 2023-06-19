using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public Image backgroundBar; // Beyaz arka plan Image referansý
    public Image fillBar; // Yeþil dolgu Image referansý
    public string nextSceneName; // Geçilecek diðer sahnenin adý

    private bool isLoadingComplete = false;

    private void Start()
    {
        // Ýþ parçacýðý baþlatma
        Thread loadingThread = new Thread(LoadingThread);
        loadingThread.Start();
    }

    private void Update()
    {
        // Ýþ parçacýðýndan gelen yüklenme ilerlemesini UI'ý güncelleme
        if (!isLoadingComplete)
        {
            float progress = Mathf.Clamp01(LoadingProgress.GetProgress()); // Ýþ parçacýðýndan ilerlemeyi al
            fillBar.fillAmount = progress; // Yeþil dolgu Image'ýnýn dolgu miktarýný güncelle
        }
        else
        {
            // Yükleme tamamlandý, diðer sahneye geçiþ
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void LoadingThread()
    {
        // Yükleme iþlemini simüle et
        for (int i = 0; i <= 100; i++)
        {
            LoadingProgress.SetProgress(i / 100f); // Ýþ parçacýðýndan ilerlemeyi güncelle
            Thread.Sleep(50); // Ýþlemi bekletme (simülasyon amaçlý)
        }

        // Yükleme tamamlandý
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
