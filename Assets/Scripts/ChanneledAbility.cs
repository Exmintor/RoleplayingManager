using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class ChanneledAbility : Ability, IChanneledAbility
{
    [SerializeField, HideInInspector]
    private float channelTime;
    public float ChannelTime
    {
        get
        {
            return channelTime;
        }
        protected set
        {
            channelTime = value;
        }
    }

    public override abstract void Cast();
}
