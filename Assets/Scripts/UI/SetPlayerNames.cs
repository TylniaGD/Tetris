using UnityEngine;
using TMPro;
using System.Collections;

public class SetPlayerNames : MonoBehaviour
{
    GameStarter gameStarter;
    TetrisGameManager tetrisGameManager;

    [Space]

    [SerializeField] GameObject nameInputPanel;

    [Space]

    [SerializeField] TextMeshProUGUI player1NameText;
    [SerializeField] TextMeshProUGUI player2NameText;

    [Space]

    [SerializeField] TMP_InputField inputField1;
    [SerializeField] TMP_InputField inputField2;

    bool isNameInputPanelActive = false;

    void Start()
    {
        nameInputPanel.SetActive(false);
        gameStarter = FindAnyObjectByType<GameStarter>();
    }

    void Awake()
    {
        StartCoroutine(FindTetrisGameManager());
    }

    void Update()
    {
        if (gameStarter.gameIsStarted && !isNameInputPanelActive)
        {
            isNameInputPanelActive = true;
            nameInputPanel.SetActive(true);

            inputField1.Select();
            inputField1.ActivateInputField();
        }
    }

    public void ApplyPlayerNames()
    {
        string name1 = inputField1.text.Trim(); // Prevents entering only spaces 
        string name2 = inputField2.text.Trim();

        if (string.IsNullOrEmpty(name1))
        {
            inputField1.text = "CAN'T BE NULL";
            return;
        }
        else if (string.IsNullOrEmpty(name2))
        {
            inputField2.text = "CAN'T BE NULL";
            return;
        }

        player1NameText.text = inputField1.text;
        player2NameText.text = inputField2.text;

        tetrisGameManager.isNameInputCompleted = true;

        Destroy(nameInputPanel, 0.1f);
    }

    IEnumerator FindTetrisGameManager()
    {
        yield return new WaitUntil(() => FindAnyObjectByType<TetrisGameManager>() != null);
        tetrisGameManager = FindAnyObjectByType<TetrisGameManager>();
    }
}
