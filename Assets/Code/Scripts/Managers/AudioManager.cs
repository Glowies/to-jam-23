using UnityEngine;

/// this is where all our functions are ///
public class AudioManager : MonoBehaviour
{
    /// this is where all our variable declaration goes ///
    /// UI ///
    public AK.Wwise.Event UIStart;
    public AK.Wwise.Event UICancel;
    public AK.Wwise.Event UIHover;
    public AK.Wwise.Event UISelect;
    /// heavy breathing ///
    public AK.Wwise.Event heavyBreathingstart;
    public AK.Wwise.Event heavyBreathingstop;
    /// old man noises ///
    public AK.Wwise.Event Grunt;
    public AK.Wwise.Event Death;
    /// eye squelchies ///
    public AK.Wwise.Event eyeSquelchies;
    /// unsafe music and drones ///
    public AK.Wwise.Event UnsafeDroneStart;
    public AK.Wwise.Event UnsafeDroneStop;
    public AK.Wwise.Event UnsafeMusicStart;
    public AK.Wwise.Event UnsafeMusicStop;
    // eye detection
    public AK.Wwise.Event EyeDetectionStart;
    public AK.Wwise.Event EyeDetectionStop;

    /// UI ///
    public void PlayUIStart(){
        UIStart.Post(gameObject);
    }
    public void PlayUICancel(){
        UICancel.Post(gameObject);
    }
    public void PlayUIHover(){
        UIHover.Post(gameObject);
    }
    public void PlayUISelect(){
        UISelect.Post(gameObject);    
    }
    /// heavy breathing ///
    public void PlayHeavyBreathingStart(){
        heavyBreathingstart.Post(gameObject);
    }   
    public void PlayHeavyBreathingStop(){
        heavyBreathingstop.Post(gameObject);
    }
    /// old man noises ///
    public void PlayGrunt(){
        Grunt.Post(gameObject);
    }
    public void PlayDeath(){
        Death.Post(gameObject);
    }
    /// eye squelchies ///  
    public void PlayEyeSquelchies(){
        eyeSquelchies.Post(gameObject);
    }    
    /// unsafe music and drones ///
    public void PlayUnsafeDroneStart(){
        UnsafeDroneStart.Post(gameObject);
    }
    public void PlayUnsafeDroneStop(){
        UnsafeDroneStop.Post(gameObject);
    }
    public void PlayUnsafeMusicStart(){
        UnsafeMusicStart.Post(gameObject);
    }
    public void PlayUnsafeMusicStop(){
        UnsafeMusicStop.Post(gameObject);
    }
    public void PlayEyeDetectionStart(){
        EyeDetectionStart.Post(gameObject);
    }

    public void PlayEyeDetectionStop()
    {
        EyeDetectionStop.Post(gameObject);
    }
}