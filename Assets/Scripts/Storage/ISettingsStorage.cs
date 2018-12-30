namespace Storage
{
    public interface ISettingsStorage
    {
        UnitSettings[] Units { get; }
        LevelSettings[] Levels { get; }

        UnitSettings GetUnitById(int id);
    }
}