using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using System.Linq;
using UnityEngine.Events;

[RequireComponent(typeof(ParticleSystem))]
public class EffectSpawn : MonoBehaviour
{
    private void OnEnable()
    {
        Gameplay.Instance.state = GameplayState.Prepare;
    }

    private void OnParticleSystemStopped()
    {
        Gameplay.Instance.state = GameplayState.Playing;
    }
}
