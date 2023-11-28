using Unity.Netcode.Components;

namespace Unity.Multiplayer.Samples.Utilities.ClientAuthority
{
    public class ClientNetworkAnimator : NetworkAnimator
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}