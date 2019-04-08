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
      Room StartRoom = new Room("Dark mud-walled room", "", @"
As you look around the area, you notice you are in a dark and dreary mud-walled cellar, deep underground.
'Look' - Next to you, some petrified wood wobbles while holding a flickering light encased in a rusted metal lantern with broken glass windows. To the West there is nothing but a large muddy wall with a small spring of water flowing from within. To the East there seems to be a hand dug hole at the base of the wall.. Where does that lead to? To the South, the stream has created a natural pool where the water is circulating down and out of the cellar. To the North is an old wooden door. Its 4 planks wide, with 1 plank running diagonally being barely held together by some old rusty nails.");
      Room Room2 = new Room("A dark room", @"
As you step in the room, your feet sink into slimy mud down to your ankles. Its quite dark in here...", @"Carrying your light, you not that the mud under your feet is mossy and gross. All the walls are carved out stone as if this used to be a mine maybe... 
It smells of fermented dead mice.
To the West, you see a passageway that has been blocked off by a door. The door has a large metal bar attached to the walls on each side and a  ");
      Room LockedRoom = new Room("Room 3", "", "", true);
      Room KeyRoom = new Room("Room 4", "As you enter the room, you trip up the step land face flat on the cold tile floor. You stand up but cant see anything in the dark room.", "You step up and into the room to realize that it has running water and electricity. On the table, sits what looks like it used to be a sandwich, but now its covered in nasty blue mold. Besides it is a key.");
      Room Room5 = new Room("Room 5", "ROOM 5 vague Description", "Room Desc2");
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
      //CREATE ITEMS HERE
      Item Sword = new Item("Sword", "A pointy Sword");
      Item Shield = new Item("Shield", "A wooden bulwark shield.");
      Item MoldyFood = new Item("Moldy Food", "It looks like it used to be a sandwich, but now its covered in nasty blue mold.");
      Item Clave = new Item("Key", "a copper key that has been petina'd blue green from the moisture.");
      Item Lantern = new Item("Lantern", "A flickering light stands encased in a rusted metal lantern with broken glass windows.");
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
      System.Console.WriteLine("Welcome to the Dungeon Crawler");
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
        return;
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
        CurrentRoom = (Room)CurrentRoom.NearbyRooms[option];
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