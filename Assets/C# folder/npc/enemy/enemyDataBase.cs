

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
    public short Level { get; set; }
    
    protected int maxHP;
    protected int aTK;
    protected int dEF;
    protected float cOIN;

    public int HP { get; set; }
    public int CD = 1;

    public int NumberOfActions = 1;

    #region abSetting
    public int MaxHP {
        get {
            switch (Level)
            {
                case 1:
                    maxHP = 3;
                    break;
                case 2:
                    maxHP = 6;
                    break;
                case 3:
                    maxHP = 12;
                    break;
                case 4:
                    maxHP = 18;
                    break;
                default:
                    maxHP = 0;
                    break;
            }
            return maxHP;
        }
    }
    public int ATK {
        get {
            switch (Level)
            {
                case 1:
                    aTK = 1;
                    break;
                case 2:
                    aTK = 2;
                    break;
                case 3:
                    aTK = 4;
                    break;
                case 4:
                    aTK = 6;
                    break;
                default:
                    aTK = 0;
                    break;
            }
            return aTK;
        }
    }
    public int DEF {
        get {
            switch (Level)
            {
                case 1:
                    dEF = 0;
                    break;
                case 2:
                    dEF = 0;
                    break;
                case 3:
                    dEF = 0;
                    break;
                case 4:
                    dEF = 0;
                    break;
                default:
                    dEF = 0;
                    break;
            }
            return dEF;
        }
    }
    public float COIN {
        get {
            switch (Level)
            {
                case 1:
                    cOIN = 11;
                    break;
                case 2:
                    cOIN = 56;
                    break;
                case 3:
                    cOIN = 304;
                    break;
                case 4:
                    cOIN = 936;
                    break;
                default:
                    cOIN = 0;
                    break;
            }
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
