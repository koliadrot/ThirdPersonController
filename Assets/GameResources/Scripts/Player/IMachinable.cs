using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMachinable
{
    GroundedState GroundedState { get; }
    RunPlayerState RunState { get; }
    IdlePlayerState IdleState { get; }
    JumpPlayerState JumpState { get; }
}
