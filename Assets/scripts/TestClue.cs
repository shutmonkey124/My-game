using UnityEngine;

public class TestClue : Interactable
{
    [Header("Evidence")]
    [SerializeField]
    private EvidenceData evidence = new EvidenceData();

    [Header("Inspection Dialogue")]
    [SerializeField]
    private DialogueLine[] dialogueLines;

    public override void Interact()
    {
        if (DialogueUI.Instance == null)
        {
            Debug.LogError(
                "No DialogueUI exists in the scene."
            );

            return;
        }

        if (EvidenceManager.Instance == null)
        {
            Debug.LogError(
                "No EvidenceManager exists in the scene."
            );

            return;
        }

        if (evidence == null ||
            string.IsNullOrWhiteSpace(evidence.Id))
        {
            Debug.LogError(
                "TestClue has no evidence ID configured."
            );

            return;
        }

        if (EvidenceManager.Instance.HasEvidence(evidence.Id))
        {
            ShowAlreadyCollectedMessage();
            return;
        }

        // Evidence is collected only after all inspection lines
        // have been read.
        DialogueUI.Instance.ShowDialogue(
            dialogueLines,
            CollectEvidence
        );
    }

    private void CollectEvidence()
    {
        bool wasAdded =
            EvidenceManager.Instance.AddEvidence(evidence);

        if (!wasAdded)
            return;

        DialogueLine[] confirmation =
        {
            new DialogueLine
            {
                speaker = "Officer",
                text = $"Evidence logged: {evidence.Title}."
            }
        };

        DialogueUI.Instance.ShowDialogue(confirmation);
    }

    private void ShowAlreadyCollectedMessage()
    {
        DialogueLine[] message =
        {
            new DialogueLine
            {
                speaker = "Officer",
                text =
                    $"{evidence.Title} is already recorded " +
                    "in my notes."
            }
        };

        DialogueUI.Instance.ShowDialogue(message);
    }
}