namespace MultiPongCommon
{
    public enum MessageType : byte
    {
        Register = 1,
        RegisterConfirmation,
        RegesterRejection,
        GetState,
        State,
        UpdatePad,
        EndGame
    }
}