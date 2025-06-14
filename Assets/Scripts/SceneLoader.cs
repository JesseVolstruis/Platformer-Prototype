using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    public GameObject[] textArray = new GameObject[10];
    public GameObject[] coinTextArray = new GameObject[4];
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject deathCount;
    [SerializeField]
    private int count;
    public int coinCount = 0;
    private int level1Coins;
    private int level2Coins;
    private int level3Coins;
    private int level4Coins;
    private int level5Coins;
    private GameObject level5Object;
    private GameObject winText;
    private GameObject winTextCoin;
    // Start is called before the first frame update
    void Start()
    {
        level5Object = GameObject.Find("Level 5");
        SceneComplete(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
        pauseMenu.transform.localScale = Vector3.zero;
        Time.timeScale = 1;
        //level5Object = GameObject.Find("Level 5");
        //level5Object.transform.localScale = new Vector3(108, 108, 108);

    }

    // Update is called once per frame
    void Update()
    {
        DeathCountColor();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        deathCount.GetComponent<TextMeshProUGUI>().text = "Deaths: " + count;
        TrackCoins();
    }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            PlayerPrefs.DeleteAll();
        }
        DontDestroyOnLoad(Instance);
        SceneManager.activeSceneChanged += SceneComplete;
        DeathManager.onDeath += IncrementDeath;
    }

    public static void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
        Debug.Log("Loading " + scene);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public void SceneComplete(Scene scene1, Scene scene2)
    {
        pauseMenu = GameObject.FindGameObjectWithTag("Pause");
        pauseMenu.transform.localScale = Vector3.zero;
        deathCount = GameObject.Find("DeathCount");
        if(!SceneManager.GetActiveScene().name.Equals("Level Select")) 
        { 
            return;
        }
        textArray = GameObject.FindGameObjectsWithTag("LevelNumber");
        if (PlayerPrefs.GetInt("Level1") == 1)
        {
            textArray[0].GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        if (PlayerPrefs.GetInt("Level2") == 1)
        {
            textArray[1].GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        if (PlayerPrefs.GetInt("Level3") == 1)
        {
            textArray[2].GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        if (PlayerPrefs.GetInt("Level4") == 1)
        {
            textArray[3].GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        if (PlayerPrefs.GetInt("Level5") == 1)
        {
            textArray[4].GetComponent<TextMeshProUGUI>().color = Color.green;
            winText = GameObject.Find("Win Text");
            if (winText != null)
            { 
            winText.transform.localScale = Vector3.one;
            }
        }
        if (PlayerPrefs.GetInt("Level1") == 1 &&
            PlayerPrefs.GetInt("Level2") == 1 &&
            PlayerPrefs.GetInt("Level3") == 1 &&
            PlayerPrefs.GetInt("Level4") == 1)
        {
            level5Object = GameObject.Find("Level 5");
            if (level5Object != null)
            {
                level5Object.transform.localScale = new Vector3(108, 108, 108);
            }
        }
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= SceneComplete;
        DeathManager.onDeath -= IncrementDeath;
    }

    private void TogglePause()
    {
        if(pauseMenu.transform.localScale == Vector3.one)
        {
            pauseMenu.transform.localScale = Vector3.zero;
            Time.timeScale = 1;
        }
        else if (pauseMenu.transform.localScale == Vector3.zero)
        {
            pauseMenu.transform.localScale = Vector3.one;
            Time.timeScale = 0;
        }
    }

    private void IncrementDeath(int i)
    {
        count = i;
    }
    private void DeathCountColor()
    {
        if(count == 0)
        {
            deathCount.GetComponent<TextMeshProUGUI>().color = new Color32(232,182,0,255);
        }
        else if(count < 100)
        {
            deathCount.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            deathCount.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
    }

    private void TrackCoins()
    {
        if (!SceneManager.GetActiveScene().name.Equals("Level Select"))
        {
            return;
        }
        coinTextArray = GameObject.FindGameObjectsWithTag("CoinCount");
        level1Coins = PlayerPrefs.GetInt("Level1Coins");
        level2Coins = PlayerPrefs.GetInt("Level2Coins");
        level3Coins = PlayerPrefs.GetInt("Level3Coins");
        level4Coins = PlayerPrefs.GetInt("Level4Coins");
        level5Coins = PlayerPrefs.GetInt("Level5Coins");
        coinTextArray[0].GetComponent<TextMeshProUGUI>().text = level1Coins +"/2";
        coinTextArray[1].GetComponent<TextMeshProUGUI>().text = level2Coins + "/3";
        coinTextArray[2].GetComponent<TextMeshProUGUI>().text = level3Coins + "/4";
        coinTextArray[3].GetComponent<TextMeshProUGUI>().text = level4Coins + "/3";
        coinTextArray[4].GetComponent<TextMeshProUGUI>().text = level5Coins + "/4";
        if (level1Coins +
            level2Coins +
            level3Coins +
            level4Coins +
            level5Coins == 16)
        {
            winTextCoin = GameObject.Find("Win Text (coin)");
            if (winTextCoin != null)
            {
                winTextCoin.transform.localScale = Vector3.one;
            }
            textArray[0].GetComponent<TextMeshProUGUI>().color = new Color32(232, 182, 0, 255);
            textArray[1].GetComponent<TextMeshProUGUI>().color = new Color32(232, 182, 0, 255);
            textArray[2].GetComponent<TextMeshProUGUI>().color = new Color32(232, 182, 0, 255);
            textArray[3].GetComponent<TextMeshProUGUI>().color = new Color32(232, 182, 0, 255);
            textArray[4].GetComponent<TextMeshProUGUI>().color = new Color32(232, 182, 0, 255);
            winTextCoin.transform.localScale = Vector3.one;
        }
    }


}
