////////////////////////////////////////////////////////////////////////////////
//
//	MG_Controls.cs
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
using System.Windows.Forms;

namespace MG_Liquidator
{
    class MG_Controls : Script
    {
        #region Fields
        private static MG_Controls _instance;
        #endregion Fields

        #region Properties
        public static bool Enabled { get; set; } = true;
        public static Keys MainButtonToBeginMission { get; set; } = Keys.NumPad1;
        public static bool CellPhoneActionPressed { get; set; } = false;
        public static bool BlockButtonActionButton { get; set; } = false;
        #endregion Properties

        #region Constructor

        public MG_Controls()
        {
            _instance = this;
            //Tick += OnTick;

            //Function.Call(Hash.SET_RANDOM_EVENT_FLAG, true);
        }
        #endregion Constructor

        #region OnTick
        #endregion OnTick

        #region OnKeyDown

        private void OnKeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == MainButtonToBeginMission)
            {
                if (MG_Advisor.SkipTutorial == true)
                {
                    if (MG_Player.IsUsingCellphone == false)
                    {
                        if (BlockButtonActionButton == false)
                        {
                            //BlockButtonActionButton = true;
                            if (MG_iFruit.IsUsing == false)
                                if (CellPhoneActionPressed == false)
                                    CellPhoneActionPressed = true;


                        }
                    }
                }
                //MG_Message.SubTitle("Pressed! BlockButtonActionButton=" + BlockButtonActionButton);
            }
        }
        #endregion OnKeyDowns

        #region Public Methods
        public static void Init()
        {
            if (Enabled)
                _instance.KeyDown += _instance.OnKeyDown;
        }

        #endregion Public Methods

        #region Private Methods
        #endregion Private Methods
    }
}
