using Xunit;
using MarsRover.Manager;
using System;

namespace MarsRover.UnitTests
{
    public class MarsRoverTests
    {
        private readonly Rover marsRover;
        
        //Make a test rover
        public MarsRoverTests()
        {
            bool[,] testGrid = new bool[2, 2];
            Direction heading = (Direction)0;
            marsRover = new Rover(0, 0, heading, testGrid);
        }
        
        //Test the X position
        [Fact]
        public void TestXPosition()
        {
            var result = marsRover.XPosition;
            
            Assert.True((result == 0), "The X position of the rover should be 0");
        }
        
        //Test the Y position
        [Fact]
        public void TestYPosition()
        {
            var result = marsRover.YPosition;
            
            Assert.True((result == 0), "The Y position of the rover should be 0");
        }
        
        //Test the heading
        [Fact]
        public void TestHeading()
        {
            var result = marsRover.Heading;
            
            Assert.True((result == (Direction)0), "The heading of the rover should be N");
        }
        
        //Test the grid
        [Fact]
        public void TestGrid()
        {
            var result = marsRover.Grid[marsRover.XPosition, marsRover.YPosition];
            
            Assert.True(result, "The rover should indicate its position on the grid");
        }
        
        //Test making a rover with an invalid x position
        [Fact]
        public void TestRoverInvalidX()
        {
            Rover invalidRover;
            
            var result = Assert.Throws<ArgumentException>(() => (invalidRover = new Rover(-1, 0, (Direction)0, new bool[1, 1])));
            
            Assert.True(result.Message == "The X location provided is not valid on the plateau", "Incorrect message for invalid x coordinate");
        }
        
        //Test making a rover with an invalid y position
        [Fact]
        public void TestRoverInvalidY()
        {
            Rover invalidRover;
            
            var result = Assert.Throws<ArgumentException>(() => (invalidRover = new Rover(0, -1, (Direction)0, new bool[1, 1])));
            
            Assert.True(result.Message == "The X location provided is not valid on the plateau", "Incorrect message for invalid y coordinate");
        }
        
        //Test making a rover with an invalid grid
        [Fact]
        public void TestRoverInvalidGrid()
        {
            Rover invalidRover;
            
            var result = Assert.Throws<ArgumentException>(() => (invalidRover = new Rover(0, 0, (Direction)0, new bool[0, 0])));
            
            Assert.True(result.Message == "The plateau is not large enough to hold a rover");
        }
        
        //Test making a rover at a position that is already occupied
        [Fact]
        public void TestRoverPositionOccupied()
        {
            Rover testRover;
            
            var result = Assert.Throws<ArgumentException>(() => (testRover = new Rover(0, 0, (Direction)0, marsRover.Grid)));
            
            Assert.True(result.Message == "There is already a rover on the plateau at that location", "Incorrect message for creation at occupied position");
        }
        
        //Test setting X
        [Fact]
        public void TestSettingXToValid()
        {
            var oldX = marsRover.XPosition;
            
            marsRover.XPosition = 1;
            
            var result = marsRover.XPosition;
            
            Assert.True(result == 1, "The rover's X position was not updated");
            
            var result2 = marsRover.Grid[marsRover.XPosition, marsRover.YPosition];
            
            Assert.True(result2, "The rover did not indicate the new Y position on the grid");
            
            var result3 = marsRover.Grid[oldX, marsRover.YPosition];
            
            Assert.False(result3, "The rover did not indicate that it was no longer at the old x position on the grid");
        }
        
        //Test setting Y
        [Fact]
        public void TestSettingYToValid()
        {
            var oldY = marsRover.YPosition;
            
            marsRover.YPosition = 1;
            
            var result1 = marsRover.YPosition;
            
            Assert.True(result1 == 1, "The rover's Y position was not updated");
            
            var result2 = marsRover.Grid[marsRover.XPosition, marsRover.YPosition];
            
            Assert.True(result2, "The rover did not indicate the new Y position on the grid");
            
            var result3 = marsRover.Grid[marsRover.XPosition, oldY];
            
            Assert.False(result3, "The rover did not indicate that it was no longer at the old y position on the grid");
        }
        
