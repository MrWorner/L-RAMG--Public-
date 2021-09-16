////////////////////////////////////////////////////////////////////////////////
//
//	MG_Message.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_Liquidator
{
    public static class MG_Message
    {
        #region Public Methods

        public static void SubTitle(string text)
        {
            UI.ShowSubtitle(text);
        }

        public static void SubTitle(string text, int duration)
        {
            UI.ShowSubtitle(text, duration);
        }

        public static void SMS(string text)
        {
            UI.Notify(text);
        }

        public static void SMS(string text, int duration)
        {
            UI.Notify(text);
        }

        public static void HelpMessage(string text)
        {
            UI.ShowHelpMessage(text);
        }

        public static void HelpMessage(string text, int duration)
        {
            UI.ShowHelpMessage(text, duration);
        }
        #endregion Public Methods
    }
}
