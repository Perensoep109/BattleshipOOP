package Objects;

import javafx.scene.SnapshotParameters;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.paint.Color;

public class BaseGameObject {
    public double posY;
    public double posX;
    public Image sprite;
    public boolean deleteOnNextDraw = false;
    public int rotation;
    public BaseGameObject(double posY, double posX, Image sprite, int rotation) {
        this.posY = posY;
        this.posX = posX;
        this.sprite = sprite;
        this.rotation = rotation;
    }

    public BaseGameObject(double posY, double posX) {
        this.posY = posY;
        this.posX = posX;
    }
    public int getRotation() {
        return rotation;
    }

    public void setRotation(int rotation) {
        this.rotation = rotation;
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

    public boolean isDeleteOnNextDraw() {
        return deleteOnNextDraw;
    }

    public void setDeleteOnNextDraw(boolean deleteOnNextDraw) {
        this.deleteOnNextDraw = deleteOnNextDraw;
    }

    public boolean clickedOn(double clickposX, double clickposY){
        return (posY < clickposY && posY + sprite.getHeight() > clickposY && posX < clickposX && posX + sprite.getWidth() > clickposX);
    }


    //set the bool to delete it on the next draw to prevent it from drawing before deleting it will be drawn off screen
    public void deleteOnNextDraw(){
        posX = 100000;
        posY = 100000;
        deleteOnNextDraw = true;
    }



//draw the object

    protected void drawImage(GraphicsContext gc, double xPos, double yPos)
    {
        ImageView iv = new ImageView(sprite);
        iv.setRotate(rotation * 90);
        SnapshotParameters params = new SnapshotParameters();
        params.setFill(Color.TRANSPARENT);
        Image rotatedImage = iv.snapshot(params, null);
        gc.drawImage(rotatedImage, xPos, yPos );
    }

    public void draw(GraphicsContext gc ) {
        if(sprite != null)
            drawImage(gc, posX, posY);
    }

}