        //Test setting the heading
        [Fact]
        public void TestSettingHeading()
        {            
            marsRover.Heading = (Direction)2;
            
            var result = marsRover.Heading;
            
            Assert.True(result == (Direction)2, "The rover's heading was not updated");
        }
        
        //Test setting the grid
        [Fact]
        public void TestSettingGridToValid()
        {
            marsRover.Grid = new bool[3, 3];
            
            var result1 = marsRover.Grid.GetLength(0);
            
            var result2 = marsRover.Grid.GetLength(1);
            
            var result3 = marsRover.Grid[marsRover.XPosition, marsRover.YPosition];
            
            Assert.True((result1 == 3) && (result1 == 3), "The grid was not updated");
            
            Assert.True(result3, "The rover did not update the new grid with the current position");
        }
        
        //Test setting X to an invalid number
        [Fact]
        public void TestSettingInvalidX()
        {
            var result = Assert.Throws<ArgumentException>(() => (marsRover.XPosition = -1));
            
            Assert.True(result.Message == "The new X coordinate is not a valid coordinate on the plateau", "Incorrect message for setting invalid X coordinate");
        }
        
        //Test setting Y to an invalid number
        [Fact]
        public void TestSettingInvalidY()
        {
            var result = Assert.Throws<ArgumentException>(() => (marsRover.YPosition = -1));
            
            Assert.True(result.Message == "The new Y coordinate is not a valid coordinate on the plateau", "Incorrect message for setting invalid Y coordinate");
        }
        
        //Test setting X to out of bounds
        [Fact]
        public void TestSettingOOBX()
        {
            var result = Assert.Throws<ArgumentException>(() => (marsRover.XPosition = 2));
            
            Assert.True(result.Message == "The new X coordinate is not a valid coordinate on the plateau", "Incorrect message for setting out of bounds X coordinate");
        }
        
        //Test setting Y to out of bounds
        [Fact]
        public void TestSettingOOBY()
        {
            var result = Assert.Throws<ArgumentException>(() => (marsRover.YPosition = 2));
            
            Assert.True(result.Message == "The new Y coordinate is not a valid coordinate on the plateau", "Incorrect message for setting out of bounds Y coordinate");
        }
        
        //Test setting X to the location of another rover
        [Fact]
        public void TestSettingOccupiedX()
        {
            Rover roverBlock = new Rover(1, 0, (Direction)0, marsRover.Grid);
            
            var result = Assert.Throws<ArgumentException>(() => (marsRover.XPosition = 1));
            
            Assert.True(result.Message == "There is already a rover on the plateau at the new X coordinate and the current Y coordinate", "Incorrect message for setting occupied X coordinate");
        }
        
        //Test setting Y to the location of another rover
        [Fact]
        public void TestSettingOccupiedY()
        {
            Rover roverBlock = new Rover(0, 1, (Direction)0, marsRover.Grid);
            
            var result = Assert.Throws<ArgumentException>(() => (marsRover.YPosition = 1));
            
            Assert.True(result.Message == "There is already a rover on the plateau at the current X coordinate and the new Y coordinate", "Incorrect message for setting occupied Y coordinate");
        }
        
        //Test setting the grid to an array where the rover's position is already occupied
        [Fact]
        public void TestSettingInvalidGrid()
        {
            Rover roverBlock = new Rover(0, 0, (Direction)0, new bool[2, 2]);
            
            var result = Assert.Throws<ArgumentException>(() => (marsRover.Grid = roverBlock.Grid));
            
            Assert.True(result.Message == "There is already a rover on the new grid at the rover's current location", "Incorrect message for setting new grid with current location occupied");
        }
        
