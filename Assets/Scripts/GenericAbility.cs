﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GenericAbility : Ability
{
    public GenericAbility()
    {
        ID = 1;
        Name = "Test";
        ImageFilePath = "Test";
        Description = "This is an ability";
        Cooldown = 1.0f;
    }
	
    public override void Cast()
    {
        throw new NotImplementedException();
    }
}
