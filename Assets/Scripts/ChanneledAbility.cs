using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class ChanneledAbility : Ability, IChanneledAbility
{
    public float ChannelTime { get; protected set; }

    public override abstract void Cast();
}
