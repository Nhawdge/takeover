namespace Takeover.Enums
{
    public enum SecurityType
    {
        None,
        MutliAttack,
        // Needs two sources of attack (e.g. player and another tower)
        SpecialAttackRequired,
        // Needs certain type of attack (prior tower)
        Protected,
        // Towers can have certain range to protect each other
        LockedBy,
        // Towers control certain things, doors? Maybe? (KEEP THIS SIMPLE)
        // Towers need to be shut off in certain order?
        TimeLock,
        // Towers need to be shut off within a certain time of each other
        SpecialSignal,
        // Towers can have different signals for send/receive

    }
}