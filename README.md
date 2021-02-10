# InspectorContributions
A collection of useful community contributions for Odin Inspector. Requires Odin Inspector 3+.

# Installing
This repository is packaged as an .unitypackage that can be included in any Unity project. 

# Compatibility
This project will attempt to maintain compatibility with any and all versions of Unity supported by the version of Odin Inspector it targets. If something is not compatible with the version of Unity you are using, please either file an issue or fix it and submit a pull request.

# Contributing
This project uses:
* Unity 2019.4.19f1
* Odin Inspector 3+

You are required to provide your own copy of Odin Inspector. **Do not under any circumstance commit Odin Inspector to this project.**

All code contributed to this repository is licensed under the [MIT License](https://opensource.org/licenses/MIT). As a reminder: the contributors and maintainers of this repository are NOT responsible for providing support, nor are they responsible for any errors or issues caused by the inclusion or usage of this project in your Unity project. You use these contributions **at your own risk**.

All contributions should:
* Live under `Assets/Plugins/InspectorContributions/<Name of Your Contribution>`
  * This directory should include all the source files and resources needed for your contribution to operate.
* Be namespaced under `OdinCommunity.InspectorContributions`
* Include any demo code or assets under `Assets/Plugins/OdinCommunity/InspectorContributions/Demos/<Name of Your Contribution>`.
  * If your contribution is something like an attribute or custom drawer, please include a GameObject in the InspectorContributionsDemos scene that shows its functionality

# Style Guide
Coming soon.
