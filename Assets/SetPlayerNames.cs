using UnityEngine;
using TMPro;

public class SetPlayerNames : MonoBehaviour
{
    [SerializeField] GameStarter gameStarter;

    [Space]

    [SerializeField] GameObject nameInputPanel;

    [Space]

    [SerializeField] TextMeshProUGUI player1NameText;
    [SerializeField] TextMeshProUGUI player2NameText;

    [Space]

    [SerializeField] TMP_InputField inputField1;
    [SerializeField] TMP_InputField inputField2;

    bool isNameInputPanelActive = false;
    [HideInInspector] public bool isNameInputCompleted = false;

    void Start()
    {
        nameInputPanel.SetActive(false);
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
        player1NameText.text = inputField1.text;
        player2NameText.text = inputField2.text;

        isNameInputCompleted = true;

        Destroy(nameInputPanel, 0.1f);
    }
}
