using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class GenericChanneled : ChanneledAbility
{
    public GenericChanneled()
    {
        ID = 2;
        Name = "Test2";
        imageFilePath = "Test2";
        Description = "This is an ability number 2";
        Cooldown = 2.0f;
        ChannelTime = 1.5f;
    }

    public void AlternateParameters()
    {
        ChannelTime = 5.5f;
    }

    public override void Cast()
    {
        throw new NotImplementedException();
    }
}
