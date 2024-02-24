using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class NameScoreManager : MonoBehaviour
{
    public static NameScoreManager Instance;

    public int m_HighScore;
    public Text m_Name;
    public string m_HSName;
    public TextMeshProUGUI highScoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadAllData();
    }

    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text = $"High Score: {m_HighScore} \nBy: {m_HSName}";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        m_Name = GameObject.Find("Name Text").GetComponent<Text>();
        SceneManager.LoadScene("main");
    }

    [System.Serializable]
    class SaveData
    {
        public int m_HighScore = 0;
        public string m_HSName;
    }

    public void SaveAllData()
    {
        SaveData data = new SaveData();
        data.m_HSName = m_HSName;
        data.m_HighScore = m_HighScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadAllData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            m_HSName = data.m_HSName;
            m_HighScore = data.m_HighScore;
        }
    }
}
