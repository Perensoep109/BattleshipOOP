package battleship;


import Objects.BaseGameObject;
import Objects.Grid;
import Objects.IClickable;
import Renderer.Render;
import javafx.scene.canvas.Canvas;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.GridPane;
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
        //create a grid
        grid = new Grid(0,0,20,20);
        //greate the arraylist with gameobjects
        gameObjects = new ArrayList<>();
        //add the grid to the gameobjects to be rendered
        gameObjects.add(grid);
        //draw the gameobjects
        render.draw(gameObjects);
        //add the canvas to the panel
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
        //TODO i guess i can put network closing stuff here
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