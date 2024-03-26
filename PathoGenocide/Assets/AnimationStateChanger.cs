using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateChanger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string currentState = "LeukIdle_Clip";

    void Start()
    {

    }

    void Update()
    {

    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
        {
            return;
        }
        currentState = newState;
        animator.Play(currentState);
    }
}
