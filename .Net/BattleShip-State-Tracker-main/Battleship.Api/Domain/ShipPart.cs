namespace Battleship.Api.Domain
{
    public class ShipPart
    {
        public int ShipPartId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Hit { get; set; }

        public Ship Ship { get; set; }
    }
}
