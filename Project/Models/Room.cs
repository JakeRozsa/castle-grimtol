using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public string Description2 { get; set; }
    public bool IsLocked { get; set; }

    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> NearbyRooms { get; set; }

    public Room(string name, string desc, string desc2, bool isLocked = false)
    {
      NearbyRooms = new Dictionary<string, IRoom>();
      Name = name;
      Description = desc;
      Description2 = desc2;
      Items = new List<Item>();
      IsLocked = isLocked;
    }
    public void AddNearbyRoom(string option, IRoom room)
    {
      NearbyRooms.Add(option, room);
    }
    public IRoom TravelToRoom(string dir)
    {
      if (NearbyRooms.ContainsKey(dir))
      {
        return NearbyRooms[dir];
      }
      System.Console.WriteLine("I can't do that!");
      return (IRoom)this;
    }
    public void AddItem(Item item)
    {
      Items.Add(item);
    }
  }
}