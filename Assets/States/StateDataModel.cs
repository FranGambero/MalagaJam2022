using System;
using System.Collections.Generic;
using UnityEngine;

namespace MJam22.States
{
    [Serializable]
    public class StateDataModel
    {
        [Header("Audio")] 
        [SerializeField] public AudioClip clip;
        [SerializeField] public float bpm;
        [SerializeField] public float firstBeatOffset;
        [SerializeField] public float secondsToArrive; 
        
        [Header("Cycle Time")]
        [SerializeField] public int duration;

        [Header("State")] 
        [SerializeField] public bool isOffice;
        
        [Header("Stress")] 
        [SerializeField] public float timedStress;
        [SerializeField] public float hitStress;
        [SerializeField] public float missStress;
        
        [Header("Tracks")]
        [SerializeField] public List<float> FirstTrack;
        [SerializeField] public List<float> SecondTrack;
        [SerializeField] public List<float> ThirdTrack;
        [SerializeField] public List<float> FourthTrack;
    }
}