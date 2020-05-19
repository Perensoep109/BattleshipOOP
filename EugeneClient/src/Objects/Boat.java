package Objects;

import javafx.scene.image.Image;

public class Boat extends BaseGameObject implements IClickable{
    public int length;
    public int rotation;
//    the constructor
    public Boat(double posY, double posX, Image sprite, int length, int rotation) {
        super(posY, posX, sprite);
        this.length = length;
        this.rotation = rotation;
    }

    public int getLength() {
        return length;
    }

    public void setLength(int length) {
        this.length = length;
    }

    public int getRotation() {
        return rotation;
    }

    public void setRotation(int rotation) {
        this.rotation = rotation;
    }

    @Override
    public void onClick() {
        deleteOnNextDraw();
    }
}
