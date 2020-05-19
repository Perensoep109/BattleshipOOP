package battleship;


import Objects.BaseGameObject;
import Objects.Boat;
import Objects.IClickable;
import Renderer.Render;
import javafx.scene.canvas.Canvas;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.control.Label;
import javafx.scene.image.Image;
import javafx.scene.input.DragEvent;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.GridPane;
import javafx.scene.paint.Color;
import sceneswitcher.IEventPane;

import java.util.ArrayList;
import java.util.List;

public class GameView extends GridPane implements IEventPane
{
    Canvas canvas;
    GraphicsContext gc;
    List<BaseGameObject> gameObjects;
    Render render;
    public GameView()
    {

        //create the canvas
        canvas = new Canvas( 1000,1000 );
        gc = canvas.getGraphicsContext2D();
        //create a renderer
        render = new Render(gc);

        //create a boat this will be the first boat
        Boat boat1 = new Boat(100,100,new Image("ship1.png"),2,2);
        gameObjects = new ArrayList<>();
        gameObjects.add(boat1);
        //draw the boat
        render.draw(gameObjects);
        this.add(canvas,0,0);


    }

    @Override
    public void onFocusGained(Object... a_data)
    {
        //make the canvas the size of the app
        canvas.setHeight(getHeight());
        canvas.setWidth(getWidth());
    }

    @Override
    public void onFocusLost()
    {

    }

    @Override
    public void onStop() {

    }



    @Override
    public void onClick(MouseEvent a_event) {
        //when a element is clicked set it on true so no new object will be made
      boolean _clickedObject = false;
      //loop though all gameobjects to test for colissions
        for (BaseGameObject gameObject : gameObjects) {
            //save the gameobject
            BaseGameObject _yee = gameObject.clickedOn((float)a_event.getX(),(float)a_event.getY());
            //test if it is clickable
            if(_yee instanceof IClickable){
                //if so run the function onclick
                ((IClickable) _yee).onClick();
                //set the variable on true
                _clickedObject = true;
            }
        }
        //if _clickedObject is false create new boat
        if(!_clickedObject){
            Boat a_boat = new Boat(a_event.getY(),a_event.getX(),new Image("ship1.png"),2,2);
            gameObjects.add(a_boat);

        }
        //draw the gameobjects
        render.draw(gameObjects);

    }

}