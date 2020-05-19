package sceneswitcher;

import javafx.scene.input.DragEvent;
import javafx.scene.input.MouseEvent;

public interface IEventPane
{
    void onFocusGained(Object... a_data);   // An event which fires when the attached view gains focus
    void onFocusLost();                     // An event which fires when the attached view loses focus
    void onStop();                          // An event which fires when the application is being stopped
    void onClick(MouseEvent a_event);
}