        //Test setting the grid to an array where the rover's position is out of bounds
        [Fact]
        public void TestSettingOOBGrid()
        {
            marsRover.XPosition = 1;
            marsRover.YPosition = 1;
            
            var result = Assert.Throws<ArgumentException>(() => (marsRover.Grid = new bool[1, 1]));
            
            Assert.True(result.Message == "The new grid is too small to relocate the rover to the same position on the new grid as it is on the current one", "Incorrect message for setting small grid");
        }
        
        //Test giving no instructions
        [Fact]
        public void TestNoInstructions()
        {
            marsRover.Navigate("");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Heading;
            
            Assert.True(result1 == 0, "The rover changed its X position");
            
            Assert.True(result2 == 0, "The rover changed its Y position");
            
            Assert.True(result3 == (Direction)0, "The rover changed its heading");
        }
        
        //Test invalid instructions
        [Fact]
        public void TestInvalidInstructions()
        {
            marsRover.Navigate("abcABC123!@#{}|;':,./<>?%^&*()_+-=[]");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Heading;
            
            Assert.True(result1 == 0, "The rover changed its X position");
            
            Assert.True(result2 == 0, "The rover changed its Y position");
            
            Assert.True(result3 == (Direction)0, "The rover changed its heading");
        }
        
        //Test turning left from north
        [Fact]
        public void TestTurnLFromN()
        {
            marsRover.Navigate("L");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Heading;
            
            Assert.True(result1 == 0, "The rover changed its X position");
            
            Assert.True(result2 == 0, "The rover changed its Y position");
            
            Assert.True(result3 == (Direction)3, "The rover is not facing west");
        }
        
        //Test turning left from east
        [Fact]
        public void TestTurnLFromE()
        {
            marsRover.Heading = (Direction)1;
            
            marsRover.Navigate("L");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Heading;
            
            Assert.True(result1 == 0, "The rover changed its X position");
            
            Assert.True(result2 == 0, "The rover changed its Y position");
            
            Assert.True(result3 == (Direction)0, "The rover is not facing north");
        }
        
        //Test turning left from south
        [Fact]
        public void TestTurnLFromS()
        {
            marsRover.Heading = (Direction)2;
            
            marsRover.Navigate("L");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Heading;
            
            Assert.True(result1 == 0, "The rover changed its X position");
            
            Assert.True(result2 == 0, "The rover changed its Y position");
            
            Assert.True(result3 == (Direction)1, "The rover is not facing east");
        }
        
        //Test turning left from west
        [Fact]
        public void TestTurnLFromW()
        {
            marsRover.Heading = (Direction)3;
            
            marsRover.Navigate("L");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Heading;
            
            Assert.True(result1 == 0, "The rover changed its X position");
            
            Assert.True(result2 == 0, "The rover changed its Y position");
            
            Assert.True(result3 == (Direction)2, "The rover is not facing south");
        }
        
        //Test turning right from north
        [Fact]
        public void TestTurnRFromN()
        {
            marsRover.Heading = (Direction)0;
            
            marsRover.Navigate("R");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Heading;
            
            Assert.True(result1 == 0, "The rover changed its X position");
            
            Assert.True(result2 == 0, "The rover changed its Y position");
            
            Assert.True(result3 == (Direction)1, "The rover is not facing east");
        }
        
        //Test turning right from east
        [Fact]
        public void TestTurnRFromE()
        {
            marsRover.Heading = (Direction)1;
            
            marsRover.Navigate("R");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Heading;
            
            Assert.True(result1 == 0, "The rover changed its X position");
            
            Assert.True(result2 == 0, "The rover changed its Y position");
            
            Assert.True(result3 == (Direction)2, "The rover is not facing south");
        }
        
        //Test turning right from south
        [Fact]
        public void TestTurnRFromS()
        {
            marsRover.Heading = (Direction)2;
            
            marsRover.Navigate("R");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Heading;
            
            Assert.True(result1 == 0, "The rover changed its X position");
            
            Assert.True(result2 == 0, "The rover changed its Y position");
            
            Assert.True(result3 == (Direction)3, "The rover is not facing west");
        }
        
