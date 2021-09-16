////////////////////////////////////////////////////////////////////////////////
//
//	MG_TargetGroup.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_Liquidator
{

    public static class MG_TargetGroup
    {
        #region Properties            
        public static int RelationsGroup { get;  set; }// = Function.Call<int>(Hash.GET_HASH_KEY, "TARGET_TEAM");
        public static PedGroup PedGroup { get;  set; }
        public static int GroupID { get;  set; }
        #endregion Properties


        #region Public Methods

        public static void InitTargetGroup(Ped ped)
        {
            string targetGroupName = "TARGET_TEAM" + MG_Statistic.TotalTargetsEliminated.ToString() + "" + MG_Random.Random();
            RelationsGroup = World.AddRelationshipGroup(targetGroupName);
            //Ped target = MG_Target.Ped;
            Ped target = ped;

            while (target.CurrentPedGroup == null)
            {
                SetGroupLeader(target);
                //Wait(1);
            }
            //SetRelationsWithPlayer(target);
            SetFormation(target);
            SetGroupRelations();
            
        }
        #endregion Public Methods

        #region Private Methods

        private static void SetGroupLeader(Ped target)
        {
            GroupID = Function.Call<int>(Hash.CREATE_GROUP, RelationsGroup);
            Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, target, GroupID);
            Function.Call(Hash.SET_PED_AS_GROUP_LEADER, target, GroupID);
            PedGroup = target.CurrentPedGroup;
            target.RelationshipGroup = RelationsGroup;
        }

        //private static void SetRelationsWithPlayer(Ped target)
        //{
        //    Function.Call(Hash.SET_PED_RELATIONSHIP_GROUP_HASH, target, RelationsGroup);
        //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 3, RelationsGroup, MG_Player.Ped.RelationshipGroup);
        //    //0 = Companion
        //    //1 = Respect
        //    //2 = Like
        //    //3 = Neutral
        //    //4 = Dislike
        //    //5 = Hate
        //    //255 = Pedestrians
        //}

        private static void SetFormation(Ped target)
        {
            Function.Call(Hash.SET_PED_CAN_BE_TARGETTED_BY_TEAM, target, GroupID, false);
            //Function.Call(Hash.SET_GROUP_FORMATION, Group, 2);//31.01.2020
            //  0: Default
            //  1: Circle Around Leader
            //  2: Alternative Circle Around Leader
            //  3: Line, with Leader at center
            Function.Call(Hash.SET_GROUP_FORMATION_SPACING, GroupID, 20f, 20f, 20f);//31.01.2020
            List<FormationType> enums = Enum.GetValues(typeof(FormationType)).Cast<FormationType>().ToList();
            target.CurrentPedGroup.FormationType = MG_Random.RandomElement(enums);
        }

        private static void SetGroupRelations()
        {
            int copHash = Function.Call<int>(Hash.GET_HASH_KEY, "COP");
            //MG_Main.TargetGroup = Function.Call<int>(Hash.CREATE_GROUP, copHash);
            //Function.Call(Hash.SET_PED_RELATIONSHIP_GROUP_HASH, _target, copHash);
            ////Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 3, MG_Main.TargetRelationsGroup, MG_Main.PlayerPed.RelationshipGroup);
            //Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, _target, copHash);
            //Function.Call(Hash.SET_PED_AS_GROUP_LEADER, _target, copHash);
            //Function.Call(Hash.SET_PED_CAN_BE_TARGETTED_BY_TEAM, _target, copHash, false);
            //Function.Call(Hash.SET_GROUP_FORMATION, copHash, 2);//31.01.2020
            //Function.Call(Hash.SET_GROUP_FORMATION_SPACING, copHash, 20f, 20f, 20f);//31.01.2020
            ////-------------Function.Call(Hash.SET_PED_AS_COP, _target, true);
            ///
            TargetType targetType = MG_Target.Type;
            if (targetType.Equals(TargetType.Police) || targetType.Equals(TargetType.Military) || (targetType.Equals(TargetType.Normal)))
            {
                Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, RelationsGroup, copHash);
                Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, copHash, RelationsGroup);

                Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, RelationsGroup, MG_WatchersGroup.RelationsGroup);
                Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, MG_WatchersGroup.RelationsGroup, RelationsGroup);
            }
            //else if (targetType.Equals(TargetType.Terrorist))
            //{
            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, RelationsGroup, copHash);
            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, copHash, RelationsGroup);

            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, RelationsGroup, MG_WatchersGroup.RelationsGroup);
            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, MG_WatchersGroup.RelationsGroup, RelationsGroup);
            //}
            //else if (targetType.Equals(TargetType.Normal))
            //{
            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 1, RelationsGroup, copHash);
            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 1, copHash, RelationsGroup);

            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 1, RelationsGroup, MG_WatchersGroup.RelationsGroup);
            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 1, MG_WatchersGroup.RelationsGroup, RelationsGroup);
            //}
            //else
            //{
            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, RelationsGroup, copHash);
            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, copHash, RelationsGroup);

            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, RelationsGroup, MG_WatchersGroup.RelationsGroup);
            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, MG_WatchersGroup.RelationsGroup, RelationsGroup);
            //}
            //0 = Companion
            //1 = Respect
            //2 = Like
            //3 = Neutral
            //4 = Dislike
            //5 = Hate
            //255 = Pedestrians
        }
        #endregion Private Methods
    }
}
