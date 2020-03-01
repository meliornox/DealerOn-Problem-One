/* 
 * Mars Rover Navigation Assistant
 * 
 * Author: Theodore Bigelow
 * Date: 02/27/20
 * 
 * This is the solution to option 1, the Mars Rover problem, of the problems given by DealerOn for version 3 of their Development Candidate Coding Test.
 * 
 * The program takes and receives instructions from the command line. 
 * The first line needed is a pair of integers separated by a space defining the northeast point of the "plateau" that the rovers navigate, assuming the southwest corner is the origin (0 0).  
 * The second line needed is the position and heading of a rover consisting of a pair of integers and a cardinal direction, N, S, E, or W. Again the three inputs need to be separated by spaces. 
 * The third line needed is the instructions for the rover, a series of letters only consisting of L for a left turn, R for a right turn, or M for moving along the current heading. It is entirely valid to give no instructions to a rover. 
 * The program will continue to accept additional lines of rover positions and instructions until nothing is entered for the next rover, in which case all the instructions will be executed for all the given rovers. 
 * In order to conform closely to the parameters of the problem the program does not give any output other than the final positions of the rovers unless there are issues with the inputs.
 * 
 */

using System;
using System.Collections.Generic;

namespace MarsRover.Manager
{
    //Defines and restricts the characters representing the cardinal directions
    public enum Direction { N, E, S, W }
    //Defines and restricts the characters representing the possible instructions
    public enum Instruction { L, R, M }
    
    //Rover object to handle navigation
    public class Rover
    {
        //The x position, y position, and grid need restrictions, so they are private and gated by set
        private int _xPosition;
        private int _yPosition;
        private bool[,] _grid;
        
        //Heading is already restricted by the Direction enum, so it's fine as public and default
        public Direction Heading { get; set; }
        
        //Make sure the target x position is positive and within the bounds of the grid and also that there is no rover at the target x position and the current y position
        public int XPosition 
        {
            get => _xPosition;
            set
            { 
                if(value == _xPosition) {}
                else if((value < 0) || (value > (_grid.GetLength(0) - 1)))
                    throw new ArgumentException("The new X coordinate is not a valid coordinate on the plateau");
                else if(_grid[value, _yPosition] == true)
                    throw new ArgumentException("There is already a rover on the plateau at the new X coordinate and the current Y coordinate");
                else
                {
                    _grid[_xPosition, _yPosition] = false;
                    _xPosition = value;
                    _grid[_xPosition, _yPosition] = true;
                }
            }
        }
        
        //Make sure the target y position is positive and within the bounds of the grid and also that there is no rover at the current x position and the target y position
        public int YPosition
        {
            get => _yPosition;
            set
            { 
                if(value == _yPosition) {}
                else if((value < 0) || (value > (_grid.GetLength(1) - 1)))
                    throw new ArgumentException("The new Y coordinate is not a valid coordinate on the plateau");
                else if(_grid[_xPosition, value] == true)
                    throw new ArgumentException("There is already a rover on the plateau at the current X coordinate and the new Y coordinate");
                else
                {
                    _grid[_xPosition, _yPosition] = false;
                    _yPosition = value;
                    _grid[_xPosition, _yPosition] = true;
                }
            }
        }
        
        //Make sure the target grid is big enough to hold the rover at its current position and that the rover's current position is not occupied on the new grid
        public bool[,] Grid
        {
            get => _grid;
            set
            {
                if((_xPosition + 1 > value.GetLength(0)) || (_yPosition + 1 > value.GetLength(1)))
                    throw new ArgumentException("The new grid is too small to relocate the rover to the same position on the new grid as it is on the current one");
                else if(value[_xPosition, _yPosition])
                    throw new ArgumentException("There is already a rover on the new grid at the rover's current location");
                else
                {
                    _grid = value;
                    _grid[_xPosition, _yPosition] = true;
                }
            }
        }
        
