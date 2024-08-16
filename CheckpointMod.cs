using Photon.Pun;
using System.Drawing;
using System.Text;
using UnityEngine;

namespace BananaOS.Pages
{
    public class CheckpointPage : WatchPage
    {
        //What will be shown on the main menu if DisplayOnMainMenu is set to true
        public override string Title => "Checkpoint Monk";

        //Enabling will display your page on the main menu if you're nesting pages you should set this to false
        public override bool DisplayOnMainMenu => true;

        //This method will be ran after the watch is completely setup
        public override void OnPostModSetup()
        {
            //max selection index so the indicator stays on the screen
            selectionHandler.maxIndex = 1;
        }

        public static bool Checkpoint;
        static GameObject CheckpointObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //What you return is what is drawn to the watch screen the screen will be updated everytime you press a button
        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<color=yellow>==</color> CheckPoint Monk<color=yellow>==</color>");
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "Enable"));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(1, "Disable"));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(2, ""));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(3, "<color=yellow>Made By FireGiraffe</color>"));
            return stringBuilder.ToString();

        }

        void Update()
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Contains("MODDED_"))
            {
                if (Checkpoint == true)
                {
                    if (ControllerInputPoller.instance.rightGrab == true)
                    {
                        CheckPointArea();
                    }
                    if (ControllerInputPoller.instance.rightControllerPrimaryButton == true)
                    {
                        TeleportCheckPoint();
                    }
                }
            }
        }

        private static void CheckPointArea()
        {
            UnityEngine.Object.Destroy(CheckpointObject.GetComponent<Collider>());
            CheckpointObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            CheckpointObject.transform.localPosition = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
            CheckpointObject.GetComponent<Renderer>().material = new Material(Shader.Find("Sprites/Default"));
        }

        private static void TeleportCheckPoint()
        {
            UnityEngine.Object.Destroy(CheckpointObject.GetComponent<Collider>());
            GorillaLocomotion.Player.Instance.transform.position = CheckpointObject.transform.position;
        }
        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            switch (buttonType)
            {
                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;

                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;

                case WatchButtonType.Enter:
                    if (selectionHandler.currentIndex == 0)
                    {
                        Checkpoint = true;
                    }
                    if (selectionHandler.currentIndex == 1)
                    {
                        Checkpoint = false;
                    }

                    return;

                //It is recommended that you keep this unless you're nesting pages if so you should use the SwitchToPage method
                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;
            }
        }
    }
}