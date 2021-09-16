////////////////////////////////////////////////////////////////////////////////
//
//	MG_WatchersGroup.cs
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
    public static class MG_WatchersGroup
    {
        #region Properties      
        public static int RelationsGroup { get; private set; }
        public static PedGroup PedGroup { get; private set; }
        public static Ped Leader { get; set; }
        public static int GroupID { get; private set; }
        #endregion Properties

        private static bool _isRelationGroupGenerated = false;

        #region Public Methods

        public static void InitGroup(Ped ped)
        {
            if (_isRelationGroupGenerated == false)
            {
                _isRelationGroupGenerated = true;

                string targetGroupName = "GHOST_TEAM";
                RelationsGroup = World.AddRelationshipGroup(targetGroupName);
            }

            Leader = ped;

            while (Leader.CurrentPedGroup == null)
            {
                SetGroupLeader(Leader);
                //Wait(1);
            }
            //SetRelationsWithPlayer(Leader);
            SetFormation(Leader);
            SetPoliceRelations();
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

        private static void SetPoliceRelations()
        {
            int copHash = Function.Call<int>(Hash.GET_HASH_KEY, "COP");
            TargetType targetType = MG_Target.Type;

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, RelationsGroup, copHash);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, copHash, RelationsGroup);

            //if (targetType.Equals(TargetType.Police) || targetType.Equals(TargetType.Military))
            //{
            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, RelationsGroup, MG_TargetGroup.RelationsGroup);
            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, MG_TargetGroup.RelationsGroup, RelationsGroup);
            //}
            //else if (targetType.Equals(TargetType.Terrorist))
            //{
            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, RelationsGroup, MG_TargetGroup.RelationsGroup);
            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, MG_TargetGroup.RelationsGroup, RelationsGroup);
            //}
            //else if (targetType.Equals(TargetType.Normal))
            //{
            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 1, RelationsGroup, MG_TargetGroup.RelationsGroup);
            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 1, MG_TargetGroup.RelationsGroup, RelationsGroup);
            //}
            //else
            //{
            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 4, RelationsGroup, MG_TargetGroup.RelationsGroup);
            //    Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 4, MG_TargetGroup.RelationsGroup, RelationsGroup);
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
