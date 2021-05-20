# VR_Fall2018_1
Experiment comparing vr, 3d and eyetracking in the context of category learning.

This repository represents the original source code used for the Virtual Reality condition of the experiment.
This was developed on Unity and the HTC Vive system was the hardware used for this condition. One of the 2018
or 2019 versions of Unity should work for building the project.

To avoid resource issues on the host, it is recommended to build an .exe and run the experiment using the .exe.
See https://github.com/SFU-Cognitive-Science-Lab/InProgress/tree/master/CategoryVR/ExpConditions/VR for a sample.

Before running this either via the Unity editor or the stand alone .exe make sure to copy the experiments.config.txt
file to the Desktop directory. The VR program bootstraps itself by looking for this file. 

The experiment depends on an additional configuration file, arrangements.json, that contains all of the conditions for the
experiment. When running the experiment from the editor, the path to arrangements.json can probably be left as-is. 
However, when running from a stand alone .exe file the full path to the file should be put in the experiments.config.txt file.

The experiment depends on the NewtonSoft json library to parse the arrangements.json file.

[Robin where do we find the running procedures for the experiment?]
