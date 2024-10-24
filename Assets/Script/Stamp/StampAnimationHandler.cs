using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StampAnimationHandler : MonoBehaviour
{
    [SerializeField] private Image stampedPaper;
    [SerializeField] private GameObject stampedBody;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


}
