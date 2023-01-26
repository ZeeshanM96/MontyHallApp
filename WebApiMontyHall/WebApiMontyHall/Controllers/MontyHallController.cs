using Microsoft.AspNetCore.Mvc;

namespace WebApiMontyHall.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MontyHallController : ControllerBase
    {

        private readonly Random _random = new Random();

        [HttpGet]
        public async Task<IActionResult> Simulate(string shouldSwitch)
        {
            bool switchValue;
            if (shouldSwitch == "Stay")
            {
                switchValue = false;
            }
            else if (shouldSwitch == "Change")
            {
                switchValue = true;
            }
            else
            {
                return BadRequest("Invalid value for shouldSwitch");
            }
            int[] doors = { 0, 0, 1 };
            //Randomly place the car behind one of the doors
            int car = _random.Next(3);
            doors[car] = 1;

            //Randomly choose a door
            int choice = _random.Next(3);

            //Randomly open a door that doesn't have the car
            int opened;
            do
            {
                opened = _random.Next(3);
            } while (opened == choice || doors[opened] == 1);

            int finalChoice;
            if (switchValue)
            {
                //If the player chooses to switch, pick the remaining door
                do
                {
                    finalChoice = _random.Next(3);
                } while (finalChoice == opened || finalChoice == choice);
            }
            else
            {
                finalChoice = choice;
            }

            //Check if the player won or lost
            string result;
            if (finalChoice == car)
            {
                result = "Won";
            }
            else
            {
                result = "Lost";
            }

            var montyHallResult = new MontyHallResult
            {
                UserSelectedDoor = getDoorLabel(choice + 1),
                HostOpenedDoor = getDoorLabel(opened + 1),
                ShouldSwitch = switchValue,
                Result = result,
                CarDoor = getDoorLabel(car + 1)
            };

            return Ok(montyHallResult);
        }


        private string getDoorLabel(int doorNumber)
        {
            switch (doorNumber)
            {
                case 1:
                    return "A";
                case 2:
                    return "B";
                case 3:
                    return "C";
                default:
                    return "Unknown";
            }
        }



    }
}

