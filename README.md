
--Instructions--
I'm using .NET on Fedora. I used the command line to run and test the solution, so my instructions use the command line.

Run the project using "dotnet run" in the MarsRover folder.
Test the project using "dotnet test" in the main folder.

When using the command line to give inputs, the rovers will execute their instructions once there is no data for the next rover's position and heading.

--Assumptions--

All the provided information will be added line-by-line.

The rovers should execute their instructions once all instructions were received for all rovers.

The input will not necessarily be pre-validated.

Rovers should not overlap, whether when navigating or when confirming location. 

When navigating rovers should stay within the boundaries of the plateau.

No extra output should be given for valid input.

The instructions for a rover can be blank.

There can be any number of rover-instructions pairs.

The Rover class should be fully implemented with setters even though no setters are used in the solution.

Rather than generating errors, rovers should skip invalid instructions and not move when they would run over another rover or move out of bounds, continuing with execution.

--Design--

The problem is very clear about the restrictions on the headings of the rovers and the instructions that can be given to them. I made two enum types, Direction and Instruction, to restrict them to their valid values.

The input validation took up a disproportionate amount of main() so I split it into two methods. ParseFirstInput() validates the first line and provides the plateau grid. ParseNextInput() validates the following rover-instructions pairs and provides a list of rovers and a list of instructions through two of the parameters. ParseNextInput leaves some of the validation to the Rover class using try-catch blocks.

Once the input is parsed into lists, the main method iterates through them and directs each rover to move in turn according to the instructions. Each rover has a grid it navigates on, an X position on the grid, a Y position on the grid, and a heading. The Rover class validates the information given to its constructor and setters to ensure expected behavior. Rovers can turn and move on the grid with the Navigate() method, and they will make sure to stay within the bounds and not run into other rovers.
