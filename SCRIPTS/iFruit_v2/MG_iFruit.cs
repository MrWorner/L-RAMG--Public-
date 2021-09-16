using GTA;
using iFruitAddon2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_Liquidator
{
    class MG_iFruit : Script
    {
        private static MG_iFruit _instance;
        public static CustomiFruit IFruit { get; private set; }
        public static string ContactName { get; set; } = "Bane";
        public static bool Enabled { get; set; } = true;
        public static bool IsUsing { get; private set; } = false;
        public static iFruitContact Bane { get; private set; }

        public MG_iFruit()
        {
            _instance = this;
        }

        public static void Create_iFruitContact()
        {
            if (Enabled == false)
            {
                return;
            }
            // Custom phone creation
            IFruit = new CustomiFruit();

            // Phone customization (totally optional)
            /*
            _iFruit.CenterButtonColor = System.Drawing.Color.Orange;
            _iFruit.LeftButtonColor = System.Drawing.Color.LimeGreen;
            _iFruit.RightButtonColor = System.Drawing.Color.Purple;
            _iFruit.CenterButtonIcon = SoftKeyIcon.Fire;
            _iFruit.LeftButtonIcon = SoftKeyIcon.Police;
            _iFruit.RightButtonIcon = SoftKeyIcon.Website;
            */

            // New contact (wait 4 seconds (4000ms) before picking up the phone)
            Bane = new iFruitContact(ContactName);
            Bane.Answered += ContactAnswered;   // Linking the Answered event with our function
            Bane.DialTimeout = 1000;            // Delay before answering
            Bane.Active = true;                 // true = the contact is available and will answer the phone
            Bane.Icon = ContactIcon.Skull;      // Contact's icon

            iFruitContactCollection contactsList = IFruit.Contacts;
            contactsList.Clear();
            contactsList.Add(Bane); 
                                        

            _instance.Tick += _instance.OnTick;
        }

        // Tick Event
        private void OnTick(object sender, EventArgs e)
        {
            IFruit.Update();
        }

        private static void ContactAnswered(iFruitContact contact)
        {
            IsUsing = true;

            if (MG_Player.IsUsingCellphone == false)
            {

                if (MG_Hitman.MadeMad)
                {
                    MG_AssassinationMission.NotAvailableAnymore();
                }
                else
                {

                    if (MG_AssassinationMission.IsJobActive)
                    {
                        if (MG_Settings.INI_isCanBeCancelled)
                        {
                            MG_Statistic.SaveHistory(MissionStatus.CANCELLED);
                            MG_AssassinationMission.CancelJob();
                        }
                    }
                    else
                    {
                        MG_AssassinationMission.StartJob();
                    }
                }
            }
            //UI.Notify("The contact has answered.");

            IFruit.Close();
            IsUsing = false;
        }
    }
}