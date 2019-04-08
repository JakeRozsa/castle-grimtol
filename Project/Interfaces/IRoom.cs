using System.Collections.Generic;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project.Interfaces
{
  public interface IRoom
  {
    string Name { get; set; }
    string Description { get; set; }
    string Description2 { get; set; }
    List<Item> Items { get; set; }
    Dictionary<string, IRoom> NearbyRooms { get; set; }
  }
}
