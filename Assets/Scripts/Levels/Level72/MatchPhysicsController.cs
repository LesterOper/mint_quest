using System.Collections;
using System.Collections.Generic;
using App.Global;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class MatchPhysicsController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private MatchAndFireMatchboxFirecontroller _matchboxFirecontroller;

    private bool isComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isComplete) return;
        if (rb.transform.position.y <= -3.5f)
        {
            Context.SoundPlayer.StopPlayingSounds(_matchboxFirecontroller.Source);
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
            isComplete = true;
        }
    }

    public void ActivatePhysics()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
