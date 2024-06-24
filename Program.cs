
using System.ComponentModel.Design;

namespace MortalCombat
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            Warrior warrior1;
            Warrior warrior2;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Welcome to death tournament!");
            Console.Clear();
            Console.WriteLine("Select first character");
            warrior1 = ChooseWarrior();
            Console.ReadKey();
            Console.Clear();
            warrior2 = ChooseWarrior();
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine($"Character 1 is {warrior1.Name}, character 2 is {warrior2.Name}");
            Console.WriteLine("Let's start a fight");
            Console.ReadKey();
            Console.Clear();
            while (warrior1.Health > 0 || warrior2.Health > 0)
            {
                i++;
                warrior1.GetDamage(warrior2.Damage); warrior2.GetDamage(warrior1.Damage);
                Console.WriteLine($"{warrior1.Name} hit {warrior1.Damage} points." +
                    $" {warrior2.Name} hit {warrior2.Damage} points.");
                if (i == 1)
                {
                    warrior1.SpecialAbilityOfClass(); warrior2 .SpecialAbilityOfClass();
                    Console.WriteLine($"Special abilities of class are used");
                }
                if (warrior1.Health <= 0 && warrior2.Health <= 0)
                {
                    Console.WriteLine("Draw");
                    Console.WriteLine("End of game");
                    break;
                }
                else if (warrior1.Health > 0 && warrior2.Health <= 0)
                {
                    Console.WriteLine($"{warrior1.Name} wins");
                    Console.WriteLine("End of game");
                    break;
                }

                else if (warrior1.Health <= 0 && warrior2.Health > 0)
                {
                    Console.WriteLine($"{warrior2.Name} wins");
                    Console.WriteLine("End of game");
                    break;
                }
                warrior1.SpecialAbilityOfWarrior();warrior2.SpecialAbilityOfWarrior();
                Console.WriteLine("Warriors are using their unique abilities");
                Console.WriteLine($"{warrior1.Name}'s health = {warrior1.Health}, his next damage would be = {warrior1.Damage} " +
                    $"; {warrior2.Name}'s health = {warrior2.Health}, his next damage would be = {warrior2.Damage}");
                Console.ReadLine();
            }
        }
        static public Warrior ChooseWarrior()
        {
            Console.WriteLine("Select class of fighter");
            Console.Write("n for ninja o for outer world warrior: ");
            string input = Console.ReadLine();
            if (char.TryParse(input, out char selectedClass))
            {
                switch (selectedClass)
                {
                    case 'n':
                        Console.Write("Now choose: s for Subzero and S for scorpion: ");
                        string result = Console.ReadLine();
                        if (char.TryParse(result, out char selectedNinja))
                        {
                            switch (selectedNinja)
                            {
                                case 's':
                                    Console.WriteLine("You choose Subzero");
                                    return new Subzero("Subzero",90,13,true,5);
                                case 'S':
                                    Console.WriteLine("You choose Scorpion");
                                    return new Subzero("Scorpion", 90, 10, true, 10);
                                default:
                                    Console.WriteLine("We haven't got this type of ninja");
                                    break;
                            }
                        }
                        else
                            Console.WriteLine("S - for scorpion, s  - for subzero, what is wrong with you?!");
                        break;
                    case 'o':
                        Console.WriteLine("For outer world there is only Shao Kan");
                        Console.WriteLine("You choose Shao Kan");
                        return new ShaoKan("Shao Kan", 100, 10, 10);
                    case '?':
                        Console.WriteLine("How did you discover about him???");
                        Console.WriteLine("You choose the secret character - Ermak");
                        return new Ermak("Ermak",50,15,10);
                    default:
                        Console.WriteLine("We have not got this type of warrior");
                        break;
                }
            }
            else
                Console.WriteLine("Please? try again. Input n or o");
            return null;
        }
    }
    class Warrior
    {
        public string Name { get; protected set; }
        public int Health {get; protected set; }
        public int Damage { get; protected set; }
        public Warrior(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }
        public virtual int SpecialAbilityOfClass() { return 0; }
        public virtual int SpecialAbilityOfWarrior() { return 0; }
        public int GetDamage(int damage)
        {
            return Health -= damage;
        }
    }
    class Ninja: Warrior
    {
        public bool DodgeHit { get; private set; }
        public Ninja(string name, int health, int damage, bool dodge): base(name, health, damage)
        {
            DodgeHit = dodge;
        }
        public override int SpecialAbilityOfClass()
        {
            return Health += 100;   
        }
    }

    class OuterWorldWarrior: Warrior
    {
        public int Armor {get; private set;}
        public OuterWorldWarrior(string name,int health,int damage,int armor): base(name,health,damage)
        {
            Armor = armor;
        }
        public override int SpecialAbilityOfClass()
        {
            int a = Armor;
            Armor -= Armor;
            return Health += a; 
        }
    }

    class Subzero: Ninja
    {
        public int Ice { get; private set; }
        public Subzero(string name,int health,int damage,bool dodge, int ice): base(name, health, damage, dodge) { Ice = ice; }
        public override int SpecialAbilityOfWarrior()
        {
            return Health += Ice;
        }
    }

    class Scorpion:Ninja
    {
        public int Fire { get; private set; }
        public Scorpion(string name, int health, int damage, bool dodge, int fire) : base(name, health, damage, dodge) { Fire = fire; }
        public override int SpecialAbilityOfWarrior()
        {
            return Damage += Fire;
        }
    }

    class ShaoKan : OuterWorldWarrior
    {
        public ShaoKan(string name, int health, int damage,int armor):base(name,health,damage,armor) { }
        public override int SpecialAbilityOfWarrior()
        {
            return Health += 10;
        }
    }

    class Ermak: OuterWorldWarrior
    {
        public Ermak(string name, int health, int damage, int armor) : base(name, health, damage, armor) { }
        public override int SpecialAbilityOfWarrior()
        {
            return Damage = 1000;
        }
    }
}