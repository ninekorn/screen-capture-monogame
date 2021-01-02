2021-jan-02-
i wanted this to have the 60 days trial using the ab3d.dxengine.OculusWrap, right out of the box, for people to use the ab3d.dxengine.oculusWrap. I modified my repos so you won't be able to make them work out of the box anymore.

You will have to go clone the repository here https://github.com/ab4d/Ab3d.OculusWrap and build the dlls separately for yourselves. If the ab3d.dxengine.oculusWrap would be provided in the future with a nugget you won't have to do those steps. 

1. Clone the github repository here: https://github.com/ab4d/Ab3d.OculusWrap
2. Build the ab3d.OculusWrap solution first with the FrameWork 4.5 or 4.7.2 whatever.
3. Then build the solution ab3d.DXEngine.OculusWrap.
4. use both the ab3d.OculusWrap.dll and the ab3d.DXEngine.OculusWrap as references for my projects sc_core_systems and SCCoreSystems and the solution sccsv10 and this one sccsv11 and that one too sccsVD4VE. Those DLLs will make the projects work. after inserting those references, rebuild your projects and this should take care of restoring the nugget packages for the other dlls to load.

thank you for reading me,
steve chassé

2020-dec-25-

# screen-capture-monogame
screen capture using sharpdx in monogame. i used xoofx original ScreenCapture c# scripts in the SharpDX-Samples found here:
https://github.com/sharpdx/SharpDX-Samples/tree/master/Desktop/Direct3D11.1/ScreenCapture

i have built this program by myself using the normal standard monogame startup project.

currently it's quite lame, you have to create a folder named "monoScreen" on your desktop and monogame writes the desktop screen to a png/jpg inside of that folder. Now i just need to have it displayed on the screen. it's just a backup program kinda, that i will mix with my repo https://github.com/ninekorn/SCCoreSystemsMono soon. when i have the chance to script.

thank you for reading me.
steve chassé
