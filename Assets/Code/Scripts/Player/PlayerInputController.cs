public class PlayerInputController : Singleton<PlayerInputController> {
  /// <summary>
  /// Class to handle character controls
  /// TODO: Make note of any music plugins we need here...
  /// </summary>
  // -------------- Input System ---------------
  public PlayerInputActions PlayerInputActions { get; private set; }

  protected override void Awake() {
    base.Awake();
    this.PlayerInputActions = new PlayerInputActions();
    this.PlayerInputActions.Enable();
  }
}
