using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Interface adds a Channel Time to the IAbility Interface
/// </summary>
public interface IChanneledAbility : IAbility
{
    // How long should the Ability channel.
    // The Ability's Cooldown shouldn't start counting until the channel is finished.
    // TODO: Decide if an "isChanneling" belongs here or in the Player Class (as a state) 
    float ChannelTime { get; }
}
