

/*
public enum enemyType
{
    normal,
    tank,
    patrol,
    masksman
};
*/

public class enemyDataBase : GeneralMovementSystem
{
    //public enemyType type;
    public int UID = 0;
    public short Level = 0;
    
    protected int maxHP;
    protected int aTK;
    protected int dEF;
    protected float cOIN;

    public int HP = 0;
    public int CD = 1;

    public int NumberOfActions = 1;

    #region abSetting
    public int MaxHP {
        get {
            maxHP = playerDataBase.Static.MonsterLevelSettingArray[Level].maxhp;
            maxHP = (maxHP * (1 + playerDataBase.Static.currentFloor / 5) ) + playerDataBase.Static.currentFloor;
            return maxHP;
        }
    }
    public int ATK {
        get {
            aTK = playerDataBase.Static.MonsterLevelSettingArray[Level].atk;
            aTK =(aTK * (1 + playerDataBase.Static.currentFloor / 10) );
            return aTK;
        }
    }
    public int DEF {
        get {
            dEF = playerDataBase.Static.MonsterLevelSettingArray[Level].def;
            dEF = (dEF * (1 + playerDataBase.Static.currentFloor / 30));
            return dEF;
        }
    }
    public float COIN {
        get {
            cOIN = playerDataBase.Static.MonsterLevelSettingArray[Level].coin;
            cOIN =( cOIN* (1 + playerDataBase.Static.currentFloor / 5) );
            return cOIN;
        }
        set {
            cOIN = value;
        }
    }
#endregion

    /*
     *Level = 1;
        MaxHP = 1 * (1 + curLevel / 5) + Level;
        HP = MaxHP;
        ATK = 1 * (1 + curLevel / 10);
        CD = 2;
        DEF = 0 * (1 + curLevel / 30);
        COIN = 2 * (1 + curLevel / 5);
     */
}