        //Constructor
        //Makes sure the x and y values are valid in the grid and that there is not already a rover at the target location
        public Rover(int x, int y, Direction direction, bool[,] grid)
        {
            if((grid.GetLength(0) == 0) || (grid.GetLength(1) == 0))
                throw new ArgumentException("The plateau is not large enough to hold a rover");
            else if((x < 0) || (x > grid.GetLength(0) - 1))
                throw new ArgumentException("The X location provided is not valid on the plateau");
            else if((y < 0) || (y > grid.GetLength(1) - 1))
                throw new ArgumentException("The X location provided is not valid on the plateau");
            else if(grid[x, y] == true)
                throw new ArgumentException("There is already a rover on the plateau at that location");
            else
            {
                _xPosition = x;
                _yPosition = y;
                Heading = direction;
                _grid = grid;
                _grid[_xPosition, _yPosition] = true;
            }
        }
        
        //Move within the grid by the given instructions
        public void Navigate(string instructions)
        {
            //Holds the current instruction
            Instruction instruction;
            
            //Iterate through the instruction string
            foreach(char c in instructions)
            {
                //Turn the character into an Instruction
                Enum.TryParse(c.ToString(), out instruction);
               
                //Execute the Instruction
                switch(instruction)
                {
                    //Instruction is "L"
                    case (Instruction)0:
                        //If the current heading is at the beginning of Direction loop around, otherwise decrement
                        if(Heading == (Direction)0)
                            Heading = (Direction)3;
                        else 
                            Heading--;
                        break;
                    //Instruction is "R"
                    case (Instruction)1:
                        //If the current heading is at the end of Direction loop around, otherwise increment
                        if(Heading == (Direction)3)
                            Heading = (Direction)0;
                        else
                            Heading++;
                        break;
                    //Instruction is "M"
                    case (Instruction)2:
                        switch(Heading)
                        {
                            //Heading is "N"
                            case (Direction)0:
                                //Test to see if the next position would be out of bounds or if there is a rover already at the target position, otherwise move and update the grid
                                if(_yPosition + 1 <= _grid.GetLength(1) - 1 && !_grid[_xPosition, _yPosition + 1])
                                {
                                    _grid[_xPosition, _yPosition] = false;
                                    _yPosition++;
                                    _grid[_xPosition, _yPosition] = true;
                                }
                                break;
                            //Heading is "E"
                            case (Direction)1:
                                //Test to see if the next position would be out of bounds or if there is a rover already at the target position, otherwise move and update the grid
                                if(_xPosition + 1 <= _grid.GetLength(0) - 1 && !_grid[_xPosition + 1, _yPosition])
                                {
                                    _grid[_xPosition, _yPosition] = false;
                                    _xPosition++;
                                    _grid[_xPosition, _yPosition] = true;
                                }
                                break;
                            //Heading is "S"
                            case (Direction)2:
                                //Test to see if the next position would be out of bounds or if there is a rover already at the target position, otherwise move and update the grid
                                if(_yPosition - 1 >= 0 && !_grid[_xPosition, _yPosition - 1])
                                {
                                    _grid[_xPosition, _yPosition] = false;
                                    _yPosition--;
                                    _grid[_xPosition, _yPosition] = true;
                                }
                                break;
                            //Heading is "W"
                            case (Direction)3:
                                //Test to see if the next position would be out of bounds or if there is a rover already at the target position, otherwise move and update the grid
                                if(_xPosition - 1 >= 0 && !_grid[_xPosition - 1, _yPosition])
                                {
                                    _grid[_xPosition, _yPosition] = false;
                                    _xPosition--;
                                    _grid[_xPosition, _yPosition] = true;
                                }
                                break;
                        }                            
                        break;
                }
            }
        }
    }
    
    class MarsRover
    {
        static void Main(string[] args)
        {
            //Lines of instructions
            List<string> instructionLines= new List<string>();
            //Rovers
            List<Rover> rovers = new List<Rover>();
            //Grid for the rovers initialized by parsing the first line
            bool[,] pointGrid = ParseFirstInput();
            
            //Parse the following lines into rovers and instructions using pointGrid
            ParseNextInput(rovers, instructionLines, pointGrid);
            
            //Iterate over the rovers and call their instructions
            for(int i = 0; i < rovers.Count; i++)
            {
                //Send instructions based on the string at i+1 and print the output
                rovers[i].Navigate(instructionLines[i]);
                Console.WriteLine(rovers[i].XPosition + " " + rovers[i].YPosition + " " + rovers[i].Heading);
            }
        }
        
