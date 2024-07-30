using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] GameObject playerNamePanel;
    [SerializeField] TextMeshProUGUI placeholder;
    [SerializeField] TMP_InputField inputText;
    private string playerInputText;


    private void Start()
    {
        // loads saved name data and cheks if empty
        string savedName = GamePersistence.Instance.Highscore.Name;
        if (savedName != "")
        {
            inputText.text = savedName;
        }

    }

    public void StartNew()
    {
        // Getting the parent of the player name panel
        Transform parent = playerNamePanel.transform.parent.gameObject.transform;

        // setting each child item to inActive
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        // setting the panel to active
        playerNamePanel.SetActive(true);
        inputText.ActivateInputField();
    }

    public void ReadPlayerInput(string text)
    {
        playerInputText = text;

        if (playerInputText == "")
        {
            // Getting inputfield that is a child of the panel
            GameObject child = playerNamePanel.transform.GetChild(1).gameObject;
            placeholder.text = "Please enter your name...";
            placeholder.color = Color.red;
            inputText.ActivateInputField();
            return;
        }

        if (GamePersistence.Instance.Name != playerInputText)
        {
            GamePersistence.Instance.Name = playerInputText;
            GamePersistence.Instance.Score = 0;
        }
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


} // MenuUIHandler
