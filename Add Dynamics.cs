#define VER_GEQ_14 // remove this for vegas 13
#if VER_GEQ_14
using ScriptPortal.Vegas;
#else
using Sony.Vegas;
#endif
using System.Windows.Forms;

public class EntryPoint {
    string DynamicsID = "{00000008-0F56-11D2-9887-00A0C969725B}";
    public void FromVegas(Vegas vegas) {
        foreach (PlugInNode plugin in vegas.AudioFX) {
            if (plugin.Name == "ExpressFX Dynamics" && plugin.UniqueID == DynamicsID) {
                foreach (Track track in vegas.Project.Tracks) {
                    foreach (TrackEvent trackEvent in track.Events) {
                        if (trackEvent.Selected && trackEvent.IsAudio()) {
                            AudioEvent audio = trackEvent as AudioEvent;
                            foreach (Effect effect in audio.Effects) {
                                if (effect.PlugIn.UniqueID == DynamicsID) {
                                    audio.Effects.Remove(effect);
                                }
                            }
                            Effect DynamicsPlugin = audio.Effects.AddEffect(plugin);
                            foreach (EffectPreset preset in DynamicsPlugin.Presets) {
                                if (preset.Name == "preset") {
                                    DynamicsPlugin.CurrentPreset = preset;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
