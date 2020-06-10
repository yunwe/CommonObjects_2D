# CommonObjects_2D
Collection of 2d common game objects I tested in my free time. For each objects, they have their own directory under `Assets` folder. A sample scene, sprites, and scripts are organized in that folder.
![](https://res.cloudinary.com/yunwe/image/upload/v1591706893/GitReadMePhoto/2d_objects/Screenshot_at_Jun_09_19-16-35.png)

Here is the list of objects in my collection
- Spinner
 
## Examples
- Spinner
![](https://res.cloudinary.com/yunwe/image/upload/v1591706895/GitReadMePhoto/2d_objects/Screenshot_at_Jun_09_19-11-34.png)
Stopping spinner at pre-defined index is very useful if we want to control the output. You can test it by typing in the stop index before clicking stop button.
I calculated deceleration to stop the wheel with desire effect. However, it have some error rate. It works fine with large slots but if your spinner have too many small slots, please be aware the pitfall.

## Examples
- Slot Machine
![](https://res.cloudinary.com/yunwe/image/upload/v1591803462/Screenshot_at_Jun_10_21-55-57.png)
You can define the stop index just like in spinner. If you define stop index 0, it will stop at the last entity of the row.(I define 10 entity for each rows, hence, stopping index for 0 would be 9 in my case.) The first and last entity of the row must identical in order to create loop effect.
