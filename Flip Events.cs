#define VER_GEQ_14 // remove this for vegas 13
#if VER_GEQ_14
using ScriptPortal.Vegas;
#else
using Sony.Vegas;
#endif
using System.Windows.Forms;

public class EntryPoint {
    public void FromVegas(Vegas vegas) {
        foreach (Track track in vegas.Project.Tracks) {
            int count = 1;
            foreach (TrackEvent trackEvent in track.Events) {
                if (trackEvent.Selected && trackEvent.IsVideo()) {
                    VideoEvent video = trackEvent as VideoEvent;
                    foreach (VideoMotionKeyframe keyframe in video.VideoMotion.Keyframes) {
                        VideoMotionVertex tl = keyframe.TopLeft, 
                                          tr = keyframe.TopRight, 
                                          bl = keyframe.BottomLeft, 
                                          br = keyframe.BottomRight;
                        VideoMotionBounds bh = new VideoMotionBounds(tl, tr, br, bl);
                        if (count == 2)
                            bh = new VideoMotionBounds(tr, tl, bl, br);
                        keyframe.Bounds = bh;
                    }
                    if (count == 2)
                        count = 0;
                    count += 1;
                }
            }
        }
    }
}
