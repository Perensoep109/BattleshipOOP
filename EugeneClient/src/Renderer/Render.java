package Renderer;

import Objects.BaseGameObject;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;

import java.util.ArrayList;
import java.util.List;

public class Render {
    GraphicsContext gc;
    List<BaseGameObject> toDelete;

    public Render(GraphicsContext gc) {
//        set the Graphics contexts
        this.gc = gc;
//        initialize the todelete array
        toDelete = new ArrayList<BaseGameObject>();
    }

    public void clearCanvas(){
//      clear the graphics contexts
        gc.clearRect(0,0,1000,1000);
    }

    public void draw(List<BaseGameObject> gameObjects){
        //clear the canvas
        clearCanvas();
        //print the total gameobjects in the array
        System.out.println("total gameobjects: " + gameObjects.size());
        //iterate though all gameobjects
        for (BaseGameObject gameObject : gameObjects) {
//            test if gameobject needs to be deleted
            if (gameObject.isDeleteOnNextDraw()){
                // if so add it to the delete array
                toDelete.add(gameObject);
            }
            //draw all the gameobjects
            gameObject.draw(gc);
        }
        //remove all the gameobjects
        gameObjects.removeAll(toDelete);
    }
}
