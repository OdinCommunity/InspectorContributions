# Page Slider
Author: Bjarke
Shared: 22-05-2019

An attribute that makes big editors with a lot of data more easy to navigate.

The root/first PageSlider property will act as the slide container, and all PageSliders inside of that will be drawn as a buttons which will slide to that object when pressed.

# Usage
```cs
[PageSlider]
public SomeTestClass test;

// Or place it above the class to enable it globally.
[PageSlider]
public class SomeOtherTestClass
{
}
```