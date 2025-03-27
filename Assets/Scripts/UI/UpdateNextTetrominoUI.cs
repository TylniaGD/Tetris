using UnityEngine;

public class UpdateNextTetrominoUI : MonoBehaviour
{
    public GameObject[] tetrominoesUI;

    void Start()
    {
        foreach (GameObject tUI in tetrominoesUI)
        {
            tUI.SetActive(false);
        }
    }

    void SetNewTetrominoUI(Player player, int nextTetrominoID)
    {
        if (player.name + "UI" != LayerMask.LayerToName(gameObject.layer)) return;

        foreach (GameObject tUI in tetrominoesUI)
        {
            tUI.SetActive(false);
        }

        if (nextTetrominoID >= 0 && nextTetrominoID < tetrominoesUI.Length)
        {
            tetrominoesUI[nextTetrominoID].SetActive(true);
        }
    }
    
    void OnEnable()
    {
        TetrisGameManager.OnNextTetrominoChanged += SetNewTetrominoUI;
    }

    void OnDisable()
    {
        TetrisGameManager.OnNextTetrominoChanged -= SetNewTetrominoUI;
    }
}
