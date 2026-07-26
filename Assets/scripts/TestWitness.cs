using UnityEngine;

public class TestWitness : Interactable
{
    public override void Interact()
    {
        if (DialogueUI.Instance == null)
        {
            Debug.LogError("No DialogueUI exists.");
            return;
        }

        DialogueLine[] introduction =
        {
            new DialogueLine
            {
                speaker = "Officer",
                text = "I need to ask you a few questions."
            },
            new DialogueLine
            {
                speaker = "Witness",
                text = "I already told the other officer. I didn't see anything."
            }
        };

        DialogueUI.Instance.ShowDialogue(
            introduction,
            ShowQuestionMenu
        );
    }

    private void ShowQuestionMenu()
    {
        string[] choices =
        {
            "Ask calmly",
            "Pressure the witness",
            "End the conversation"
        };

        DialogueUI.Instance.ShowChoices(
            "Officer",
            "How should I respond?",
            choices,
            HandleChoice
        );
    }

    private void HandleChoice(int choiceIndex)
    {
        switch (choiceIndex)
        {
            case 0:
                AskCalmly();
                break;

            case 1:
                PressureWitness();
                break;

            case 2:
                EndConversation();
                break;
        }
    }

    private void AskCalmly()
    {
        DialogueLine[] response =
        {
            new DialogueLine
            {
                speaker = "Officer",
                text = "Take your time. What exactly did you hear?"
            },
            new DialogueLine
            {
                speaker = "Witness",
                text = "Footsteps... then a door slammed around 10:15."
            }
        };

        DialogueUI.Instance.ShowDialogue(
            response,
            ShowQuestionMenu
        );
    }

    private void PressureWitness()
    {
        DialogueLine[] response =
        {
            new DialogueLine
            {
                speaker = "Officer",
                text = "You're withholding evidence from a murder investigation."
            },
            new DialogueLine
            {
                speaker = "Witness",
                text = "Fine! I saw someone wearing a dark coat. That's all."
            }
        };

        DialogueUI.Instance.ShowDialogue(
            response,
            ShowQuestionMenu
        );
    }

    private void EndConversation()
    {
        DialogueLine[] response =
        {
            new DialogueLine
            {
                speaker = "Officer",
                text = "We're done for now."
            }
        };

        DialogueUI.Instance.ShowDialogue(response);
    }
}