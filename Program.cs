using System.Runtime.InteropServices;

class FightUnit
{
    protected string Name = "None";
    protected int AT = 10;
    protected int HP = 50;
    protected int MAXHP = 100;

    public bool IsDeath()
    {
        return HP <= 0;
    }

    public void StatusRender()
    {
        Console.Write(Name);
        Console.WriteLine("의 능력치 -----------------------------------------");
        Console.Write("공격력 : ");
        Console.WriteLine(AT);

        Console.Write("체력 : ");
        Console.Write(HP);
        Console.Write("/");
        Console.WriteLine(MAXHP);
        Console.WriteLine("-------------------------------------------------");

    }
    public void Damage(FightUnit _OtherUnit)
    {
        Console.Write(Name);
        Console.Write("가 ");
        Console.Write(_OtherUnit.AT);
        Console.WriteLine("의 데미지를 입었습니다.");
        Console.ReadKey();
        HP -= _OtherUnit.AT;
    }
}

class Player : FightUnit
{

    public void PrintHp()
    {
        Console.WriteLine("");
        Console.Write("현재 플레이어의 HP는 ");
        Console.Write(HP);
        Console.WriteLine("입니다.");
        Console.ReadKey();
    }
    public void MaxHeal()
    {
        if (HP >= MAXHP)
        {
            Console.WriteLine("");
            Console.WriteLine("체력이 모두 회복되어있어 회복할 필요가 없습니다.");
            Console.ReadKey();
        }
        else
        {
            HP = MAXHP;
            PrintHp();
        }
        return;
    }

    public Player()
    {
        Name = "플레이어";
    }

    
}
class Monster : FightUnit
{
    public Monster()
    {
        Name = "몬스터";
    }
}

enum STARTSELECT
{
    SELECTTOWN,
    SELECTBATTLE,
    NONSELECT,
}


internal class Program
{
    static STARTSELECT StartSelect()
    {


        Console.Clear();
        Console.WriteLine("1. 마을");
        Console.WriteLine("2. 배틀");
        Console.WriteLine("어디로 가시겠습니까?");

        ConsoleKeyInfo CKI = Console.ReadKey();

        Console.WriteLine("");

        switch (CKI.Key)
        {
            case ConsoleKey.D1:
                Console.WriteLine("마을로 이동합니다.");
                Console.ReadKey();
                return STARTSELECT.SELECTTOWN;
            case ConsoleKey.D2:
                Console.WriteLine("배틀을 시작합니다.");
                Console.ReadKey();
                return STARTSELECT.SELECTBATTLE;
            default:
                Console.WriteLine("잘못된 선택입니다.");
                Console.ReadKey();
                return STARTSELECT.NONSELECT;
        }

    }

    static STARTSELECT Town(Player _Player)
    {
        while (true)
        {
            Console.Clear();
            _Player.StatusRender();
            Console.WriteLine("마을에서 무슨 일을 하시겠습니까?");
            Console.WriteLine("1. 체력을 회복한다.");
            Console.WriteLine("2. 무기를 강화한다.");
            Console.WriteLine("3. 마을을 나간다.");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    _Player.MaxHeal();
                    break;
                case ConsoleKey.D2:
                    break;
                case ConsoleKey.D3:
                    return STARTSELECT.NONSELECT;
            }
        }
    }

    static STARTSELECT Battle(Player _Player)
    {
        Monster NewMonster = new Monster();

        while ( false == NewMonster.IsDeath() && false == _Player.IsDeath() )
        {

            Console.Clear();
            _Player.StatusRender();
            NewMonster.StatusRender();

            NewMonster.Damage(_Player);
            if(false == NewMonster.IsDeath() )
            {
                _Player.Damage(NewMonster);
            }
            Console.ReadKey();
        }

        Console.WriteLine("싸움의 결판이 났습니다.");

        if (true == NewMonster.IsDeath())
        {
            Console.WriteLine("플레이어의 승리입니다.");
        }
        else
        {
            Console.WriteLine("몬스터의 승리입니다.");
        }

        Console.ReadKey();
        return STARTSELECT.SELECTTOWN;
    }

    private static void Main(string[] args)
    {

        Player NewPlayer = new Player();

        STARTSELECT SelectCheck = STARTSELECT.NONSELECT;

        while (true)
        {
            
            

            switch (SelectCheck)
            {
                case STARTSELECT.NONSELECT:
                    SelectCheck = StartSelect();
                    break;
                case STARTSELECT.SELECTTOWN:
                    SelectCheck = Town(NewPlayer);
                    break;
                case STARTSELECT.SELECTBATTLE:
                    SelectCheck = Battle(NewPlayer);
                    break;
            }
        }

    }
}