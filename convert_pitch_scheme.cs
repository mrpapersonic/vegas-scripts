/* NOTE: Requires VEGAS 16 or higher. */

using ScriptPortal.Vegas;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

public class EntryPoint {
    private Dictionary<string, int> stretchAttributes = new Dictionary<string, int>
    {
        {"Pro", 0},
        {"Efficient", 1},
        {"Soloist (Monophonic)", 2},
        {"Soloist (Speech)", 3},
        {"A01. Music 1 (minimum flange, may echo)", 4},
        {"A02. Music 2", 5},
        {"A03. Music 3 (less echo)", 6},
        {"A04. Music 4 (fast, good for bass)", 7},
        {"A05. Music 5", 8},
        {"A06. Music 6", 9},
        {"A07. Speech 1", 10},
        {"A08. Speech 2", 11},
        {"A09. Speech 3 (fast)", 12},
        {"A10. Solo instruments 1", 13},
        {"A11. Solo instruments 2", 14},
        {"A12. Solo instruments 3", 15},
        {"A13. Solo instruments 4 (less echo)", 16},
        {"A14. Solo instruments 5", 17},
        {"A15. Solo instruments 6", 18},
        {"A16. Solo instruments 7 (fast)", 19},
        {"A17. Drums, unpitched (minimum echo)", 20},
        {"A18. Drums (better for toms)", 21},
        {"A19. Drums (tiny echo)", 22}
    };
    public void FromVegas(Vegas vegas) {
        int stretchAttribute = Prompt.GetOptions(stretchAttributes);
        foreach (var track in vegas.Project.Tracks) {
            foreach (var trackEvent in track.Events) {
                if (trackEvent.Selected && trackEvent.IsAudio()) {
                    AudioEvent audio = trackEvent as AudioEvent;
                    if ((stretchAttribute >= 0) && (stretchAttribute <= 3)) {
                        audio.Method = (TimeStretchPitchShift)2; /* pitch shift method; 0 is elastique, 1 is acid style, 2 is classic, and 3 is none */
                        audio.ElastiqueAttribute = (ElastiqueStretchAttributes)stretchAttribute;
                    } else if (stretchAttribute >= 4) {
                        audio.Method = (TimeStretchPitchShift)0;
                        audio.ClassicAttribute = (ClassicStretchAttributes)stretchAttribute-4;
                    }
                }
            }
        }
    }
}

class Prompt
{
    public static int GetOptions(Dictionary<string,int> stretchAttributes)
    {
        Form prompt = new Form() {
            Width = 300,
            Height = 103,
            FormBorderStyle = FormBorderStyle.FixedSingle,
            MaximizeBox = false,
            MinimizeBox = false,
            ShowIcon = false,
            Text = "made by paper"
        };
        ComboBox
        inputValue = new ComboBox()
        {
            Left = 7,
            Top = 10,
            Width = (prompt.Width - 30)
        };
        Button
        confirmation = new Button()
        {
            Text = "OK",
            Left = 7,
            Width = (prompt.Width - 30),
            Top = 35,
            DialogResult = DialogResult.OK
        };

        inputValue.BeginUpdate();
        inputValue.DataSource = new BindingSource(stretchAttributes, null);
        inputValue.DisplayMember = "Key";
        inputValue.ValueMember = "Value";
        inputValue.EndUpdate();

        inputValue.DropDownStyle = ComboBoxStyle.DropDownList;

        prompt.Controls.Add(inputValue);
        prompt.Controls.Add(confirmation);
        prompt.AcceptButton = confirmation;

        confirmation.Click += (sender, e) =>
        {
            prompt.Close();
        };

        if (prompt.ShowDialog() == DialogResult.OK)
        {
            return inputValue.SelectedIndex;
        }
        else
        {
            return -1;
        }
    }
}