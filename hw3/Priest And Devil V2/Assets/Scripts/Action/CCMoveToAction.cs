using System;
using System.Collections;
using UnityEngine;

namespace ActionSpace
{
    //此类的作是将一个物体移动到目标位置，并通知人物完成
    //这个的局限性在于 他只能进行直线运动，不能转弯
    public class CCMoveToAction:SSAction
    {
        public Vector3 target;
        public float speed;

        public static CCMoveToAction GetSSAction(Vector3 target, float speed)
        {
            CCMoveToAction action = ScriptableObject.CreateInstance<CCMoveToAction> ();
            action.speed = speed;
            action.target = target;
            return action;

        }

        public override void Start()
        {

        }

        public override void Update()
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed*Time.deltaTime);
            if(this.transform.position == target)
            {
                destroy = true;
                callback.SSActionEvent(this);
            }
        }
    }
}