package Objects;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;

public class BaseGameObject {
    public double posY;
    public double posX;
    public Image sprite;
    public boolean clickable = true;
    public boolean deleteOnNextDraw = false;

    public BaseGameObject(double posY, double posX, Image sprite) {
        this.posY = posY;
        this.posX = posX;
        this.sprite = sprite;
    }

    public double getPosY() {
        return posY;
    }

    public void setPosY(double posY) {
        this.posY = posY;
    }

    public double getPosX() {
        return posX;
    }

    public void setPosX(double posX) {
        this.posX = posX;
    }

    public Image getSprite() {
        return sprite;
    }

    public void setSprite(Image sprite) {
        this.sprite = sprite;
    }

    public boolean isClickable() {
        return clickable;
    }

    public void setClickable(boolean clickable) {
        this.clickable = clickable;
    }

    public boolean isDeleteOnNextDraw() {
        return deleteOnNextDraw;
    }

    public void setDeleteOnNextDraw(boolean deleteOnNextDraw) {
        this.deleteOnNextDraw = deleteOnNextDraw;
    }

    public BaseGameObject clickedOn(float clickposX, float clickposY){
        // test if a object is clicked on
        if(posY < clickposY && posY + sprite.getHeight() > clickposY){
            if(posX < clickposX && posX + sprite.getWidth() > clickposX) {
//                test if the object is actually clickable
                if(clickable){
//                    return the object
                    return this;
                }
                return null;

            }
        }
        return null;
    }

    //set the bool to delete it on the next draw to prevent it from drawing before deleting it will be drawn off screen
    public void deleteOnNextDraw(){
        posX = 100000;
        posY = 100000;
        deleteOnNextDraw = true;
    }


//draw the object
    public void draw(GraphicsContext gc) {
        gc.drawImage( sprite, posX, posY );
    }
}
