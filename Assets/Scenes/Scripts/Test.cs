using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SpineAnimation]
    public string runAnimationName;

    SkeletonAnimation skeletonAnimation;

    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;
    private TrackEntry track;

    [SpineEvent(dataField: "skeletonytiiAnimation", fallbackToTextField: true)]
    public string footstepEventName;
    Spine.EventData footstepEventData;

    public AudioSource audioSource;
    void OnValidate()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }
    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
        //StartCoroutine(IEDoExample());
        track = spineAnimationState.SetAnimation(0, runAnimationName, true);



        skeletonAnimation.Initialize(false);
        if (!skeletonAnimation.valid) return;
        footstepEventData = skeletonAnimation.Skeleton.Data.FindEvent(footstepEventName);
        skeletonAnimation.AnimationState.Event += HandleAnimationStateEvent;
    }

    private void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if(footstepEventData == e.Data)
        {
            Debug.Log("Event Foot");
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            track.TimeScale = 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            track.TimeScale = 1f;
        }
    }

    private IEnumerator IEDoExample()
    {
        while (true)
        {
            spineAnimationState.SetAnimation(0, runAnimationName, true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
