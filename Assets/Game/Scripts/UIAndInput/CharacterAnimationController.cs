using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private AudioSource footStepSound;
    
    public void FootStep()
    {
        Debug.Log("FootStep Sound here!");
        footStepSound.Play();
    }
}
