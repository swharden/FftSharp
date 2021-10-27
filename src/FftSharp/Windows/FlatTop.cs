using System;
using System.Collections.Generic;
using System.Text;

namespace FftSharp.Windows
{
    public class FlatTop : Blackman
    {
        public override string Name => "Flat Top";
        public override string Description =>
            "A flat top window is a partially negative-valued window that has minimal scalloping loss in the frequency domain. " +
            "These properties are desirable for the measurement of amplitudes of sinusoidal frequency components. " +
            "Drawbacks of the broad bandwidth are poor frequency resolution and high noise bandwidth. " +
            "The flat top window crosses the zero line causing a broader peak in the frequency domain, " +
            "which is closer to the true amplitude of the signal than with other windows";

        public FlatTop() : base(0.2810639, 0.5208972, 0.1980399)
        {

        }
    }
}
