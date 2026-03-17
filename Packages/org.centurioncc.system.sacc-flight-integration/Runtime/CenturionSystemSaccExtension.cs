using CenturionCC.System.Player;
using CenturionCC.System.Utils;
using DerpyNewbie.Common;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class CenturionSystemSaccExtension : ObjectMarkerBase
{
    [SerializeField] [NewbieInject]
    private PlayerManagerBase playerManager;
    [SerializeField] [NewbieInject]
    private PlayerController playerController;

    private void RefreshPlayers()
    {
        var players = playerManager.GetPlayers();
        foreach (var player in players)
        {
            if (!player || player.VrcPlayer == null || !Utilities.IsValid(player.VrcPlayer))
            {
                continue;
            }

            var isInVehicle = player.VrcPlayer.GetPlayerTag("SF_InVehicle") == "T";
            player.SetCollidersActive(!isInVehicle);

            if (player.IsLocal)
            {
                if (isInVehicle)
                {
                    playerController.AddHoldingObject(this);
                }
                else
                {
                    playerController.RemoveHoldingObject(this);
                }
            }
        }
    }

    #region SaccFlightExtensionCallbacks
    // local pilot enter
    public void SFEXT_O_PilotEnter()
    {
        RefreshPlayers();
    }

    // global pilot enter
    public void SFEXT_G_PilotEnter()
    {
        RefreshPlayers();
    }

    // local pilot exit
    public void SFEXT_O_PilotExit()
    {
        RefreshPlayers();
    }

    // global pilot exit
    public void SFEXT_G_PilotExit()
    {
        RefreshPlayers();
    }

    // local passengers enter
    public void SFEXT_P_PassengerEnter()
    {
        RefreshPlayers();
    }

    // local passengers exit
    public void SFEXT_P_PassengerExit()
    {
        RefreshPlayers();
    }

    // global passengers enter
    public void SFEXT_G_PassengerEnter()
    {
        RefreshPlayers();
    }

    // global passengers exit
    public void SFEXT_G_PassengerExit()
    {
        RefreshPlayers();
    }
    #endregion

    public override ObjectType ObjectType => ObjectType.Prototype;
    public override float ObjectWeight => 0;
    public override float WalkingSpeedMultiplier => 1;
    public override string[] Tags => new[] { "NoFootstep" };
}
