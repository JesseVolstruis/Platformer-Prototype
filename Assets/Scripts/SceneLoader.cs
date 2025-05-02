using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    public GameObject[] textArray = new GameObject[10];
    [SerializeField]
    private GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        SceneComplete(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
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
    }

    public static void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SceneComplete(Scene scene1, Scene scene2)
    {
        pauseMenu = GameObject.FindGameObjectWithTag("Pause");
        pauseMenu.transform.localScale = Vector3.zero;
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
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= SceneComplete;
    }

    private void TogglePause()
    {
        if(pauseMenu.transform.localScale == Vector3.one)
        {
            pauseMenu.transform.localScale = Vector3.zero;
            Debug.Log("1");
        }
        else if (pauseMenu.transform.localScale == Vector3.zero)
        {
            pauseMenu.transform.localScale = Vector3.one;
            Debug.Log("2");
        }
    }


}
