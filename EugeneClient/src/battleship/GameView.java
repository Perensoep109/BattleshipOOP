package battleship;


import Objects.BaseGameObject;
import Objects.Boat;
import Objects.Grid;
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
    Grid grid;
    Render render;
    public GameView()
    {

        //create the canvas
        canvas = new Canvas( 1000,1000 );
        gc = canvas.getGraphicsContext2D();
        //create a renderer
        render = new Render(gc);
        grid = new Grid(0,0,20,20);
        //create a boat this will be the first boat
//        Boat boat1 = new Boat(1,1,new Image("ship1.png"),2,1);
        gameObjects = new ArrayList<>();
//        gameObjects.add(boat1);
        gameObjects.add(grid);
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
            //test if it is clickable
            if(gameObject instanceof IClickable){
                //if so run the function onclick
                if(gameObject.clickedOn(a_event.getX(), a_event.getY())) {
                    ((IClickable) gameObject).onClick();
                }
            }
        }

        grid.getClickedCell(a_event.getX(), a_event.getY());

        //draw the gameobjects
        render.draw(gameObjects);

    }

}