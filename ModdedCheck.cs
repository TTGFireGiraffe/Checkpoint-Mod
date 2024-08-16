using Photon.Pun;

public class Moddedcheck : MonoBehaviourPunCallbacks
{
    public static Moddedcheck modcheck;
    public object gameMode;
    public void Start()
    {
        modcheck = this;
    }
    public bool IsModded()
    {
        if (!PhotonNetwork.InRoom)
            return false;
        return gameMode.ToString().Contains("MODDED");
    }

    public override void OnJoinedRoom() => PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("gameMode", out gameMode);
}
