using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChanneledAbility : IAbility
{ 
    float ChannelTime { get; }
}