        //Test turning right from west
        [Fact]
        public void TestTurnRFromW()
        {
            marsRover.Heading = (Direction)3;
            
            marsRover.Navigate("R");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Heading;
            
            Assert.True(result1 == 0, "The rover changed its X position");
            
            Assert.True(result2 == 0, "The rover changed its Y position");
            
            Assert.True(result3 == (Direction)0, "The rover is not facing north");
        }
        
        //Test moving north to a valid position
        [Fact]
        public void TestMoveN()
        {
            marsRover.Navigate("M");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Heading;
            
            var result4 = marsRover.Grid[0, 1];
            
            var result5 = marsRover.Grid[0, 0];
            
            Assert.True(result1 == 0, "The rover changed its X position");
            
            Assert.False(result2 == 0, "The rover did not change its Y position");
            
            Assert.True(result2 == 1, "The rover did not change its Y position correctly");
            
            Assert.True(result3 == (Direction)0, "The rover changed its heading");
            
            Assert.True(result4, "The rover did not update the grid with the new position");
            
            Assert.False(result5, "The rover did not update the old position on the grid");
        }
        
        //Test moving east to a valid position
        [Fact]
        public void TestMoveE()
        {
            marsRover.Heading = (Direction)1;
            
            marsRover.Navigate("M");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Heading;
            
            var result4 = marsRover.Grid[1, 0];
            
            var result5 = marsRover.Grid[0, 0];
            
            Assert.False(result1 == 0, "The rover did not change its X position");
            
            Assert.True(result1 == 1, "The rover did not change its X position correctly");
            
            Assert.True(result2 == 0, "The rover changed its Y position");
            
            Assert.True(result3 == (Direction)1, "The rover changed its heading");
            
            Assert.True(result4, "The rover did not update the grid with the new position");
            
            Assert.False(result5, "The rover did not update the old position on the grid");
        }
        
        //Test moving south to a valid position
        [Fact]
        public void TestMoveS()
        {
            marsRover.XPosition = 1;
            marsRover.YPosition = 1;
            marsRover.Heading = (Direction)2;
            
            marsRover.Navigate("M");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Heading;
            
            var result4 = marsRover.Grid[1, 0];
            
            var result5 = marsRover.Grid[1, 1];
            
            Assert.True(result1 == 1, "The rover changed its X position");
            
            Assert.False(result2 == 1, "The rover did not change its Y position");
            
            Assert.True(result2 == 0, "The rover did not change its Y position correctly");
            
            Assert.True(result3 == (Direction)2, "The rover changed its heading");
            
            Assert.True(result4, "The rover did not update the grid with the new position");
            
            Assert.False(result5, "The rover did not update the old position on the grid");
        }
        
        //Test moving west to a valid position
        [Fact]
        public void TestMoveW()
        {
            marsRover.XPosition = 1;
            marsRover.YPosition = 1;
            marsRover.Heading = (Direction)3;
            
            marsRover.Navigate("M");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Heading;
            
            var result4 = marsRover.Grid[0, 1];
            
            var result5 = marsRover.Grid[1, 1];
            
            Assert.False(result1 == 1, "The rover did not change its X position");
            
            Assert.True(result1 == 0, "The rover did not change its X position correctly");
            
            Assert.True(result2 == 1, "The rover changed its Y position");
            
            Assert.True(result3 == (Direction)3, "The rover changed its heading");
            
            Assert.True(result4, "The rover did not update the grid with the new position");
            
            Assert.False(result5, "The rover did not update the old position on the grid");
        }
        
