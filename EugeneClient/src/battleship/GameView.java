package battleship;


import Networking.INetworking;
import Networking.NetworkSingleton;
import Networking.Packet;
import Objects.BaseGameObject;
import Objects.Grid;
import Objects.IClickable;
import Renderer.Render;
import javafx.scene.canvas.Canvas;

import javafx.scene.canvas.GraphicsContext;

import javafx.scene.input.KeyEvent;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.GridPane;
import sceneswitcher.IEventPane;

import java.util.ArrayList;
import java.util.List;

public class GameView extends GridPane implements IEventPane, IKeyPress
{
    GameManager gm;
    Canvas canvas;
    GraphicsContext gc;
    List<BaseGameObject> gameObjects;
    Grid enemyfield, playerfield;
    Render render;

    public GameView()
    {
        gm = new GameManager();
        gm.setGameView(this);

        //create the canvas
        canvas = new Canvas( 1000,1000 );
        gc = canvas.getGraphicsContext2D();
        //create a renderer
        render = new Render(gc);
        //create a grid
        enemyfield = new Grid(20,20,16 ,16,1);
        playerfield = new Grid(500,20,16 ,16,2);
        gm.setEnemyfield(enemyfield);
        gm.setPlayerfield(enemyfield);
        //greate the arraylist with gameobjects
        gameObjects = new ArrayList<>();

        //add the grid to the gameobjects to be rendered
        gameObjects.add(enemyfield);
        gameObjects.add(playerfield);
        //draw the gameobjects
        render.draw(gameObjects);
        //add the canvas to the panel
        this.add(canvas,0,1);
    }

    @Override
    public void onFocusGained(Object... a_data)
    {
        //make the canvas the size of the app
        canvas.setHeight(getHeight());
        canvas.setWidth(getWidth());

        if(NetworkSingleton.getInstance().getC().eventHandeler == null)
            NetworkSingleton.getInstance().getC().eventHandeler = new INetworking() {
                @Override
                public void OnRecievePacket(Packet packet) {

//                    System.out.println(Arrays.toString(packet.getData()));
//                    grid.getCell().changeTile();
                    GameManager.getInstance().getShot(packet.getData()[0],packet.getData()[1],packet.getData()[1]);


                }

            };
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

        enemyfield.getClickedCell(a_event.getX(), a_event.getY());
        playerfield.getClickedCell(a_event.getX(), a_event.getY());

        //draw the gameobjects
        render.draw(gameObjects);

    }

    @Override
    public void onKeyPress(KeyEvent a_event) {
        System.out.println("key pressed: " + a_event.getText());
    }

    public void registerShot(int gridID, int posY, int posX){
        for (BaseGameObject gameObject : gameObjects) {
            //test if it is clickable
            if(gameObject instanceof Grid ){
                Grid grid = (Grid) gameObject;
                if(grid.getGridId() == gridID){
                    grid.getCell(posY,posX).changeTile();
                }
            }
        }
    }
}