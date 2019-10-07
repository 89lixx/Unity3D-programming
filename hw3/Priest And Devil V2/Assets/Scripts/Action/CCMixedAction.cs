using System;
using UnityEngine;
using System.Collections.Generic;
using MySpace;
namespace ActionSpace
{
    //CCMoveToAction 只是一个能够直线运动的模版
    //这个类就是将上面的类组合起来，由于model的动作都是直线运动，所以无需在创建其他类
    //对于人物来说他们的动作是折线运动，组合一下就好
    public class CCMixedAction:SSAction,ISSActionCallback
    {
        public List<SSAction> Actions;
        public int repeat = -1;
        public int start = 0;
        public static CCMixedAction GetSSAction(int repeat, int start, List<SSAction>Actions)
        {
            CCMixedAction action = ScriptableObject.CreateInstance<CCMixedAction>();
            action.repeat = repeat;
            action.start = start;
            action.Actions = Actions;
            return action;
        }

        public override void Update()
        {
            //如果系列动作都做完了
            if (Actions.Count == 0) return;

            //还没做完
            if (start < Actions.Count)
            {
                Actions[start].Update();
            }
        }

        public void SSActionEvent(SSAction source)
        {
            source.destroy = false;
            this.start++;
            if(this.start >= this.Actions.Count)
            {
                this.start = 0;
                if (this.repeat > 0) this.repeat--;
                if(this.repeat == 0)
                {
                    this.destroy = true;
                    this.callback.SSActionEvent(this);
                }
            }
        }

        public override void Start()
        {
            foreach (SSAction action in this.Actions)
            {
                action.gameobject = this.gameobject;
                action.transform = this.transform;
                action.callback = this;
                action.Start();
            }
        }

        //释放物体
        void OnDestroy()
        {
            foreach(SSAction action in this.Actions)
            {
                DestroyObject(action);
                //action.Destrio;
            }
        }
    }
}
