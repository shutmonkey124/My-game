using System;
using System.Text;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text dialogueText;

    private DialogueLine[] currentLines;
    private int currentLineIndex;
    private Action dialogueFinished;

    private bool waitingForChoice;
    private int currentChoiceCount;
    private Action<int> choiceSelected;

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideDialogue();
            return;
        }

        if (waitingForChoice)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) ||
                Input.GetKeyDown(KeyCode.Keypad1))
            {
                SelectChoice(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) ||
                     Input.GetKeyDown(KeyCode.Keypad2))
            {
                SelectChoice(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) ||
                     Input.GetKeyDown(KeyCode.Keypad3))
            {
                SelectChoice(2);
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextLine();
        }
    }

    public void ShowDialogue(
        DialogueLine[] lines,
        Action onFinished = null)
    {
        if (!ReferencesAreConnected())
            return;

        if (lines == null || lines.Length == 0)
        {
            Debug.LogWarning("Dialogue has no lines.");
            return;
        }

        currentLines = lines;
        currentLineIndex = 0;
        dialogueFinished = onFinished;

        waitingForChoice = false;
        currentChoiceCount = 0;
        choiceSelected = null;

        dialoguePanel.SetActive(true);
        DisplayCurrentLine();
    }

    public void ShowChoices(
        string speaker,
        string prompt,
        string[] choices,
        Action<int> onChoiceSelected)
    {
        if (!ReferencesAreConnected())
            return;

        if (choices == null || choices.Length == 0)
        {
            Debug.LogWarning("There are no dialogue choices.");
            return;
        }

        currentLines = null;
        dialogueFinished = null;

        waitingForChoice = true;
        currentChoiceCount = Mathf.Min(choices.Length, 3);
        choiceSelected = onChoiceSelected;

        dialoguePanel.SetActive(true);
        speakerText.text = speaker;

        StringBuilder choiceDisplay = new StringBuilder();

        choiceDisplay.AppendLine(prompt);
        choiceDisplay.AppendLine();

        for (int i = 0; i < currentChoiceCount; i++)
        {
            choiceDisplay.AppendLine(
                $"[{i + 1}] {choices[i]}"
            );
        }

        dialogueText.text = choiceDisplay.ToString();
    }

    private void SelectChoice(int choiceIndex)
    {
        if (choiceIndex < 0 ||
            choiceIndex >= currentChoiceCount)
        {
            return;
        }

        Action<int> callback = choiceSelected;

        waitingForChoice = false;
        currentChoiceCount = 0;
        choiceSelected = null;

        callback?.Invoke(choiceIndex);
    }

    private void ShowNextLine()
    {
        if (currentLines == null)
            return;

        currentLineIndex++;

        if (currentLineIndex >= currentLines.Length)
        {
            Action finishedCallback = dialogueFinished;

            HideDialogue();
            finishedCallback?.Invoke();

            return;
        }

        DisplayCurrentLine();
    }

    private void DisplayCurrentLine()
    {
        DialogueLine line = currentLines[currentLineIndex];

        speakerText.text = line.speaker;
        dialogueText.text = line.text;
    }

    private bool ReferencesAreConnected()
    {
        if (dialoguePanel != null &&
            speakerText != null &&
            dialogueText != null)
        {
            return true;
        }

        Debug.LogError(
            "DialogueUI references are not connected."
        );

        return false;
    }

    public void HideDialogue()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        currentLines = null;
        currentLineIndex = 0;
        dialogueFinished = null;

        waitingForChoice = false;
        currentChoiceCount = 0;
        choiceSelected = null;
    }
}