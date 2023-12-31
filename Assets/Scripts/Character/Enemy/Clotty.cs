using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clotty : Character
{
    int[,] dir = new int[2, 5]
    {
        { 0,0,0,-1,1},
        {0,1,-1,0,0 },
    };
    protected override void Awake()
    {
        base.Awake();

        type = TYPE.enemy;
        index = 1;

        maxHP = new ObservableValue<int>(20, -1);
        curHP = new ObservableValue<int>(0, -1);
        curHP.Value = maxHP.Value;


        c_height = 0.8f;
        character_Shade.transform.localPosition = new Vector3(0, -c_height, 0);
        moveSpeed = 2f;
        frictionSpeed = new Vector2(0.4f, 0.4f);
        tearDamage = 1;
        tearSpeed = 6f;
        tearSpeedDivisionWhileMoving = 10f;
        tearRange = 6.5f;
        tearSize = 0.7f;


        skill_loadTimer = new() { 0.3f };
        skill_loadCD = new() { 2f };
        skill_usingMaxTime = new() { 1f };
        skill_usingTimer = new() { 0f };
        skill_range = new() { 0f };
        skill_emit = new() { false };
        skillFuncs = new() { Skill_0 };

        skill_loadTimer.Add(0.3f);
        skill_loadCD.Add(2.5f);
        skill_usingMaxTime.Add(0.2f);
        skill_usingTimer.Add(0f);
        skill_range.Add(9999f);
        skill_emit.Add(false);
        skillFuncs.Add(delegate { Skill_1(); });
    }
    private void Update()
    {
        base.EmitSkill();
    }

    private void Skill_1()
    {
        skill_emit[1] = true;
        for (int direction = 1;direction <=4;direction++)
        {
            tear.GenerateTear(gameObject, direction);
        }
        EndSkill_1();
    }
    public void EndSkill_1()
    {
        if (skill_usingTimer[1] <= skill_usingMaxTime[1])
        {
            skill_usingTimer[1] += 0.02f;
            Invoke(nameof(EndSkill_1), 0.02f);
            return;
        }
        skill_emit[1] = false;
        skill_usingTimer[1] = 0;
        skill_loadTimer[1] = 0;
        //Invoke(nameof(StopMove), slidingTime);
    }
}
