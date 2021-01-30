# Awesome Product

This is an awesome product to use, it processes things!
##  
## Running the Awesome Product

To run, just start the project on Visual Studio, the UI should be launched and the two APIs should start listening to awesome requests.

# Testing

Both Unit and End-to-End tests can be run on the angular UI by using:

`npm run test` - Unit Tests
`npm run e2e` - End-To-End

The Unit Test run generates a code coverage report that will be accessible in the Coverage folder of the Angular Project folder.
The Terminal Run will also display the desired coverage thresholds

## To help other developers, when running in develop mode each API will expose a swagger endpoint


# Security

Great amounts of effort were put into the security of this Awesome Product!

to know what's the current state run `npm audit`
At the time of writing, all possible fixes were applied "**automagically**" by running `npm audit fix`

# Design decisions and assumptions

I thought about making the history view a feature module and lazy load it, but due to the simplicity of the app I didn't implement it.

I extracted the data display from the processing view to be able to reutilize it on the history view.
