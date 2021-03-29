using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task
{
    public abstract bool run();
}

public class IsTrue : Task
{
    bool varToTest;

    public IsTrue(bool someBool)
    {
        varToTest = someBool;

    }

    public override bool run()
    {
        return varToTest;
    }
}


public class IsFalse : Task
{
    bool varToTest;

    public IsFalse(bool someBool)
    {
        varToTest = someBool;
    }

    public override bool run()
    {
        return !varToTest;
    }
}

public class OpenDoor : Task
{
    Door mDoor;

    public OpenDoor(Door someDoor)
    {
        mDoor = someDoor;
    }

    public override bool run()
    {
        return !mDoor.Open();
    }
}

public class BargeDoor : Task
{
    Rigidbody mDoor;

    public BargeDoor(Rigidbody someDoor)
    {
        mDoor = someDoor;
    }

    public override bool run()
    {
        mDoor.AddForce(-10f, 0, 0, ForceMode.VelocityChange);
        return true;
    }
}

public class HulkOut : Task
{
    GameObject mEntity;

    public HulkOut(GameObject someEntity)
    {
        mEntity = someEntity;
    }

    public override bool run()
    {
        mEntity.transform.localScale *= 2;
        mEntity.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        return true;
    }
}

public class Wait : Task
{
    float mTimeToWait;

    public Wait(float time)
    {
        mTimeToWait = time;
    }

    public override bool run()
    {
        return true;
    }
}

public class MoveKinematicToObject : Task
{
    Arriver2 mMover;
    GameObject mTarget;

    public MoveKinematicToObject(Kinematic mover, GameObject target)
    {
        mMover = mover as Arriver2;
        mTarget = target;
    }

    public override bool run()
    {
        mMover.myTarget= mTarget;
        return true; 
    }
}

public class fixDoor : Task
{
    Rigidbody mDoor;
    public fixDoor(Rigidbody door)
    {
        mDoor = door;
    }
    public override bool run()
    {
        mDoor.AddForce(10f, 0, 0, ForceMode.VelocityChange);
        mDoor.transform.position = new Vector3(-0.55f, 3f, -5f);
        mDoor.transform.Rotate(0f, 0f, 0f);
        return true;
    }
}

public class stopAtObject : Task
{
    Arriver2 mMover;
    public stopAtObject(Kinematic mover)
    {
        mMover = mover as Arriver2;
    }
    public override bool run()
    {
        mMover.maxSpeed = 1f;
        return true;
    }
}

public class throwTreasure : Task
{
    Rigidbody mTarget;
    public throwTreasure(Rigidbody target)
    {
        mTarget = target;
    }
    public override bool run()
    {
        mTarget.AddForce(80f, 0, 0, ForceMode.VelocityChange);
        return true;
    }
}

public class Sequence : Task
{
    List<Task> children;


    public Sequence(List<Task> taskList)
    {
        children = taskList;
    }

    public override bool run()
    {
        foreach (Task child in children)
        {
            if (child.run() == false)
                return false;
        }
        return true;
    }
}
public class Selector : Task
    {
        List<Task> children;

        public Selector(List<Task> taskList)
        {
            children = taskList;
        }
        public override bool run()
        {
            foreach (Task child in children)
            {
                if (child.run())
                    return true;
            }
            return false;
        }
    }

