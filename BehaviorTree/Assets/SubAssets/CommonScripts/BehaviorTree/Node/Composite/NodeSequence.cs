﻿using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// 顺序节点(组合节点)
    /// </summary>
    public class NodeSequence : NodeComposite
    {
        private NodeBase lastRunningNode;
        public static string descript = "顺序节点依次执行子节点，只要节点返回Success，就\n" +
                                         "继续执行后续节点，直到一个节点返回 Fail 或者 \n" +
                                         "Running，停止执行后续节点，向父节点返回 Fail \n" +
                                         "或者 Running，如果所有节点都返回 Success，则向\n" +
                                         "父节点返回 Success和选择节点一样，如果一个节点\n" +
                                         "返回 Running，则需要记录该节点，下次执行时直接\n" +
                                         "从该节点开始执行";

        public NodeSequence():base(NODE_TYPE.SEQUENCE)
        {  }

        public override void OnEnter()
        {
            base.OnEnter();
            lastRunningNode = null;
        }

        public override void OnExit()
        {
            base.OnExit();

            if (null != lastRunningNode)
            {
                lastRunningNode.Postposition(ResultType.Fail);
                lastRunningNode = null;
            }
        }

        public override ResultType Execute()
        {
            int index = 0;
            if (lastRunningNode != null)
            {
                index = lastRunningNode.NodeIndex;
            }
            lastRunningNode = null;

            ResultType resultType = ResultType.Fail;
            for (int i = index; i < nodeChildList.Count; ++i)
            {
                NodeBase nodeBase = nodeChildList[i];

                nodeBase.Preposition();
                resultType = nodeBase.Execute();
                nodeBase.Postposition(resultType);

                if (resultType == ResultType.Fail)
                {
                    break;
                }

                if (resultType == ResultType.Success)
                {
                    continue;
                }

                if (resultType == ResultType.Running)
                {
                    lastRunningNode = nodeBase;
                    break;
                }
            }

            NodeNotify.NotifyExecute(EntityId, NodeId, resultType, Time.realtimeSinceStartup);
            return resultType;
        }
    }
}

/*

    index = 1
    if != lastRunningNode null then
        index = lastRunningNode.index
    end

    lastRunningNode = null
    for i <- index to N do 
    
        Node node =  GetNode(i);

        result = node.execute()
        
        if result == fail then
           return fail;
        end

        if result == running then
            lastRunningNode = node
            return running
        end

    end

    return success



*/
