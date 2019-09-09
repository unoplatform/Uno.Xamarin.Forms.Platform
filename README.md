# Uno Platform WebAssembly Renderers for Xamarin.Forms 

This repository is the home of the Uno.Xamarin.Forms.Platform package, which provides a set of Xamarin.Forms renderers to provide WebAssembly support through the Uno Platform.

You can read about the Uno Platform https://platform.uno, and about Xamarin.Forms at https://www.xamarin.com/forms.

<img src="banner.png" alt="Xamarin.Forms banner" height="145" >

## Build Status ##

[![Build Status](https://uno-platform.visualstudio.com/Uno%20Platform/_apis/build/status/Uno%20Platform/Uno.Xamarin.Forms%20-%20CI?branchName=uno)](https://uno-platform.visualstudio.com/Uno%20Platform/_build/latest?definitionId=12&branchName=uno)


## Getting Started ##

- Require VS 2019 with asp.net web development and .Net Core cross-platform Workloads.


## Building the renderers ##

- Using Visual Studio 2019 (16.2 or later)
- Open the `Uno.Xamarin.Forms.Platform.sln` solution
- Build the `Xamarin.Forms.ControlGallery.Uno.Wasm` project
- Run it without the debugger (usually with `Ctrl+F5`)

## Coding Style ##

We follow the style used by the [.NET Foundation](https://github.com/dotnet/corefx/blob/master/Documentation/coding-guidelines/coding-style.md), with a few exceptions:

- We do not use the `private` keyword as it is the default accessibility level in C#.
- We use hard tabs over spaces. You can change this setting in VS 2015 via `Tools > Options` and navigating to `Text Editor > C#` and selecting the "Keep tabs" radio option. In Visual Studio for Mac it's set via preferences in `Source Code > Code Formatting > C# source code` and disabling the checkbox for `Convert tabs to spaces`.
- Lines should be limited to a max of 120 characters (or as close as possible within reason). This may be set in Visual Studio for Mac via preferences in `Source Code > Code Formatting > C# source code` and changing the `Desired file width` to `120`.

## Contributing ##

- [How to Contribute](https://github.com/xamarin/Xamarin.Forms/blob/master/.github/CONTRIBUTING.md)

### Reporting Bugs ###

We use [GitHub Issues](https://github.com/xamarin/Xamarin.Forms/issues) to track issues. If at all possible, please submit a [reproduction of your bug](https://gist.github.com/jassmith/92405c300e54a01dcc6d) along with your bug report.