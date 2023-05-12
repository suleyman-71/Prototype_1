using UnityEngine;

[System.Serializable]
public class UserProgress
{
    public int[] levelScores;

    public UserProgress(int levelCount)
    {
        levelScores = new int[levelCount];
    }

    public void SaveProgress()
    {
        string jsonData = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("UserProgress", jsonData);
        PlayerPrefs.Save();
    }

    public static UserProgress LoadProgress()
    {
        string jsonData = PlayerPrefs.GetString("UserProgress", "");
        if (!string.IsNullOrEmpty(jsonData))
        {
            return JsonUtility.FromJson<UserProgress>(jsonData);
        }
        else
        {
            // �lk kez kaydediliyorsa varsay�lan ilerleme de�erlerini kullanabilirsiniz
            int defaultLevelCount = 10; // Varsay�lan seviye say�s�
            UserProgress defaultProgress = new UserProgress(defaultLevelCount);
            return defaultProgress;
        }
    }
}
