namespace MultiPongCommon
{
    public enum MessageType : byte
    {
        Register = 1,
        RegisterConfirmation,
        GetState,
        State,
        UpdatePad
    }
}