        //Test moving north to an occupied position
        [Fact]
        public void TestOccupiedMoveN()
        {
            Rover roverBlock = new Rover(0, 1, (Direction)0, marsRover.Grid);
            
            marsRover.Navigate("M");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Grid[0, 0];
            
            Assert.True(result1 == 0, "The rover changed its X position");
            
            Assert.True(result2 == 0, "The rover changed its Y position");
            
            Assert.True(result3, "The rover updated the grid");
        }
        
        //Test moving east to an occupied position
        [Fact]
        public void TestOccupiedMoveE()
        {
            marsRover.Heading = (Direction)1;
            
            Rover roverBlock = new Rover(1, 0, (Direction)0, marsRover.Grid);
            
            marsRover.Navigate("M");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Grid[0, 0];
            
            Assert.True(result1 == 0, "The rover changed its X position");
            
            Assert.True(result2 == 0, "The rover changed its Y position");
            
            Assert.True(result3, "The rover updated the grid");
        }
        
        //Test moving south to an occupied position
        [Fact]
        public void TestOccupiedMoveS()
        {
            marsRover.XPosition = 1;
            marsRover.YPosition = 1;
            marsRover.Heading = (Direction)2;
            
            Rover roverBlock = new Rover(1, 0, (Direction)0, marsRover.Grid);
            
            marsRover.Navigate("M");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Grid[1, 1];
            
            Assert.True(result1 == 1, "The rover changed its X position");
            
            Assert.True(result2 == 1, "The rover changed its Y position");
            
            Assert.True(result3, "The rover updated the grid");
        }
        
        //Test moving west to an occupied position
        [Fact]
        public void TestOccupiedMoveW()
        {
            marsRover.XPosition = 1;
            marsRover.YPosition = 1;
            marsRover.Heading = (Direction)3;
            
            Rover roverBlock = new Rover(0, 1, (Direction)0, marsRover.Grid);
            
            marsRover.Navigate("M");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Grid[1, 1];
            
            Assert.True(result1 == 1, "The rover changed its X position");
            
            Assert.True(result2 == 1, "The rover changed its Y position");
            
            Assert.True(result3, "The rover updated the grid");
        }
        
        //Test moving to an out-of-bounds north position
        [Fact]
        public void TestOOBNorthMove()
        {
            marsRover.XPosition = 1;
            marsRover.YPosition = 1;
            
            marsRover.Navigate("M");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Grid[1, 1];
            
            Assert.True(result1 == 1, "The rover changed its X position");
            
            Assert.True(result2 == 1, "The rover changed its Y position");
            
            Assert.True(result3, "The rover updated the grid");
        }
        
        //Test moving to an out-of-bounds east position
        [Fact]
        public void TestOOBEastMove()
        {
            marsRover.XPosition = 1;
            marsRover.YPosition = 1;
            marsRover.Heading = (Direction)1;
            
            marsRover.Navigate("M");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Grid[1, 1];
            
            Assert.True(result1 == 1, "The rover changed its X position");
            
            Assert.True(result2 == 1, "The rover changed its Y position");
            
            Assert.True(result3, "The rover updated the grid");
        }
        
        //Test moving to an out-of-bounds south position
        [Fact]
        public void TestOOBSouthMove()
        {
            marsRover.Heading = (Direction)2;
            
            marsRover.Navigate("M");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Grid[0, 0];
            
            Assert.True(result1 == 0, "The rover changed its X position");
            
            Assert.True(result2 == 0, "The rover changed its Y position");
            
            Assert.True(result3, "The rover updated the grid");
        }
        
        //Test moving to an out-of-bounds west position
        [Fact]
        public void TestOOBWestMove()
        {
            marsRover.Heading = (Direction)3;
            
            marsRover.Navigate("M");
            
            var result1 = marsRover.XPosition;
            
            var result2 = marsRover.YPosition;
            
            var result3 = marsRover.Grid[0, 0];
            
            Assert.True(result1 == 0, "The rover changed its X position");
            
            Assert.True(result2 == 0, "The rover changed its Y position");
            
            Assert.True(result3, "The rover updated the grid");
        }
    }
}
