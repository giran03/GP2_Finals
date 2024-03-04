using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleUnlockerHandler : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] List<int> solutionCode;
    [SerializeField] Animator animator;
    [SerializeField] string animName;

    [HideInInspector] public List<int> answerCode;

    GameObject[] buttonCollection;
    ButtonInteraction buttonInteraction;
    GameManager gameManager;

    private void Start()
    {
        buttonCollection = GameObject.FindGameObjectsWithTag("Button");
        gameManager = GameManager.Instance;
    }

    private void Update() => CompareValues();

    void CompareValues()
    {
        if (answerCode.Count == solutionCode.Count && solutionCode.SequenceEqual(answerCode))
            AcceptAnswer();
        else if (answerCode.Count >= solutionCode.Count && !solutionCode.SequenceEqual(answerCode))
            ResetAnswer();
    }

    void AcceptAnswer() => gameManager.TriggerAnimation(animator, animName);

    void ResetAnswer()
    {
        answerCode.Clear();
        foreach (var item in buttonCollection)
        {
            buttonInteraction = item.GetComponent<ButtonInteraction>();
            buttonInteraction.ButtonRedLight();
        }
    }
}
