1. Open CodeChallenge.sln in Visual Studio 2019
2. Build the solution
3. Run CodeChallenge.Tests to ensure all unit tests are passing
4. Modify the CodeChallenge.Console app to you needs to manually test the project.
5. According to the important notes
	"How do we scale the solution?
		1. Adding more Sports? MLB, NHL, NBA
		2. Adding all the NFL teams?" 
	For Point 1: Im not too sure about what requirements there are for MLB, NHL or NBA as i don't really watch them however if you let me know
	of the differences im happy to modify the application.
	The depthChart is standard if we need to add it for other teams we will create a new team model (with the required fields that i dont have) 
	and attach the depth chart to it.
6. Some of your sample inputs/outputs might be incorrect (unless if i've understood the exercise)

	getBackups(“QB”, JaelonDarden)
	/* Output */
	#10 – Scott Miller

	Above is incorrect, player is configured for a different position.

	removePlayerFromDepthChart(“WR”, MikeEvans)
	/* Output */
	#13 – MikeEvans
	
	Player doesn't exist at that position
7. Happy to hear your suggestions back :)