        //This method parses the first line of input
        private static bool[,] ParseFirstInput()
        {
            //Current line
            string input;
            //Allows each input value on the current line to be accessed by index
            string[] splitInput;
            //Array of both dimensions
            int[] dimensions = { 0, 0 };
            
            //Loop until we get a valid input
            while(true)
            {
                //Take route input
                input = Console.ReadLine();
            
                //Split up the input into X and Y
                splitInput = input.Split(' ');
            
                //Validate the number of inputs
                if(splitInput.Length != 2)
                    Console.WriteLine("Please enter 2 and only 2 dimensions: ");
                //Check that the first input is a positive integer
                else if(!int.TryParse((splitInput[0]), out dimensions[0]) || (dimensions[0] < 0))
                    Console.WriteLine("Please enter a positive integer for the X dimension of the plateau");
                //Check that the second input is a positive integer
                else if(!int.TryParse((splitInput[1]), out dimensions[1]) || (dimensions[1] < 0))
                    Console.WriteLine("Please enter a positive integer for the Y dimension of the plateau");
                //Return the validated 2D array
                else
                    return new bool[dimensions[0] + 1, dimensions[1] + 1];
            }
        }
        
        //This method parses the lines of input into rovers and instructions using the given 2D array of points
        private static void ParseNextInput(List<Rover> rovers, List<string> instructions, bool[,] plateau)
        {
            //Current line
            string input;
            
            //Allows the state of the current rover to be accessed by index
            string[] startState;
            
            //X position of the current rover
            int xPosition;
            //Y position of the current rover
            int yPosition;
            //Heading of the current rover
            Direction heading;
            
            //The current rover's Instructions
            Instruction instruction;
            
            //Flag for the for loop that validates each instruction
            bool instructionsValid;
            
            //Loop until the next rover is not entered
            while(true)
            {
                //Read rover's starting state
                input = Console.ReadLine();
                //If no input is given for a rover, break from the while loop to stop reading input
                if(input.Length == 0)
                    break;
                //Else if valid add new rover
                else
                {
                    //Split up the input into X, Y, and Z
                    startState = input.Split(' ');
                    //Validate number of inputs
                    if(startState.Length != 3)
                        Console.WriteLine("Please enter 3 and only 3 inputs for the starting state of a rover");
                    //Validate the first input
                    else if(!int.TryParse(startState[0], out xPosition))
                        Console.WriteLine("Please enter an integer for the X position of a rover");
                    //Validate the second input
                    else if(!int.TryParse(startState[1], out yPosition))
                        Console.WriteLine("Please enter a positive integer for the Y position of a rover");
                    //Validate the third input
                    else if(!Enum.TryParse(startState[2], out heading))
                        Console.WriteLine("Please enter a valid direction for the starting  heading of a rover (N E S W)");
                    else
                    {
                        //Try to make a new Rover, if valid then get the instruction
                        try
                        {
                            rovers.Add(new Rover(xPosition, yPosition, heading, plateau));
                            
                            //Loop until we have valid instructions for the rover
                            while(true)
                            {
                                //Read instructions
                                input = Console.ReadLine();
                                
                                //Flag for the foreach
                                instructionsValid = true;
                            
                                //Iterate through the input and validate that everything is an instruction, blank instruction sequences are not checked
                                foreach(char c in input)
                                {
                                    //Validation
                                    if(!Enum.TryParse(c.ToString(), out instruction))
                                    {
                                        //Trip the flag
                                        instructionsValid = false;
                                        Console.WriteLine(c + " is not a valid instruction,     please only use R, L, or M");
                                        //Don't bother with the rest of the characters
                                        break;
                                    } 
                                }
                                //If valid add new instruction (return is valid)
                                if(instructionsValid)
                                {
                                    //Add the current instruction string to the instructions list
                                    instructions.Add(input);
                                    break;
                                }
                            }
                        }
                        //If the rover is not valid, advise the user and loop
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }
        }
    }
}
