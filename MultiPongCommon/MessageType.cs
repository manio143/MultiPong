namespace MultiPongCommon
{
    public enum MessageType : byte
    {
        Register = 1,
        RegisterConfirmation,
        RegisterRejection,
        GetState,
        State,
        UpdatePad,
        EndGame
    }
}