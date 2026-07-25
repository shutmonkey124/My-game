using UnityEngine;

public class TestClue : Interactable
{
    [TextArea]
    [SerializeField]
    private string clueText =
        "There are fresh scratches around the lock.";

    public override void Interact()
    {
        if (DialogueUI.Instance == null)
        {
            Debug.LogError("No DialogueUI exists in the scene.");
            return;
        }

        DialogueUI.Instance.ShowDialogue(clueText);
    }
}