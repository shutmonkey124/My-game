using UnityEngine;

public class TestClue : Interactable
{
    [TextArea(2, 5)]
    [SerializeField]
    private string[] clueLines =
    {
        "There are fresh scratches around the lock.",
        "Someone forced this door open recently.",
        "But nothing inside appears to have been stolen..."
    };

    public override void Interact()
    {
        if (DialogueUI.Instance == null)
        {
            Debug.LogError("No DialogueUI exists in the scene.");
            return;
        }

        DialogueUI.Instance.ShowDialogue(clueLines);
    }
}