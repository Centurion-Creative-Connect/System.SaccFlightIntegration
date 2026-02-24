using CenturionCC.System.Player;
using DerpyNewbie.Common;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class CenturionSystemSaccExtension : UdonSharpBehaviour
{
    [SerializeField] [NewbieInject]
    private PlayerManagerBase playerManager;

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
}
