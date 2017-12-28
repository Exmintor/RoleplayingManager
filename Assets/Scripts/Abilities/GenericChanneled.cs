using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GenericChanneled : ChanneledAbility
{
    public GenericChanneled()
    {
        ID = 100;
        Name = "GenericChanneled";
        ImageFilePath = "GenericChanneled";
        Description = "This is a generic channeled ability";
        Cooldown = 0.0f;
        ChannelTime = 0.0f;
    }

    public override void Cast()
    {
        throw new NotImplementedException();
    }
}
