using System;
using System.Collections.Generic;
using System.Threading;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    public Room CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    public bool Running { get; private set; }

    private void Initialize()
    {
      //CREATE ROOMS HERE
      Room StartRoom = new Room("Dark mud-walled room", "Its quite dark in here...Where is your wand?", @"
As you look around the area, you notice you are in a dark and dreary mud-walled cellar, deep underground. 
If only your had your wand!
'Look' - Next to you, some petrified wood wobbles while holding a flickering light encased in a rusted metal lantern with broken glass windows. Could this be Argus Filch's Lantern?
To the West there is nothing but a large muddy wall with a small spring of water flowing from within. 
To the East there seems to be a hand dug hole at the base of the wall below a random painting on the wall.. Where does that lead to? 
To the South, the stream has created a natural pool where the water is circulating down and out of the cellar. 
To the North is an old wooden door. Its 4 planks wide, with 1 plank running diagonally being barely held together by some old rusty nails.");
      Room Room2 = new Room("A dark room", @"
As you step in the room, your feet sink into slimy mud down to your ankles. Its quite dark in here... 
Maybe that lantern would be useful in here.",
@"Carrying your lantern, you notice that the mud under your feet is mossy and gross. 
All the walls are carved out stone as if this used to be a mine maybe... 
It smells of fermented dead mice.
You think to yourself, 'why is there another random painting staring at me?'.
To the West, you see a passageway that has been blocked off by a door. The door has a large metal bar attached to the walls on each side and an enchanted lock. 
To the east, lays a hallway. 
You can feel the presence of dark magic nearby. ");
      Room LockedRoom = new Room("Monitor Room", "Its quite dark in here...", @"As you enter, you see a lot whole wall of frames on the North wall that are mirroring the rooms your had been in! There is a door betwixt them leading north.. Its creaking as it wobbles open. Mounted on the West wall is a shield and sword!
                              |`-._/\_.-`|
                              |    ||    |
    /                         |___o()o___|
O===[====================-    |__((<>))__|
    \                         \   o\/o   /
                               \   ||   /
                                \  ||  /
                                 '.||.'
                                   ``
                            
      ", true);
      Room KeyRoom = new Room("Tiled floor room.", "As you enter the room, you trip up the step land face flat on the cold tile floor. You stand up but cant see anything in the dark room. If only you had your wand!", @"You step up and into the room to realize that it has running water and a warm hearth. 
                    _.---._                                    
                _.-~       ~-._                                    
            _.-~               ~-._            8 8 8 8                      ,ooo.             
        _.-~                       ~---._      8a8 8a8                     oP   ?b
    _.-~                                 ~\    d888a888zzzzzzzzzzzzzzzzzzzz8     8b
 .-~                                    _.;    `''^'''                     ?o___oP'
 :-._                               _.-~ ./                    
 }-._~-._                   _..__.-~ _.-~)                    
 `-._~-._~-._              / .__..--~_.-~                       
     ~-._~-._\.        _.-~_/ _..--~~                           
         ~-. \`--...--~_.-~/~~                                    
            \.`--...--~_.-~                                    
              ~-..----~                                        
On the table, sits what looks like it used to be a sandwich, but now its covered in nasty blue mold. Besides it is a key in front of a portrait of the Ministry of Magic.");
      Room FinalRoom = new Room("Final Room", "You kick the door open violently. As the door swings open you dive and roll into the room and behnd a table that you push over for cover. You peak your head over only to quickly dodge a curse spit at you by a Death Eater!", "You kick the door open violently. As the door swings open you dive and roll into the room and behnd a table that you push over for cover. You peak your head over to dodge a curse spit at you by a Death Eater");
      Room Room6 = new Room("Room 6", "ROOM 6 vague Description", "Room Desc2");
      Room Room7 = new Room("Room 7", "ROOM 7 vague Description", "Room Desc2");
      Room Room8 = new Room("Room 8", "ROOM 8 vague Description", "Room Desc2");
      Room MuddyTunnel = new Room("Muddy Tunnel", @"You try to wiggle down into the hand dug tunnel but your get stuck and cant move!
.....", @"You try to wiggle down into the hand dug tunnel but your get stuck and cant move!.....");
      Room Pool = new Room("Natural Pool of Water", "You dive down into the spring where the water is circulating down and out of the cave in hopes that it will lead you out to a safety...", "You dive down into the spring where the water is circulating down and out of the cave in hopes that it will lead you out to a safety...");
      //ESTABLISH RELATIONSHIPS HERE
      StartRoom.AddNearbyRoom("north", Room2);
      StartRoom.AddNearbyRoom("east", MuddyTunnel);
      StartRoom.AddNearbyRoom("south", Pool);
      Room2.AddNearbyRoom("south", StartRoom);
      Room2.AddNearbyRoom("west", LockedRoom);
      Room2.AddNearbyRoom("east", KeyRoom);
      LockedRoom.AddNearbyRoom("east", Room2);
      KeyRoom.AddNearbyRoom("west", Room2);
      LockedRoom.AddNearbyRoom("north", FinalRoom);
      FinalRoom.AddNearbyRoom("south", LockedRoom);

      //CREATE ITEMS HERE
      Item Sword = new Item("Sword", "Could it be? It appears to be so! It's the Sword of Godric Griffindor!");
      Item Shield = new Item("Shield", "It seems to be quite a sustainable shield.. It appears to have the signature of the great and terrible Voldemort on it!");
      Item MoldyFood = new Item("Moldy-Food", "It looks like it used to be a sandwich, but now its covered in nasty blue mold.");
      Item Clave = new Item("Key", "a copper key that has been petina'd blue green from the moisture. Looks like, it had once been enchanted as to fly with butterfy wings.");
      Item Lantern = new Item("Lantern", "A flickering light stands encased in a rusted metal lantern with broken glass windows. Could this be Argus Filch's Lantern?");
      //ADD ITEMS TO ROOM
      LockedRoom.AddItem(Sword);
      LockedRoom.AddItem(Shield);
      KeyRoom.AddItem(MoldyFood);
      KeyRoom.AddItem(Clave);
      StartRoom.AddItem(Lantern);
      Running = true;
      CurrentRoom = StartRoom;
      CurrentPlayer = new Player();
    }
    internal void Run()
    {
      Initialize();
      Console.Clear();
      System.Console.WriteLine(@"Welcome to:
_________          _______    _______  _______           _______ 
\__   __/|\     /|(  ____ \  (  ____ \(  ___  )|\     /|(  ____ \
   ) (   | )   ( || (    \/  | (    \/| (   ) || )   ( || (    \/
   | |   | (___) || (__      | |      | (___) || |   | || (__    
   | |   |  ___  ||  __)     | |      |  ___  |( (   ) )|  __)   
   | |   | (   ) || (        | |      | (   ) | \ \_/ / | (      
   | |   | )   ( || (____/\  | (____/\| )   ( |  \   /  | (____/\
   )_(   |/     \|(_______/  (_______/|/     \|   \_/   (_______/
                                                        ");
      System.Console.WriteLine("Would you like to play? (y/n)");
      string GameStart = Console.ReadLine().ToLower();
      if (GameStart == "y")
      {
        Console.Clear();
        System.Console.WriteLine($@"
**eyes open and close bleakly**
As you wake from your deep slumber, you wonder where you are... You can't even remember your name. 
      ");
        System.Console.WriteLine("'What is my name?? Why can't I remember?'");
        string UserName = Console.ReadLine();
        Console.Clear();

        System.Console.WriteLine($"'Ahh, that's right.. My name is {UserName}.' {CurrentRoom.Description2}");
        System.Console.WriteLine("\nCommands:'go (north)', 'look (east)', 'take (item)', 'use (item)', 'bag', 'help', 'reset', 'quit'\n");
        StartGame();
      }
      else
      {
        System.Console.WriteLine("Alas, what a wimpy dingle blommer you are.. Farewell.");
        Run();
      }

    }
    public void StartGame()
    {
      while (Running)
      {
        System.Console.WriteLine("What would you like to do?");
        GetUserInput();
        Console.Clear();
        // Item key = CurrentPlayer.Inventory.Find(i => i.Name == "Key");
        // if (key != null)
        // {

        // }
        Item lantern = CurrentPlayer.Inventory.Find(i => i.Name == "Lantern");
        if (lantern != null)
        {
          System.Console.WriteLine($"current room: {CurrentRoom.Name}");
          System.Console.WriteLine(CurrentRoom.Description2);
        }
        else
        {
          System.Console.WriteLine($"current room: {CurrentRoom.Name}");
          System.Console.WriteLine(CurrentRoom.Description);
        }
        if (CurrentRoom.Name == "Muddy Tunnel")
        {
          System.Console.WriteLine("You starve to death.");
          System.Console.WriteLine("GAME OVER");
          Console.ReadLine();
          Run();
        }
        else if (CurrentRoom.Name == "Natural Pool of Water")
        {
          System.Console.WriteLine("You drown.");
          System.Console.WriteLine("GAME OVER");
          Console.ReadLine();
          Run();
        }
      }
    }
    public void GetUserInput()
    {
      string[] userChoice = Console.ReadLine().ToLower().Split(" ");
      string command = userChoice[0];
      string option = "";
      if (userChoice.Length > 1)
      {
        option = userChoice[1];
      }
      switch (command)
      {
        case "go":
          System.Console.WriteLine($"you go {option}");
          Go(option);
          break;
        case "look":
          Look();
          break;
        case "take":
          TakeItem(option);
          break;
        case "use":
          UseItem(option);
          break;
        case "bag":
          Inventory();
          break;
        // case "drink":
        //   Drink();
        //   break;
        case "help":
          System.Console.WriteLine("HELP ME");
          Help(option);
          break;
        case "quit":
          Console.Clear();
          System.Console.WriteLine("Good-Bye");
          Quit();
          break;
        case "reset":
          Console.Clear();
          Reset();
          break;
        default:
          System.Console.WriteLine("I can't do that!");
          break;
      }
    }

    public void Go(string option)
    {
      if (!CurrentRoom.NearbyRooms.ContainsKey(option))
      {
        System.Console.WriteLine("You cant do that!");
      }
      else if (CurrentRoom.NearbyRooms.ContainsKey(option))
      {
        var room = (Room)CurrentRoom.NearbyRooms[option];
        if (room.IsLocked)
        {
          System.Console.WriteLine("The door is locked.");
          Console.ReadLine();
        }
        else
        {
          CurrentRoom = (Room)CurrentRoom.NearbyRooms[option];
        }
      }
    }

    public void Help(string option)
    {
      System.Console.WriteLine($@"
here are some helpful helping helper helps....
NOT! you're in a cellar... BY. YOUR. SELF. no one is there to help you dummy.
      ");
      Thread.Sleep(5000);
      System.Console.WriteLine("GAME OVER");
      Run();
    }

    public void Inventory()
    {
      if (CurrentPlayer.Inventory.Count == 0)
      {
        Console.Clear();
        System.Console.WriteLine("There doesn't seem to be anything of use in here.\n");
        System.Console.WriteLine("What would you like to do?");
        GetUserInput();
      }
      else if (CurrentPlayer.Inventory.Count > 0)
      {
        Console.Clear();
        System.Console.WriteLine($"Current Inventory:\n");
        CurrentPlayer.Inventory.ForEach(item =>
        {
          System.Console.WriteLine($"{item.Name}: {item.Description}\n");
        });
        System.Console.WriteLine("What would you like to do?");
        GetUserInput();
      }
    }

    public void Look()
    {
      if (CurrentRoom.Items.Count == 0)
      {
        Console.Clear();
        System.Console.WriteLine("There doesn't seem to be anything of use in here.\n");
        System.Console.WriteLine("What would you like to do?");
        GetUserInput();
      }
      else if (CurrentRoom.Items.Count > 0)
      {
        Console.Clear();
        System.Console.WriteLine($"{CurrentRoom.Name}, {CurrentRoom.Description}.\nThere are items in here!\n");
        System.Console.WriteLine("Items:\n");
        CurrentRoom.Items.ForEach(item =>
        {
          System.Console.WriteLine($"{item.Name}: {item.Description}\n");
        });
        System.Console.WriteLine("What would you like to do?");
        GetUserInput();
      }

    }


    public void TakeItem(string option)
    {
      Item itemToTake = CurrentRoom.Items.Find(i => i.Name.ToLower() == option);
      if (itemToTake != null)
      {
        CurrentPlayer.Inventory.Add(itemToTake);
        System.Console.WriteLine($"Successfully picked up {option}");
        CurrentRoom.Items.Remove(itemToTake);
      }
    }

    public void UseItem(string option)
    {
      Item InventoryItem = CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == option);
      Console.Clear();
      if (InventoryItem != null)
      {
        if (CurrentRoom.Name == "Final Room")
        {
          Random rnd = new Random();
          int roll = rnd.Next(1, 100);
          if (InventoryItem.Name == "Sword")
          {
            if (roll >= 50)
            {
              System.Console.WriteLine(@"
You leap over the table and charge at the death eater with the Sword of Griffindor!
    /                     
O===[====================-   
    \               
 As you raise the nimble sword over your head to slash down upon his head, he yells 'Avada Kedavra!'... The spell comes out of his wand and hits you straight in the chest... The sword loses momentum and falls aside the death eater. You collapse and the light leaves your eyes and your soul leaves your body... slowly elevating out from your lips..
You lose.");
              Console.ReadLine();
              Run();
            }
            else
            {
              System.Console.WriteLine(@"
You leap over the table and charge at the death eater with the Sword of Griffindor!
    /                     
O===[====================-   
    \               
 As you raise the nimble sword over your head to slash down upon his head, he yells 'Avada Kedavra!'... The spell comes out of his wand and hits your sword straight on and absorbs the spell! You continue and heave down! The sword cracks his head open. He falls to the floor mumbling as the blood pools beneath him.");
              EndGame();
            }
          }
          else if (InventoryItem.Name == "Shield")
          {
            if (roll >= 40)
            {
              System.Console.WriteLine(@"
You leap over the table and charge at the death eater with the shield! 
 |`-._/\_.-`|
 |    ||    |
 |___o()o___|
 |__((<>))__|
 \   o\/o   /
  \   ||   /
   \  ||  /
    '.||.'
      ``
The death eater violently spews 'Avada Kedavra'. You pull the shield up to block the spell but you are not quick enough and hits you straight in the chest... You take one more step and you crumble over yourself as you collapse on the ground. The light slowly leaves your eyes and your soul leaves your body... slowly elevating out from your lips..
You lose.
");
              Console.ReadLine();
              Run();
            }
            else
            {
              System.Console.WriteLine(@"
You leap over the table and charge at the death eater with the shield! 
 |`-._/\_.-`|
 |    ||    |
 |___o()o___|
 |__((<>))__|
 \   o\/o   /
  \   ||   /
   \  ||  /
    '.||.'
      ``
The death eater violently spews 'Avada Kedavra'. You pull the shield up to block the spell and are barely quick enough for the spell to hit the rim of the shield! To your surprise, the metal brim was enchanted and the spell bounces back at the Death Eater which hits him right in the face, instantly knocking him off his feet and onto his back. His eye glitch then his body relaxes as his eyes stare blankly into space. He dies!
              ");
              EndGame();
            }
          }
          else if (InventoryItem.Name == "Moldy-Food")
          {
            System.Console.WriteLine(@"You decide that your best chance is to distract the Death Eater so you hurl the sandwich from where your sitting. 
                    _.---._
                _.-~       ~-._
            _.-~               ~-._
        _.-~                       ~---._
    _.-~                                 ~\
 .-~                                    _.;
 :-._                               _.-~ ./
 }-._~-._                   _..__.-~ _.-~)
 `-._~-._~-._              / .__..--~_.-~
     ~-._~-._\.        _.-~_/ _..--~~
         ~-. \`--...--~_.-~/~~
            \.`--...--~_.-~
              ~-..----~
To your surprise it hits him right in the face upon which he inhales part of the sandwich! He begins to cough and choke on the blue moldy sandwich and collapses to his death!");
            EndGame();
          }
        }
        if (CurrentRoom.Name == "A dark room" && InventoryItem.Name == "Key")
        {
          var LockedRoom = (Room)CurrentRoom.NearbyRooms["west"];
          LockedRoom.IsLocked = false;
        }
        CurrentPlayer.Inventory.Remove(InventoryItem);
        System.Console.WriteLine($"You used {option}!");
      }
      else
      {
        System.Console.WriteLine("You can't do that!");
      }
      System.Console.WriteLine("What would you like to do?");
      GetUserInput();
    }

    public void EndGame()
    {
      System.Console.WriteLine($"You've done it!!");
      Console.ReadLine();
      Console.Clear();
      System.Console.WriteLine(@"
You walk up to your captor, the Death Eater, and pull his mask off only to realize you have no idea who he is. You take his wand and look around and notice a spiral staircase that goes straight up to a door way. From under the door, you see sunlight!
                    []
                    []
                    []
                    []
   _______________  []         _________________
   _______________) []        (_______________
    !     !     !   []        '  !     !     !
    !     !     !   []       ,!  !     !     !
    !     !     !   []      ! !  !     !     !
    !_____!_____!___[]_____'!_!__!_____!_____!_____
                    []__,_!_!_!
                    []_!__!_!|
                   ,[]_!__!_!
                 ,! []_!__!|
               ,! ! []_!__!
              ! ! ! []_!|
             !! ! !|[]_|
             !!!._|_[]
             !!!|!_.[]
             !|!_!__[]!.
             !_!_!__[]! !.
             !_!_!__[]! ! `.
              |!_!__|]! ! ! `.
               |_!__|]! ! ! ! `.
                 |____|_! ! ! !  `
                   |____|_! ! ! !
                    []____|_! ! !
                    []______|_! !
                    []________|_!
  __________________[]__________|____________________
You open the door and realize where you are! You enter the room, sit down on the couch which smells of roses and cats. Around the corner, sits an old lady sipping on a cup of steaming tea. She notices you mid sip and jolts, spilling the tea all over the place! 'How dare you enter unannounced!' She begins to draw her wand and casting the death curse! Luckily you are faster and yell 'Stupefy!'. She stiffens up and lands flat on her face.. You run out relieved you made it out alive! Next stop, the Ministry of Magic...
  ");
      Console.ReadLine();
      Run();
    }
    public void Quit()
    {
      Environment.Exit(0);
    }

    public void Reset()
    {
      Run();
    }

    public void Setup()
    {
      throw new System.NotImplementedException();
    }
  }
}