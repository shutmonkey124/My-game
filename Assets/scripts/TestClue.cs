using UnityEngine;

public class TestClue : Interactable
{
    [SerializeField]
    private DialogueLine[] dialogueLines =
    {
        new DialogueLine
        {
            speaker = "Officer",
            text = "There are fresh scratches around the lock."
        },
        new DialogueLine
        {
            speaker = "Officer",
            text = "Someone forced this door open recently."
        },
        new DialogueLine
        {
            speaker = "Officer",
            text = "But nothing inside appears to have been stolen..."
        }
    };

    public override void Interact()
    {
        if (DialogueUI.Instance == null)
        {
            Debug.LogError("No DialogueUI exists in the scene.");
            return;
        }

        DialogueUI.Instance.ShowDialogue(dialogueLines);
    }
}