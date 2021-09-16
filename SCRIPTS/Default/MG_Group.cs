////////////////////////////////////////////////////////////////////////////////
//
//	MG_Group.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Native;

namespace MG_Liquidator
{
    public static class MG_Group
    {
        #region Public Methods

        public static void SetGroup(Ped ped, Ped leader, int relationshipGroup, int groupID)
        {
            //-----Function.Call(Hash.SET_PED_RELATIONSHIP_GROUP_HASH, ped, RelationshipID);

            if (leader == null)
            {
                UI.ShowHelpMessage("ERROR SetGroup _leader == null");
                return;
            }
            if (ped == null)
            {
                UI.ShowHelpMessage("ERROR SetGroup _ped == null");
                return;
            }
            //if (leader.CurrentPedGroup == null)
            //{
            //    // UI.ShowHelpMessage("ERROR _leader.CurrentPedGroup RESOLVING!!!!!!!!");
            //    Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped, groupID);
            //    Function.Call(Hash.SET_PED_AS_GROUP_LEADER, ped, groupID);
            //    return;
            //}
            while (leader.CurrentPedGroup == null)
            {
                UI.ShowHelpMessage("ERROR _leader.CurrentPedGroup RESOLVING!!!!!!!!");
                groupID = Function.Call<int>(Hash.CREATE_GROUP, relationshipGroup);
                Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, leader, groupID);
                Function.Call(Hash.SET_PED_AS_GROUP_LEADER, leader, groupID);
            }

            leader.CurrentPedGroup.Add(ped, false);
            Function.Call(Hash.SET_PED_CAN_BE_TARGETTED_BY_TEAM, ped, groupID, false);
            ped.RelationshipGroup = relationshipGroup;


        }
        #endregion Public Methods
    }
}
