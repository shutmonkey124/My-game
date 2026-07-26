using System;
using System.Collections.Generic;
using UnityEngine;

public class TestWitness : Interactable
{
    private bool askedCalmly;
    private bool pressuredWitness;

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
        List<string> choices = new List<string>();
        List<Action> actions = new List<Action>();

        if (!askedCalmly)
        {
            choices.Add("Ask calmly");
            actions.Add(AskCalmly);
        }

        if (!pressuredWitness)
        {
            choices.Add("Pressure the witness");
            actions.Add(PressureWitness);
        }

        choices.Add("End the conversation");
        actions.Add(EndConversation);

        DialogueUI.Instance.ShowChoices(
            "Officer",
            "How should I respond?",
            choices.ToArray(),
            selectedIndex =>
            {
                if (selectedIndex >= 0 &&
                    selectedIndex < actions.Count)
                {
                    actions[selectedIndex].Invoke();
                }
            }
        );
    }

    private void AskCalmly()
    {
        askedCalmly = true;

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
        pressuredWitness = true;

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