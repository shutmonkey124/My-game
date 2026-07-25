using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (dialoguePanel != null &&
            dialoguePanel.activeSelf &&
            (Input.GetKeyDown(KeyCode.Space) ||
             Input.GetKeyDown(KeyCode.Escape)))
        {
            HideDialogue();
        }
    }

    public void ShowDialogue(string message)
    {
        if (dialoguePanel == null || dialogueText == null)
        {
            Debug.LogError("DialogueUI references are not connected.");
            return;
        }

        dialogueText.text = message;
        dialoguePanel.SetActive(true);
    }

    public void HideDialogue()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }
}