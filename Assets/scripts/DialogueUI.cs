using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    private string[] currentLines;
    private int currentLineIndex;

    public bool IsOpen =>
        dialoguePanel != null && dialoguePanel.activeSelf;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HideDialogue();
    }

    private void Update()
    {
        if (!IsOpen)
            return;

        // Space advances to the next line.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextLine();
        }

        // Escape immediately closes the conversation.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideDialogue();
        }
    }

    public void ShowDialogue(string[] lines)
    {
        if (dialoguePanel == null || dialogueText == null)
        {
            Debug.LogError(
                "DialogueUI references are not connected."
            );
            return;
        }

        if (lines == null || lines.Length == 0)
        {
            Debug.LogWarning("Dialogue has no lines.");
            return;
        }

        currentLines = lines;
        currentLineIndex = 0;

        dialoguePanel.SetActive(true);
        dialogueText.text = currentLines[currentLineIndex];
    }

    private void ShowNextLine()
    {
        currentLineIndex++;

        // Close after the final line.
        if (currentLineIndex >= currentLines.Length)
        {
            HideDialogue();
            return;
        }

        dialogueText.text = currentLines[currentLineIndex];
    }

    public void HideDialogue()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        currentLines = null;
        currentLineIndex = 0;
    }
}